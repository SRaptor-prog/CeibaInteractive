using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainPausePanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Timeline")]
    [SerializeField] private PlayableDirector timeline;

    private bool paused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        mainPausePanel.SetActive(true);
        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    private void Update()
    {
        if (
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame
        )
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        paused = true;

        pauseMenu.SetActive(true);
        mainPausePanel.SetActive(true);
        optionsPanel.SetActive(false);

        if (timeline != null)
        {
            timeline.Pause();
        }

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        paused = false;

        pauseMenu.SetActive(false);
        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (timeline != null)
        {
            timeline.Resume();
        }
    }

    public void OpenOptions()
    {
        mainPausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainPausePanel.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene("Menu");
    }
}
    

