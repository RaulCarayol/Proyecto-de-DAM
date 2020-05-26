using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject levelsMenu;
    // Start is called before the first frame update
    void Start()
    {
        levelsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelsMenu()
    {
        levelsMenu.SetActive(true);
    }
    public void JugarSupervivencia()
    {
        SceneManager.LoadScene("ModoInfinito");
    }
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
