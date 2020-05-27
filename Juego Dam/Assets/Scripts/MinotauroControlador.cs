using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinotauroControlador : MonoBehaviour
{
    public int vidaMaxima = 400;
    public Animator animator;
     
    public float distanciaAtacar = 6f;
    public int dañoAtaque = 30;
     
    public float rapidezBarrido = 1f;
    public float velocidadAtaques = 1f;
    public float veolicidadBarrido = 10f;
    public Transform puntoAtaque;
    public Rigidbody2D rb;
    public float velocidad = 5f;
    public bool estaGirado = false;
    public int vidaActual;
    public float rangoAtaque = 2f;
    public LayerMask capaJugador;
    public LayerMask suelo;
    public GameObject slider;
    
    float tiempoSiguienteAtaque = 2f;
    Transform jugador;
    bool sliderActivado = false;
    bool modoMuerte = false;
    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
        jugador = GameObject.FindWithTag("Player").transform;
        if (slider != null)
        {
            slider.GetComponent<Slider>().maxValue = vidaMaxima;
        }
    }

    public void RecibirDaño(int dañoRecibido)
    {
        if (vidaActual <= vidaMaxima / 2)
        {
            vidaActual = vidaActual - (int)(dañoRecibido*0.5);
        }
        else
        {
            vidaActual = vidaActual - dañoRecibido;
        }
        if (slider != null)
        {
            slider.GetComponent<Slider>().value = vidaActual;
        }
        //iniciar animacion dañado
        animator.SetTrigger("golpeado");

        if (vidaActual <= 0)
        {
            Debug.Log("VidaActual: " + vidaActual);
            //Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, new Vector2(rb.position.x, rb.position.y - 0.79f), velocidad * Time.fixedDeltaTime);
            rb.gravityScale = 0;
            rb.inertia = 0;
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("estaMuerto", true);
            //rb.Sleep();
        }
    }

    void Muere()
    {
        if (sliderActivado)
        {
            slider.SetActive(false);
            sliderActivado = false;
        }
        //animacion muerte
        //Debug.Log("Esqueleto muerto");
        //desabilitar
        rb.velocity = Vector3.zero;
        Debug.Log("ha muerto: ");
        animator.enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }

    private void Update()
    {
        Debug.Log(Vector2.Distance(jugador.position, transform.position));
        if (Vector2.Distance(jugador.position, transform.position) < 38)
        {
            if (!sliderActivado && slider !=null)
            {
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 11;
                slider.SetActive(true);
                sliderActivado = true;
                slider.GetComponent<Slider>().value = vidaActual;
            }
            if (vidaActual > 0)
            {
                MirarAlJugador();
                if (Vector2.Distance(jugador.position, transform.position) < distanciaAtacar)
                {
                    animator.SetBool("barrido", false);
                    rb.inertia = 0;
                    Atacar();
                }
                else
                {
                    Barrido2();

                }
                if (!modoMuerte && vidaActual <= vidaMaxima / 2)
                {
                    modoMuerte = true;
                    rb.transform.localScale = new Vector3(rb.transform.localScale.x * 1.2f,
                        rb.transform.localScale.y * 1.2f,
                        rb.transform.localScale.z * 1.2f);
                    velocidadAtaques *= 2;
                    dañoAtaque *= 2;
                    this.GetComponent<Renderer>().material.color = Color.red;
                }

            }
        }
        else
        {
            if (sliderActivado && slider != null)
            {
                slider.SetActive(false);
                sliderActivado = false;
            }
        }

    }
    /*
    void Barrido()
    {
        if (Time.time >= tiempoSiguienteBarrido)
        {
            if (!barrido)
            {
                tiempoSiguienteBarrido = Time.time + 1f / rapidezBarrido;

                if (jugador.position.x - rb.position.x < 0)
                {
                    x = -40;
                }
                else
                {
                    x = 40;
                }
                Debug.Log(x);
                objetivo = new Vector2((rb.position.x + x), rb.position.y);
                nuevaPosicion = Vector2.MoveTowards(rb.position, objetivo, velocidad * Time.fixedDeltaTime);
                //rb.MovePosition(nuevaPosicion);

                animator.SetBool("barrido", true);
                barrido = true;
            }

            if (barrido && !chocado)
            {

                rb.MovePosition(nuevaPosicion);
                animator.SetBool("barrido", true);
                if (rb.position.x <= muro1x && x < 0)
                {
                    Debug.Log(x);
                    chocado = true;
                    barrido = false;
                    animator.SetBool("barrido", false);
                }
                else if (rb.position.x >= muro1x && x > 0)
                {
                    chocado = true;
                    barrido = false;
                    animator.SetBool("barrido", false);
                }
            }
            else { animator.SetBool("barrido", false); }


        }
    }
    */
    void Barrido2()
    {

        Vector2 objetivo = new Vector2(jugador.position.x, rb.position.y);
        Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, objetivo, velocidad * Time.fixedDeltaTime);
        rb.MovePosition(nuevaPosicion);
        if (jugador.position.x - rb.position.x < 0)
                {

                    rb.AddForce(Vector2.left * veolicidadBarrido);
                    //rb.AddRelativeForce(Vector2.left * veolicidadBarrido);
                }
                else
                {
                    rb.AddForce(Vector2.right * veolicidadBarrido);
                     //rb.AddRelativeForce(Vector2.right * veolicidadBarrido);
                     //
                }
                animator.SetBool("barrido", true);

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
        int daño = dañoAtaque;
        //Detecatar si hemos dado a un jugador a un cierto rango
        Collider2D jugadorTocado = Physics2D.OverlapCircle(puntoAtaque.position, rangoAtaque, capaJugador);

        //si ha tocado al jugador
        if (jugadorTocado != null)
        {
            //quitarle vida
            jugadorTocado.GetComponent<JugadorVida>().RecibirDaño(daño);
        }
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
}