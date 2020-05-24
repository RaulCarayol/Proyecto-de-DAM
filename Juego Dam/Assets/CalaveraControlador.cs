using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalaveraControlador : MonoBehaviour
{
    public Transform puntoAtaque;
    public GameObject bolaFuego;
    public GameObject gema;
    public float tiempoSiguienteAtaque = 2f;
    public float velocidadAtaque=0.2f;
    public Animator animator;
    bool estaGirado=false;
     Transform jugador;
    private Vector3 dir;
    public Rigidbody2D rb;
    BolaFuegoControlador bola;
    public int vidaMaxima = 80;
    public float rangoPared = 3;
    public LayerMask pared;
    public float veleocidadInicio = 2f;
    int vidaActual;
    bool enfurecido = false;
    public int maxContadorDelayCambioDireccion = 24;
    int cambiado = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
        jugador = GameObject.FindWithTag("Player").transform;
        cambiado = maxContadorDelayCambioDireccion;
    }

    // Update is called once per frame
    void Update()
    {
        //MirarAlJugador2();
        if (Vector2.Distance(jugador.position, transform.position) < 150)
        {
            //se mueve hacia la derecha (aunque segun la velocidadInicio puede ser izquierda)
            rb.velocity = transform.right * veleocidadInicio;
            //se mira si choca con la pared
            Collider2D[] paredTocado = Physics2D.OverlapCircleAll(transform.position, rangoPared, pared);
            for (int i = 0; i < paredTocado.Length; i++)
            {
                //si ha chocado con la pared
                if (paredTocado[i].gameObject != gameObject)
                {
                    //un pequeño contador para ajustar el tiempo de respuesta con el contacto con la pered del collider
                    if (cambiado >= maxContadorDelayCambioDireccion)
                    {
                        //se cambia la direccion
                        veleocidadInicio *= -1;
                        cambiado = 0;
                    }
                    else
                    {
                        cambiado++;
                    }
                }
            }
            // MirarAlJugador();
            //mirar si ya puede hacer un ataque
            if (Time.time >= tiempoSiguienteAtaque)
            {
                //nuevo tiempo para el siguiente ataque segun su velocidad de ataque
                tiempoSiguienteAtaque = Time.time + 1f / velocidadAtaque;
                //se genera una bola de fuego en el punto de ataque con rotacion 0 (Quaternion.identity)
                 Instantiate(bolaFuego,puntoAtaque.position,Quaternion.identity);

                //intento de rotar 
                /*
                Vector3 girado = transform.localScale;
                girado.x *= -1;
                Debug.Log("esta a " + transform.localScale);
                rb.transform.localScale = girado;
                rb.transform.Rotate(0f, 180f, 0f);
                */
            }

        }
    }

    public void RecibirDaño(int dañoRecibido)
    {
        vidaActual = vidaActual - dañoRecibido;

        if (vidaActual <= vidaMaxima / 2)
        {

            if (vidaActual <= 0)
            {

                rb.gravityScale = 0;
                rb.inertia = 0;
                GetComponent<Collider2D>().enabled = false;
                animator.SetBool("estaMuerto", true);
                //rb.Sleep();
            }
            else if(!enfurecido)
            {
                animator.SetBool("mitadVida", true);
                velocidadAtaque *= 2;
                enfurecido = true;
            }
        }
    }
    void Muere()
    {
        //animacion muerte
        //Debug.Log("Esqueleto muerto");
        //desabilitar
        Debug.Log("ha muerto: ");
        Instantiate(gema, puntoAtaque.position, Quaternion.identity);
        animator.enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }

    public void MirarAlJugador()
    {
        Vector3 girado = transform.localScale;
        girado.z *= -1f;

        if (transform.position.x > jugador.position.x && estaGirado)
        {
            transform.localScale = girado;
            transform.Rotate(0f, 180f, 0f);
            estaGirado = false;

        }
        else if (transform.position.x < jugador.position.x && !estaGirado)
        {
            transform.localScale = girado;
            transform.Rotate(0f, 180f, 0f);
            estaGirado = true;
        }

    }

    public void MirarAlJugador2()
    {
        //dir = jugador.position - transform.position;
        //rb.rotation += 5.0f;
        transform.LookAt(2 * transform.position - jugador.position);

    }
}
