using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControlador : MonoBehaviour
{
    public Vector2 nuevaPosicion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           collision.GetComponent<JugadorController>().transform.position = nuevaPosicion;
        }
    }
}
