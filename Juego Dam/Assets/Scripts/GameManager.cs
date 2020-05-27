using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameManager gameManager;
    public int puntuacion;
    public Vector3 checkpoint;
    public Joystick joystick;
    public Button botonAtacar;
    public Text textoMonedas;

    // Start is called before the first frame update
    void Start()
    {
        puntuacion = 0;
        gameManager = this;
        OpcionesDatos opcionesDatos = GuardarDatos.CargarDatosOpciones();
        if (opcionesDatos != null)
        {
            //recuperar las escalas
            joystick.transform.localScale = Vector3.one * opcionesDatos.escalaJoystick;
            botonAtacar.transform.localScale = Vector3.one * opcionesDatos.escalaBotonAtaque;

        }
    }

    // Update is called once per frame
    void Update()
    {
        textoMonedas.text = puntuacion + "";
    }
    public void sumarMonedas(int monedas)
    {
        puntuacion += monedas;
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void VolverJugar()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
