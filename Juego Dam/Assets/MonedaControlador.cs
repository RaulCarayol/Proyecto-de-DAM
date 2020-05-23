using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaControlador : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.gameManager.sumarMonedas(1);
            //this.enabled=false;
            Destroy(transform.gameObject);
        }
    }
}
