using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneSequence : MonoBehaviour
{
    [Header("Subtitulos")]
    [SerializeField] private TMP_Text subtitleText;

    [Header("QTE")]
    [SerializeField] private QTEManager qteManager;

    [Header("Timeline")]
    [SerializeField] private PlayableDirector timeline;

    [Header("Corte a negro")]
    [SerializeField] private GameObject blackPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource voiceSource;

    [Header("Voces Guajiro")]
    [SerializeField] private AudioClip[] vocesGuajiro;

    [Header("Voces Niño")]
    [SerializeField] private AudioClip[] vocesNino;

    [Header("Voces Pueblo")]
    [SerializeField] private AudioClip[] vocesPueblo;

    [Header("Voces Narrador")]
    [SerializeField] private AudioClip[] vocesNarrador;


    private IEnumerator Start()
    {
        subtitleText.text = "";

        if (blackPanel != null)
        {
            blackPanel.SetActive(false);
        }

        yield return null;

        if (timeline != null)
        {
            timeline.time = 0;
            timeline.Play();
        }

        StartCoroutine(Story());
    }


    private IEnumerator Story()
    {
        // STORYBOARD 1

        yield return SubtitleTimeline(
            0.0,
            4.8,
            "CAMINO DE CAMPO — NOCHE"
        );

        yield return SubtitleTimeline(
            5.0,
            9.0,
            "Guajiro: Arre, Canela… vamos, mi’ja.\n" +
            "Que esta noche está demasiado callá pa’ mi gusto."
        );


        // STORYBOARD 2
        // llora


        // STORYBOARD 3

        yield return SubtitleTimeline(
            13.2,
            15.0,
            "Guajiro: ¿Eh? ¿Quién anda ahí?"
        );


        // STORYBOARD 4
        // aparece la guagua


        // STORYBOARD 5
        // KeTeE Random

        yield return WaitUntilTimeline(
            16.1
        );

        yield return PlayQTE(
            QTEManager.QTEType.Buttons
        );

        yield return SubtitleTimeline(
            16.2,
            19.7,
            "Guajiro: Ay, bendito…\n" +
            "¿Y tú qué haces aquí solito, muchachito?"
        );


        // STORYBOARD 6

        yield return SubtitleTimeline(
            19.9,
            23.5,
            "Guajiro: Na’, ven acá. No te voy a dejar botao en medio del monte."
        );


        // STORYBOARD 7

        yield return SubtitleTimeline(
            25.2,
            27.5,
            "MOMENTOS DESPUÉS"
        );


        // STORYBOARD 8

        yield return SubtitleTimeline(
            29.7,
            32.4,
            "Guajiro: ¿Qué fue? ¿Tienes hambre?"
        );

        yield return SubtitleTimeline(
            32.5,
            36.9,
            "Guajiro: Aguántate un tantico, que algo debo tener por aquí."
        );


        // STORYBOARD 9
        // diente / transformación


        // STORYBOARD 10

        yield return SubtitleTimeline(
            40.1,
            43.0,
            "Guajiro: Ave María Purísima…\n" +
            "¿Pero qué cosa eres tú?"
        );


        // STORYBOARD 11

        yield return SubtitleTimeline(
            43.1,
            44.6,
            "Niño: Tengo hambre."
        );

        yield return SubtitleTimeline(
            44.7,
            47.0,
            "Guajiro: ¡Quítate de arriba, condenao!"
        );


        // STORYBOARD 12
        // KeTeE terremoto

        yield return WaitUntilTimeline(
            47.3
        );

        yield return PlayQTE(
            QTEManager.QTEType.Shake
        );

        yield return SubtitleTimeline(
            47.4,
            48.9,
            "Niño: ¡Tengo hambre!"
        );

        yield return SubtitleTimeline(
            49.0,
            51.2,
            "Guajiro: ¡Pues conmigo te vas a quedar con ella!"
        );


        // STORYBOARD 13
        // Empuja


        // STORYBOARD 14

        yield return SubtitleTimeline(
            52.4,
            55.3,
            "Guajiro: ¡Arre, Canela! ¡Arre, mi’ja!"
        );


        // STORYBOARD 15
        // KeTee pium pium

        yield return WaitUntilTimeline(
            55.5
        );

        yield return PlayQTE(
            QTEManager.QTEType.Triggers
        );


        // STORYBOARD 16 / 17

        yield return SubtitleTimeline(
            58.6,
            60.0,
            "Guajiro: ¡Canela! ¡Quieta!"
        );

        yield return SubtitleTimeline(
            60.0,
            60.8,
            "Guajiro: ¡Ay, mi madre!"
        );


        // STORYBOARD 18
        // negro


        // STORYBOARD 19

        yield return SubtitleTimeline(
            63.3,
            64.7,
            "AMANECER"
        );


        // STORYBOARD 20

        yield return SubtitleTimeline(
            64.8,
            66.8,
            "Guajiro: Ay… carijo…"
        );

        yield return SubtitleTimeline(
            67.0,
            70.9,
            "Guajiro: De muchacho aquello no tenía na’…"
        );


        // STORYBOARD 21
        // pueblo

        yield return SubtitleTimeline(
            71.3,
            72.8,
            "PUEBLO — MAÑANA"
        );

        yield return SubtitleTimeline(
            72.9,
            74.8,
            "Vecino: ¡Compay! ¿Qué le pasó?"
        );

        yield return SubtitleTimeline(
            75.0,
            79.0,
            "Guajiro: Anoche encontré un muchachito llorando solo en el camino."
        );

        yield return SubtitleTimeline(
            79.1,
            80.9,
            "Vecina: ¿Y dónde está?"
        );

        yield return SubtitleTimeline(
            81.0,
            83.6,
            "Guajiro: Eso no era ningún muchachito."
        );

        yield return SubtitleTimeline(
            84.0,
            88.5,
            "Guajiro: Le salió un diente así de largo…\n" +
            "y después me dijo que tenía hambre."
        );

        yield return SubtitleTimeline(
            89.0,
            94.5,
            "Guajiro: Así que si oyen a una criatura llorando por el monte de noche…"
        );

        yield return SubtitleTimeline(
            95.0,
            99.0,
            "Guajiro: …sigan pa’lante y no miren pa’trás."
        );


        // FINAL

        yield return WaitUntilTimeline(
            104.0
        );

        subtitleText.text = "";

        if (blackPanel != null)
        {
            blackPanel.SetActive(true);
        }

        yield return new WaitForSeconds(
            1.5f
        );

        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene(
            "Menu"
        );
    }


    // subs sincronizados con Timeline

    private IEnumerator SubtitleTimeline(
        double startTime,
        double endTime,
        string text
    )
    {
        yield return WaitUntilTimeline(
            startTime
        );

        subtitleText.text = text;

        yield return WaitUntilTimeline(
            endTime
        );

        subtitleText.text = "";
    }


    // esperar directamente al tiempo del Timeline

    private IEnumerator WaitUntilTimeline(
        double targetTime
    )
    {
        while (
            timeline != null &&
            timeline.time < targetTime
        )
        {
            yield return null;
        }
    }


    // KeTeE

    private IEnumerator PlayQTE(
        QTEManager.QTEType type
    )
    {
        subtitleText.text = "";

        if (timeline != null)
        {
            timeline.Pause();
        }

        yield return qteManager.Play(
            type
        );

        if (timeline != null)
        {
            timeline.Resume();
        }
    }


    // voces guardadas por si volvemos a usarlas sin Timeline

    private AudioClip GetVoice(
        AudioClip[] voices,
        int index
    )
    {
        if (
            voices == null ||
            index < 0 ||
            index >= voices.Length
        )
        {
            return null;
        }

        return voices[index];
    }


    // AUDIO DESACTIVADO
    // AHORA LAS VOCES SE REPRODUCEN DESDE TIMELINE

    /*
    private void PlayVoice(
        AudioClip voice
    )
    {
        if (
            voice != null &&
            voiceSource != null
        )
        {
            voiceSource.PlayOneShot(
                voice
            );
        }
    }
    */
}