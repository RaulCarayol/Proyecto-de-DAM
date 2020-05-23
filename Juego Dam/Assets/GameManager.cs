using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameManager gameManager;
    public Text monedasText;
    public int monedas;
    public Vector3 checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        monedas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void sumarMonedas(int puntos)
    {
        monedas += puntos;
        monedasText.text = monedas + "";
    }
}
