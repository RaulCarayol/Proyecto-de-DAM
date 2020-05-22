using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool juegoParado = false;
    public GameObject pausaMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPausa();
        }

    }

    public void MenuPausa()
    {
        if (juegoParado)
        {
            Reinicia();
        }
        else
        {
            Pausa();
        }
    }

    public void Reinicia()
    {
        Debug.Log("Reinicia");
        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f;
        juegoParado = false;
    }
    public void Pausa()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f;
        juegoParado = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitarJuego()
    {
        Application.Quit();
    }
    
}
