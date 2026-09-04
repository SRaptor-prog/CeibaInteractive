using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GyroInputTester : MonoBehaviour
{
    private const string LayoutName = "DualShock4GamepadHIDCustom";

    private const string LayoutJson = @"
    {
        ""name"": ""DualShock4GamepadHIDCustom"",
        ""extend"": ""DualShock4GamepadHID"",
        ""controls"": [
            {
                ""name"": ""gyro"",
                ""format"": ""VC3S"",
                ""offset"": 13,
                ""layout"": ""Vector3"",
                ""processors"": ""ScaleVector3(x=-1,y=-1,z=1)""
            },
            {
                ""name"": ""gyro/x"",
                ""format"": ""SHRT"",
                ""offset"": 0
            },
            {
                ""name"": ""gyro/y"",
                ""format"": ""SHRT"",
                ""offset"": 2
            },
            {
                ""name"": ""gyro/z"",
                ""format"": ""SHRT"",
                ""offset"": 4
            }
        ]
    }";

    private Quaternion accumulatedGyro = Quaternion.identity;

    private InputAction gyroAction;

    public Vector3 GyroValue { get; private set; }

    private static Quaternion GyroInputToRotation(
        InputAction.CallbackContext context)
    {
        Vector3 gyro = context.ReadValue<Vector3>();

        const double GyroToAngle =
            16.0 * 360.0 / System.Math.PI;

        double deltaTime =
            context.time -
            context.control.device.lastUpdateTime;

        deltaTime =
            System.Math.Min(
                deltaTime,
                1.0 / 60.0
            );

        return Quaternion.Euler(
            gyro *
            (float)(GyroToAngle * deltaTime)
        );
    }

    private void Start()
    {
        if (InputSystem.LoadLayout(LayoutName) == null)
        {
            InputSystem.RegisterLayoutOverride(
                LayoutJson
            );
        }

        gyroAction =
            new InputAction(
                binding: "<Gamepad>/gyro"
            );

        gyroAction.performed += OnGyro;
        gyroAction.canceled += OnGyroCanceled;

        gyroAction.Enable();
    }

    private void OnGyro(
        InputAction.CallbackContext context)
    {
        GyroValue =
            context.ReadValue<Vector3>();

        accumulatedGyro *=
            GyroInputToRotation(context);
    }

    private void OnGyroCanceled(
        InputAction.CallbackContext context)
    {
        GyroValue = Vector3.zero;
    }

    private void Update()
    {
        transform.localRotation *=
            accumulatedGyro;

        accumulatedGyro =
            Quaternion.identity;
    }

    private void OnDestroy()
    {
        if (gyroAction != null)
        {
            gyroAction.performed -= OnGyro;
            gyroAction.canceled -= OnGyroCanceled;

            gyroAction.Disable();
            gyroAction.Dispose();

            gyroAction = null;
        }
    }
}