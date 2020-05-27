using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuegoControlador : MonoBehaviour
{
    public float velocidad = 3f;
    public int daño=5;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vecVelocidad = new Vector3(0, -1*velocidad,0);
        rb.velocity = vecVelocidad;
            //transform.right * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<JugadorVida>().RecibirDaño(daño);
            Destroy(gameObject);
        }
        else if (collision.tag == "Suelo" 
            || collision.tag == "Pared" 
            || collision.tag == "Enemigo" 
            || collision.tag == "Limites")
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
