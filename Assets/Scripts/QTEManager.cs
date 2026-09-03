using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Controls;

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

    [Header("QTE 1 - Botones")]
    [SerializeField] private int buttonsRequired = 5;
    [SerializeField] private float buttonsDuration = 5f;

    [Header("QTE Agitar")]
    [SerializeField] private int shakesRequired = 8;
    [SerializeField] private float shakeDuration = 6f;
    [SerializeField] private float degreesPerShake = 15f;

    [SerializeField] private Transform ds4Motion;

    [Header("QTE 3 - Gatillos")]
    [SerializeField] private int triggersRequired = 10;
    [SerializeField] private float triggersDuration = 6f;

    private readonly string[] playstationSymbols =
    {
        "X",
        "O",
        "Cuadrado",
        "Triangulo"
    };

    private readonly string[] keyboardSymbols =
    {
        "↓",
        "→",
        "←",
        "↑"
    };

    private void Awake()
    {
        qtePanel.SetActive(false);

        timerBar.minValue = 0f;
        timerBar.maxValue = 1f;
        timerBar.value = 1f;
    }

    public IEnumerator Play(QTEType type)
    {
        qtePanel.SetActive(true);

        bool completed = false;

        while (!completed)
        {
            switch (type)
            {
                case QTEType.Buttons:
                    completed = false;
                    yield return ButtonsQTE(result => completed = result);
                    break;

                case QTEType.Shake:
                    completed = false;
                    yield return ShakeQTE(result => completed = result);
                    break;

                case QTEType.Triggers:
                    completed = false;
                    yield return TriggersQTE(result => completed = result);
                    break;
            }

            if (!completed)
                yield return ShowFailure();
        }

        instructionText.text = "¡LISTO!";
        symbolText.text = "";
        timerBar.value = 1f;

        yield return new WaitForSeconds(0.4f);

        qtePanel.SetActive(false);
    }

    // KeTeE 1 - Botnes Random

    private IEnumerator ButtonsQTE(System.Action<bool> result)
    {
        int completedButtons = 0;
        int expectedButton = Random.Range(0, 4);

        float timeLeft = buttonsDuration;

        while (timeLeft > 0f && completedButtons < buttonsRequired)
        {
            instructionText.text = "¡PULSA!";

            if (Gamepad.current != null)
            {
                symbolText.text = playstationSymbols[expectedButton];
            }
            else
            {
                symbolText.text =
                    playstationSymbols[expectedButton] +
                    "    " +
                    keyboardSymbols[expectedButton];
            }

            int pressedButton = ReadFaceButton();

            if (pressedButton == expectedButton)
            {
                completedButtons++;

                if (completedButtons < buttonsRequired)
                {
                    int previous = expectedButton;

                    do
                    {
                        expectedButton = Random.Range(0, 4);
                    }
                    while (expectedButton == previous);
                }
            }

            UpdateTimer(ref timeLeft, buttonsDuration);

            yield return null;
        }

        result(completedButtons >= buttonsRequired);
    }

    // KeTeE 2 - Terremoto


    private IEnumerator ShakeQTE(System.Action<bool> result)
    {
        int shakes = 0;
        float timeLeft = shakeDuration;

        float accumulatedMovement = 0f;

        Quaternion lastRotation = ds4Motion.localRotation;

        while (timeLeft > 0f && shakes < shakesRequired)
        {
            instructionText.text = "¡FORCEJEA!";
            symbolText.text = "¡AGITA EL MANDO!";

            Quaternion currentRotation = ds4Motion.localRotation;

            float movement =
                Quaternion.Angle(lastRotation, currentRotation);

            // Ignoramos movimientos minúsculos / ruido
            if (movement > 0.2f)
            {
                accumulatedMovement += movement;
            }

            // Cuando acumulamos suficiente movimiento,
            // cuenta como una sacudida
            if (accumulatedMovement >= degreesPerShake)
            {
                shakes++;

                accumulatedMovement = 0f;

                Debug.Log(
                    "Sacudida " +
                    shakes +
                    "/" +
                    shakesRequired
                );
            }

            lastRotation = currentRotation;

            UpdateTimer(ref timeLeft, shakeDuration);

            yield return null;
        }

        result(shakes >= shakesRequired);
    }

    // KeTeE 3 - Piu Piu (gatillos) 


    private IEnumerator TriggersQTE(System.Action<bool> result)
    {
        int completedTriggers = 0;

        bool expectLeft = true;

        float timeLeft = triggersDuration;

        while (timeLeft > 0f && completedTriggers < triggersRequired)
        {
            instructionText.text = "¡ARRE, CANELA!";

            symbolText.text = expectLeft
         ? "L2      Q"
         : "R2      E";

            int trigger = ReadTrigger();

            bool correct =
                (expectLeft && trigger == -1) ||
                (!expectLeft && trigger == 1);

            if (correct)
            {
                completedTriggers++;
                expectLeft = !expectLeft;
            }

            UpdateTimer(ref timeLeft, triggersDuration);

            yield return null;
        }

        result(completedTriggers >= triggersRequired);
    }

   
    // Tiempito
    

    private void UpdateTimer(ref float timeLeft, float duration)
    {
        timeLeft -= Time.deltaTime;

        timerBar.value =
            Mathf.Clamp01(timeLeft / duration);
    }

   
    // Botones
  

    private int ReadFaceButton()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        // X / boton abajo
        if ((keyboard != null &&
             keyboard.downArrowKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonSouth.wasPressedThisFrame))
        {
            return 0;
        }

        // O / botón derecho
        if ((keyboard != null &&
             keyboard.rightArrowKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonEast.wasPressedThisFrame))
        {
            return 1;
        }

        // Cuadrado / botón izquierdo
        if ((keyboard != null &&
             keyboard.leftArrowKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonWest.wasPressedThisFrame))
        {
            return 2;
        }

        // Triángulo / botón arriba
        if ((keyboard != null &&
             keyboard.upArrowKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.buttonNorth.wasPressedThisFrame))
        {
            return 3;
        }

        return -1;
    }

    
    // Agitar con teclao porque no tengo control kekw
    

    private int ReadShake()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.aKey.wasPressedThisFrame)
                return -1;

            if (keyboard.dKey.wasPressedThisFrame)
                return 1;
        }

        Gamepad gamepad = Gamepad.current;

        if (gamepad != null)
        {
            float stickX =
                gamepad.leftStick.x.ReadValue();

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
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        if ((keyboard != null &&
             keyboard.qKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.leftTrigger.wasPressedThisFrame))
        {
            return -1;
        }

        if ((keyboard != null &&
             keyboard.eKey.wasPressedThisFrame) ||
            (gamepad != null &&
             gamepad.rightTrigger.wasPressedThisFrame))
        {
            return 1;
        }

        return 0;
    }

 
    // wajaaaaa fallaste *meme del gato riendose*
 

    private IEnumerator ShowFailure()
    {
        instructionText.text = "FALLASTE";
        symbolText.text = "";
        timerBar.value = 0f;

        yield return new WaitForSeconds(0.8f);

        timerBar.value = 1f;
    }
}