using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaTraslador : MonoBehaviour
{
    public GameObject puertaAbierta;
    public GameObject puertaCerrada;
    public enemigo enemigo1;
    public enemigo enemigo2;
    public Vector2 nuevaPosicion;
    // Start is called before the first frame update
    void Start()
    {
        puertaAbierta.SetActive(false);
        puertaCerrada.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigo1 == null && enemigo2 == null ||
               enemigo1 == null && enemigo2.vidaActual <= 0 ||
               enemigo1.vidaActual <= 0 && enemigo2.vidaActual <= 0)
        {
            puertaAbierta.SetActive(true);
            puertaCerrada.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (enemigo1 == null && enemigo2 == null ||
                enemigo1 == null && enemigo2.vidaActual<=0 ||
                enemigo1.vidaActual <= 0 && enemigo2.vidaActual <= 0)
            {
                collision.GetComponent<JugadorController>().transform.position = nuevaPosicion;
            }
        }
    }
}
