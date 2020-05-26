using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonControlador : MonoBehaviour
{
    public int vida = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<JugadorVida>().RecibirVida(vida);
            //this.enabled=false;
            Destroy(transform.gameObject);
        }
    }
}
