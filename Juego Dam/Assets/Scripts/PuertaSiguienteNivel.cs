using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSiguienteNivel : MonoBehaviour
{
    public CargaInicio cargarInicio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("siguienteNivel");
            cargarInicio.setSiguienteNivel(true);
        }
        else
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
