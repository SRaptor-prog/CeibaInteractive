using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject panelOpciones;

    private void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        menuPrincipal.SetActive(true);
        panelOpciones.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("PruebaQTE");
    }

    public void AbrirOpciones()
    {
        menuPrincipal.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }
}