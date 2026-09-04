using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(
            "Volume",
            1f
        );

        AudioListener.volume = volume;

        volumeSlider.value = volume;

        volumeSlider.onValueChanged.AddListener(
            CambiarVolumen
        );
    }

    private void CambiarVolumen(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat(
            "Volume",
            volume
        );

        PlayerPrefs.Save();
    }
}