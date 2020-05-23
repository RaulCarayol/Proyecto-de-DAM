using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    public Animator animator;
    public Transform jugador;
    public float distanciaMoverse = 20f;
    public float distanciaAtacar = 15f;
    public int dañoAtaque = 40;
    public float tiempoSiguienteAtaque = 2f;
    public float velocidadAtaques = 1f;
    public Transform puntoAtaque;
    public Rigidbody2D rb;
    public float velocidad = 5f;
    public bool estaGirado = false;
    public int vidaActual;
    public float rangoAtaque =2f;
    public LayerMask capaJugador;
    public bool esqueleto = false;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int dañoRecibido)
    {
        vidaActual = vidaActual - dañoRecibido;
        //iniciar animacion dañado
        animator.SetTrigger("golpeado");

        if(vidaActual <= 0)
        {    
            Debug.Log("VidaActual: " + vidaActual);
            //Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, new Vector2(rb.position.x, rb.position.y - 0.79f), velocidad * Time.fixedDeltaTime);
            rb.gravityScale = 0;
            rb.inertia = 0;
            if (esqueleto)
            {
                rb.position = new Vector2(rb.position.x, rb.position.y - 0.79f);
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("estaMuerto", true);
            //rb.Sleep();
        }
    }

     void Muere()
    {
        //animacion muerte
        //Debug.Log("Esqueleto muerto");
        //desabilitar
        Debug.Log("ha muerto: ");
        animator.enabled = false;
        this.enabled=false;
    }

    private void Update()
    {
        if (vidaActual > 0)
        {
            MirarAlJugador();
            if (Vector2.Distance(jugador.position, transform.position) < distanciaMoverse)
            {
                if (Vector2.Distance(jugador.position, transform.position) < distanciaAtacar)
                {
                    Atacar();
                }
                else
                {
                    //moverse hacia jugador
                    Vector2 objetivo = new Vector2(jugador.position.x, rb.position.y);
                    Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, objetivo, velocidad * Time.fixedDeltaTime);
                    rb.MovePosition(nuevaPosicion);
                    animator.SetBool("caminando", true);
                }
            }
            else
            {
                animator.SetBool("caminando", false);
            }
        }
        
    }

    public void Atacar()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            tiempoSiguienteAtaque = Time.time + 1f / velocidadAtaques;
            //poner la animacion de atacar
            animator.SetTrigger("ataque");
        }
    }

    public void Ataque()
    {
        //Detecatar si hemos dado a un jugador a un cierto rango
        Collider2D jugadorTocado = Physics2D.OverlapCircle(puntoAtaque.position, rangoAtaque, capaJugador);

        //si ha tocado al jugador
        if (jugadorTocado != null)
        {
            //quitarle vida
            jugadorTocado.GetComponent<JugadorVida>().RecibirDaño(dañoAtaque);
        }
    }
    public void MirarAlJugador()
    {
        Vector3 girado = transform.localScale;
        girado.z *= -1f;

        if(transform.position.x > jugador.position.x && estaGirado)
        {
            transform.localScale = girado;
            transform.Rotate(0f, 180f, 0f);
            estaGirado = false;

        }else if (transform.position.x < jugador.position.x && !estaGirado)
        {
            transform.localScale = girado;
            transform.Rotate(0f, 180f, 0f);
            estaGirado = true;
        }
        {

        }

    }
}
