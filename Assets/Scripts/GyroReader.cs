using MoreStories.GyroTools;
using UnityEngine;
using UnityEngine.InputSystem;  

public class GyroReader : MonoBehaviour
{
    [SerializeField] private Vector3 gyroValue;
    [SerializeField] private Vector3 accelerationValue;

    private InputAction imuAction;

    public Vector3 GyroValue
    {
        get { return gyroValue; }
    }

    public Vector3 AccelerationValue
    {
        get { return accelerationValue; }
    }

    private void OnEnable()
    {
        imuAction = new InputAction(
            binding: "<Gamepad>/IMU"
        );

        imuAction.performed += ReadIMU;

        imuAction.Enable();
    }

    private void ReadIMU(InputAction.CallbackContext context)
    {
        IMUState imu = context.ReadValue<IMUState>();

        gyroValue = imu.gyroscope;
        accelerationValue = imu.accelerometer;
    }

    private void OnDisable()
    {
        if (imuAction != null)
        {
            imuAction.performed -= ReadIMU;

            imuAction.Disable();
            imuAction.Dispose();

            imuAction = null;
        }

        gyroValue = Vector3.zero;
        accelerationValue = Vector3.zero;
    }
}