using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaInicio : MonoBehaviour
{
    bool siguienteNivel = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(siguienteNivel)
        {
            CargarSiguienteNivel();
        }
    }
    public void CargarSiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void setSiguienteNivel(bool sigueNivel)
    {
        siguienteNivel = sigueNivel;
    }
}
