using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaControlador : MonoBehaviour
{
    public int valorMonedas = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.gameManager.sumarMonedas(valorMonedas);
            //this.enabled=false;
            Destroy(transform.gameObject);
        }
    }
}
