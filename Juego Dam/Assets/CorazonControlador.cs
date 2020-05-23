using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonControlador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<JugadorVida>().RecibirVida(50);
            //this.enabled=false;
            Destroy(transform.gameObject);
        }
    }
}
