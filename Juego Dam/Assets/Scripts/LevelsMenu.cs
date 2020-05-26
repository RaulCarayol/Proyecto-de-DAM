using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public GameObject levelsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Volver()
    {
        levelsMenu.SetActive(false);
    }
    public void JugarTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void JugarNivel(int nivel)
    {
        SceneManager.LoadScene("Nivel_"+nivel);
    }
}
