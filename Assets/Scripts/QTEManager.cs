using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public enum QTEType
    {
        Buttons,
        Shake,
        Triggers
    }

    [Header("UI")]
    [SerializeField] private GameObject qtePanel;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text symbolText;
    [SerializeField] private Slider timerBar;

    [Header("Imagenes botones")]
    [SerializeField] private Image buttonIcon;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;

    [Header("Visuales QTE")]
    [SerializeField] private GameObject shakeVisual;
    [SerializeField] private GameObject triggerVisual;
    [SerializeField] private RectTransform l2Icon;
    [SerializeField] private RectTransform r2Icon;

    [Header("QTE 1 - Botones")]
    [SerializeField] private int buttonsRequired = 5;
    [SerializeField] private float buttonsDuration = 5f;

    [Header("QTE Agitar")]
    [SerializeField] private int shakesRequired = 5;
    [SerializeField] private float shakeDuration = 6f;
    [SerializeField] private GyroReader gyroReader;
    [SerializeField] private float shakeThreshold = 1.5f;
    [SerializeField] private float gyroXDebug;

    [Header("QTE 3 - Gatillos")]
    [SerializeField] private int triggersRequired = 10;
    [SerializeField] private float triggersDuration = 6f;

    private void Awake()
    {
        qtePanel.SetActive(false);

        buttonIcon.gameObject.SetActive(false);
        shakeVisual.SetActive(false);
        triggerVisual.SetActive(false);

        timerBar.minValue = 0f;
        timerBar.maxValue = 1f;
        timerBar.value = 1f;
    }

    public IEnumerator Play(QTEType type)
    {
        qtePanel.SetActive(true);

        buttonIcon.gameObject.SetActive(
            type == QTEType.Buttons
        );

        shakeVisual.SetActive(
            type == QTEType.Shake
        );

        triggerVisual.SetActive(
            type == QTEType.Triggers
        );

        bool completed = false;

        while (!completed)
        {
            switch (type)
            {
                case QTEType.Buttons:
                    completed = false;
                    yield return ButtonsQTE(
                        result => completed = result
                    );
                    break;

                case QTEType.Shake:
                    completed = false;
                    yield return ShakeQTE(
                        result => completed = result
                    );
                    break;

                case QTEType.Triggers:
                    completed = false;
                    yield return TriggersQTE(
                        result => completed = result
                    );
                    break;
            }

            if (!completed)
                yield return ShowFailure();
        }

        instructionText.text = "¡LISTO!";
        symbolText.text = "";

        buttonIcon.gameObject.SetActive(false);
        shakeVisual.SetActive(false);
        triggerVisual.SetActive(false);

        timerBar.value = 1f;

        yield return new WaitForSeconds(0.4f);

        qtePanel.SetActive(false);
    }

    // KeTeE 1 - Botnes Random

    private IEnumerator ButtonsQTE(
        System.Action<bool> result
    )
    {
        buttonIcon.gameObject.SetActive(true);
        shakeVisual.SetActive(false);
        triggerVisual.SetActive(false);

        int completedButtons = 0;
        int expectedButton = Random.Range(0, 4);

        float timeLeft = buttonsDuration;

        symbolText.text = "";

        while (
            timeLeft > 0f &&
            completedButtons < buttonsRequired
        )
        {
            instructionText.text = "¡PULSA!";

            buttonIcon.sprite =
                GetButtonSprite(expectedButton);

            buttonIcon.preserveAspect = true;

            int pressedButton =
                ReadFaceButton();

            if (pressedButton == expectedButton)
            {
                completedButtons++;

                if (
                    completedButtons <
                    buttonsRequired
                )
                {
                    int previous =
                        expectedButton;

                    do
                    {
                        expectedButton =
                            Random.Range(0, 4);
                    }
                    while (
                        expectedButton ==
                        previous
                    );
                }
            }

            UpdateTimer(
                ref timeLeft,
                buttonsDuration
            );

            yield return null;
        }

        result(
            completedButtons >= buttonsRequired
        );
    }

    // KeTeE 2 - Terremoto


    private IEnumerator ShakeQTE(
        System.Action<bool> result
    )
    {
        buttonIcon.gameObject.SetActive(false);
        shakeVisual.SetActive(true);
        triggerVisual.SetActive(false);

        int shakes = 0;

        float timeLeft = shakeDuration;

        bool wentForward = false;

        while (
            timeLeft > 0f &&
            shakes < shakesRequired
        )
        {
            instructionText.text =
                "¡FORCEJEA!";

            symbolText.text = "";

            float gyroX =
                gyroReader.GyroValue.x;

            gyroXDebug = gyroX;

            if (
                !wentForward &&
                gyroX > shakeThreshold
            )
            {
                wentForward = true;

                Debug.Log("Adelante");
            }

            if (
                wentForward &&
                gyroX < -shakeThreshold
            )
            {
                shakes++;

                wentForward = false;

                Debug.Log(
                    "Sacudida " +
                    shakes +
                    "/" +
                    shakesRequired
                );
            }

            UpdateTimer(
                ref timeLeft,
                shakeDuration
            );

            yield return null;
        }

        result(
            shakes >= shakesRequired
        );
    }

    // KeTeE 3 - Piu Piu (gatillos) 


    private IEnumerator TriggersQTE(
        System.Action<bool> result
    )
    {
        buttonIcon.gameObject.SetActive(false);
        shakeVisual.SetActive(false);
        triggerVisual.SetActive(true);

        int completedTriggers = 0;

        bool expectLeft = true;

        float timeLeft =
            triggersDuration;

        symbolText.text = "";

        while (
            timeLeft > 0f &&
            completedTriggers <
            triggersRequired
        )
        {
            instructionText.text =
                "¡ARRE, CANELA!";

            if (expectLeft)
            {
                l2Icon.localScale =
                    new Vector3(
                        1.25f,
                        1.25f,
                        1f
                    );

                r2Icon.localScale =
                    Vector3.one;
            }
            else
            {
                l2Icon.localScale =
                    Vector3.one;

                r2Icon.localScale =
                    new Vector3(
                        1.25f,
                        1.25f,
                        1f
                    );
            }

            int trigger =
                ReadTrigger();

            bool correct =
                (expectLeft &&
                 trigger == -1) ||
                (!expectLeft &&
                 trigger == 1);

            if (correct)
            {
                completedTriggers++;

                expectLeft =
                    !expectLeft;
            }

            UpdateTimer(
                ref timeLeft,
                triggersDuration
            );

            yield return null;
        }

        l2Icon.localScale =
            Vector3.one;

        r2Icon.localScale =
            Vector3.one;

        result(
            completedTriggers >=
            triggersRequired
        );
    }


    // Tiempito


    private void UpdateTimer(
        ref float timeLeft,
        float duration
    )
    {
        timeLeft -= Time.deltaTime;

        timerBar.value =
            Mathf.Clamp01(
                timeLeft / duration
            );
    }


    // Botones


    private int ReadFaceButton()
    {
        Keyboard keyboard =
            Keyboard.current;

        Gamepad gamepad =
            Gamepad.current;

        // X / boton abajo
        if (
            (keyboard != null &&
             keyboard.downArrowKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonSouth
                .wasPressedThisFrame)
        )
        {
            return 0;
        }

        // O / botón derecho
        if (
            (keyboard != null &&
             keyboard.rightArrowKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonEast
                .wasPressedThisFrame)
        )
        {
            return 1;
        }

        // Cuadrado / botón izquierdo
        if (
            (keyboard != null &&
             keyboard.leftArrowKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonWest
                .wasPressedThisFrame)
        )
        {
            return 2;
        }

        // Triángulo / botón arriba
        if (
            (keyboard != null &&
             keyboard.upArrowKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonNorth
                .wasPressedThisFrame)
        )
        {
            return 3;
        }

        return -1;
    }


    // Agitar con teclao porque no tengo control kekw


    private int ReadShake()
    {
        Keyboard keyboard =
            Keyboard.current;

        if (keyboard != null)
        {
            if (
                keyboard.aKey
                    .wasPressedThisFrame
            )
                return -1;

            if (
                keyboard.dKey
                    .wasPressedThisFrame
            )
                return 1;
        }

        Gamepad gamepad =
            Gamepad.current;

        if (gamepad != null)
        {
            float stickX =
                gamepad.leftStick.x
                    .ReadValue();

            if (stickX < -0.7f)
                return -1;

            if (stickX > 0.7f)
                return 1;
        }

        return 0;
    }


    // gatillos, lo mismo de arriba sigo sin control 


    private int ReadTrigger()
    {
        Keyboard keyboard =
            Keyboard.current;

        Gamepad gamepad =
            Gamepad.current;

        if (
            (keyboard != null &&
             keyboard.qKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.leftTrigger
                .wasPressedThisFrame)
        )
        {
            return -1;
        }

        if (
            (keyboard != null &&
             keyboard.eKey
                .wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.rightTrigger
                .wasPressedThisFrame)
        )
        {
            return 1;
        }

        return 0;
    }


    // wajaaaaa fallaste *meme del gato riendose*


    private IEnumerator ShowFailure()
    {
        instructionText.text =
            "FALLASTE";

        symbolText.text = "";

        timerBar.value = 0f;

        yield return new WaitForSeconds(
            0.8f
        );

        timerBar.value = 1f;
    }

    private Sprite GetButtonSprite(
        int button
    )
    {
        switch (button)
        {
            case 0:
                return xSprite;

            case 1:
                return circleSprite;

            case 2:
                return squareSprite;

            case 3:
                return triangleSprite;

            default:
                return null;
        }
    }
}