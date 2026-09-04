using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

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
        blackPanel.SetActive(false);

        yield return null;

        timeline.time = 0;
        timeline.Play();

        StartCoroutine(Story());
    }

    private IEnumerator Story()
    {
        yield return Subtitle(
            "CAMINO DE CAMPO — NOCHE",
            2f,
            vocesNarrador[0]
        );

        yield return Subtitle(
            "Guajiro: Arre, Canela… vamos, mi’ja.\n" +
            "Que esta noche está demasiado callá pa’ mi gusto.",
            4f,
            vocesGuajiro[0]
        );

        yield return Wait(1f);

        // llora
        yield return Wait(2f);

        yield return Subtitle(
            "Guajiro: ¿Eh? ¿Quién anda ahí?",
            2f,
            vocesGuajiro[1]
        );

        // aparece la guagua
        yield return Wait(1f);

        // KeTeE Random
        yield return PlayQTE(QTEManager.QTEType.Buttons);

        yield return Subtitle(
            "Guajiro: Ay, bendito…\n" +
            "¿Y tú qué haces aquí solito, muchachito?",
            3.5f,
            vocesGuajiro[2]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: Na’, ven acá. No te voy a dejar botao en medio del monte.",
            3.5f,
            vocesGuajiro[3]
        );

        // wawa fea se acerca a guajiro full hd 
        yield return Wait(1f);
        yield return Wait(1f);

        yield return Subtitle(
            "MOMENTOS DESPUÉS",
            1.5f,
            vocesNarrador[1]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: ¿Qué fue? ¿Tienes hambre?",
            2.5f,
            vocesGuajiro[4]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: Aguántate un tantico, que algo debo tener por aquí.",
            3f,
            vocesGuajiro[5]
        );

        yield return Wait(1f);

        // diente
        yield return Wait(3f);

        yield return Subtitle(
            "Guajiro: Ave María Purísima…\n" +
            "¿Pero qué cosa eres tú?",
            3f,
            vocesGuajiro[6]
        );

        yield return Subtitle(
            "Niño: Tengo hambre.",
            2f,
            vocesNino[0]
        );

        // Ataque
        yield return Wait(0.3f);

        yield return Subtitle(
            "Guajiro: ¡Quítate de arriba, condenao!",
            2f,
            vocesGuajiro[7]
        );

        // KeTeE terremoto
        yield return PlayQTE(QTEManager.QTEType.Shake);

        yield return Subtitle(
            "Niño: ¡Tengo hambre!",
            1.5f,
            vocesNino[1]
        );

        yield return Subtitle(
            "Guajiro: ¡Pues conmigo te vas a quedar con ella!",
            2.5f,
            vocesGuajiro[8]
        );

        // Empuja
        yield return Wait(0.5f);

        yield return Wait(0.5f);

        yield return Subtitle(
            "Guajiro: ¡Arre, Canela! ¡Arre, mi’ja!",
            2f,
            vocesGuajiro[9]
        );

        // runnnn **** runnn
        yield return Wait(1f);

        // KeTee pium pium
        yield return PlayQTE(QTEManager.QTEType.Triggers);

        yield return Wait(0.5f);

        yield return Subtitle(
            "Guajiro: ¡Canela! ¡Quieta!",
            2f,
            vocesGuajiro[10]
        );

        yield return Wait(0.5f);

        yield return Subtitle(
            "Guajiro: ¡Ay, mi madre!",
            1.5f,
            vocesGuajiro[11]
        );

        yield return Wait(0.7f);

        // pokemon black 2
        yield return Blackout(1.5f);

        // se hace la lu'
        yield return Subtitle(
            "AMANECER",
            1.5f,
            vocesNarrador[2]
        );

        yield return Subtitle(
            "Guajiro: Ay… carijo…",
            2f,
            vocesGuajiro[12]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: De muchacho aquello no tenía na’…",
            2.5f,
            vocesGuajiro[13]
        );

        yield return Wait(1f);

        // pueblo 
        yield return Blackout(1f);

        yield return Subtitle(
            "PUEBLO — MAÑANA",
            1.5f,
            vocesNarrador[3]
        );

        yield return Subtitle(
            "Vecino: ¡Compay! ¿Qué le pasó?",
            2f,
            vocesPueblo[0]
        );

        yield return Subtitle(
            "Guajiro: Anoche encontré un muchachito llorando solo en el camino.",
            3f,
            vocesGuajiro[14]
        );

        yield return Subtitle(
            "Vecina: ¿Y dónde está?",
            1.8f,
            vocesPueblo[1]
        );

        yield return Subtitle(
            "Guajiro: Eso no era ningún muchachito.",
            2.5f,
            vocesGuajiro[15]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: Le salió un diente así de largo…\n" +
            "y después me dijo que tenía hambre.",
            3.5f,
            vocesGuajiro[16]
        );

        yield return Wait(1.5f);

        yield return Subtitle(
            "Guajiro: Así que si oyen a una criatura llorando por el monte de noche…",
            3.5f,
            vocesGuajiro[17]
        );

        yield return Wait(1f);

        yield return Subtitle(
            "Guajiro: …sigan pa’lante y no miren pa’trás.",
            3f,
            vocesGuajiro[18]
        );

        yield return Wait(1f);

        // muejejeje
        yield return Wait(2.5f);

        blackPanel.SetActive(true);
        subtitleText.text = "";
    }

    // subs

    private IEnumerator Subtitle(string text, float duration)
    {
        subtitleText.text = text;

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
    }

    private IEnumerator Subtitle(string text, float duration, AudioClip voice)
    {
        subtitleText.text = text;

        if (voice != null)
        {
            voiceSource.Stop();
            voiceSource.PlayOneShot(voice);
        }

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
    }

    // KeTeE

    private IEnumerator PlayQTE(QTEManager.QTEType type)
    {
        subtitleText.text = "";
        timeline.Pause();

        yield return qteManager.Play(type);

        timeline.Resume();
    }

    // waiteo

    private IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    // pokemon black

    private IEnumerator Blackout(float duration)
    {
        subtitleText.text = "";

        blackPanel.SetActive(true);

        yield return new WaitForSeconds(duration);

        blackPanel.SetActive(false);
    }
}