using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JugadorController : MonoBehaviour
{

     public float fuerzaSalto = 600f;                      
     public float velocidadAgacharse = .36f;         
     public float suavidadMovimiento = .05f; 
     public bool controlEnAire = true;                   
     public LayerMask sueloParaElPersonaje;                        
     public Transform sueloCheck;                           
     public Transform techoCheck;                          
     public Collider2D agacharseDisableCollider;     

    const float radioEnElSuelo = .4f; 
    private bool estaEnElSuelo; 
    const float radioTecho = .2f; 
    public Rigidbody2D rb;
    private bool estaMirandoBien = true;
    private Vector3 velocity = Vector3.zero;

    //evento para avisar cuando llega al suelo despues de saltar
    public UnityEvent OnLandEvent;


    public class BoolEvent : UnityEvent<bool> { }
    //evento para decir cuando esta agachado
    public BoolEvent OnCrouchEvent;
    private bool estabaAgachado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }


    private void FixedUpdate()
    {
        bool estabaEnSuelo = estaEnElSuelo;
        estaEnElSuelo = false;

        //mirar si esta en el suelo cuando el suelo check en un radio estipulado (radioEnElSuelo) toca algo desigando como suelo para el jugador
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sueloCheck.position, radioEnElSuelo, sueloParaElPersonaje);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            { 
                estaEnElSuelo = true;
                //si no estaba en el suelo
                if (!estabaEnSuelo)
                {
                    //avisar que esta en el suelo (JugadorMovimiento)
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Mover(float movimiento, bool agacharse, bool saltar)
    {
        // si esta agachado, mirar si se puede levantar
        if (!agacharse)
        {
            // si esta agachjado y tiene algo encima, manterlo agachado
            if (Physics2D.OverlapCircle(techoCheck.position, radioTecho, sueloParaElPersonaje))
            {
                agacharse = true;
            }
        }

        //solo si esta en el suelo o tiene control en el aire
        if (estaEnElSuelo || controlEnAire)
        {

            // si esta agachado
            if (agacharse)
            {
                if (!estabaAgachado)
                {
                    estabaAgachado = true;
                    //avisar que esta agachado
                    OnCrouchEvent.Invoke(true);
                }
                // reducir la velocidad cuando esta agachado 
                movimiento *= velocidadAgacharse;

                // desabilitar un collider mientras esta agachado
                if (agacharseDisableCollider != null)
                    agacharseDisableCollider.enabled = false;
            }
            else
            {
                // volver activar el collider cuando no esta agachado
                if (agacharseDisableCollider != null)
                { agacharseDisableCollider.enabled = true; }

                if (estabaAgachado)
                {
                    estabaAgachado = false;
                    OnCrouchEvent.Invoke(false);
                }

            }

            Vector3 velocidadObjetivo = new Vector2(movimiento * 10f, rb.velocity.y);
            // aplicar la velocidad al jugador con un factor de suavidad
            rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocity, suavidadMovimiento);

            // si el jugador se mueve a la derecha y esta mirando a la izquierda
            if (movimiento > 0 && !estaMirandoBien)
            {
                Girar();
            }
            // tambien hay que girar al jugador cuando se mueve a la izquierda y mira a la derecha
            else if (movimiento < 0 && estaMirandoBien)
            {
                // ... flip the player.
                Girar();
            }
        }

        // If the player should jump...
        if (estaEnElSuelo &&   saltar)
        {
            // Add a vertical force to the player.
            estaEnElSuelo = false;
            rb.AddForce(Vector3.up * fuerzaSalto);
            //Debug.Log("saltado");
            //jump = false;
        }
    }


    private void Girar()
    {
        //cambiar el estado de mirando bien cuando se gira
        estaMirandoBien = !estaMirandoBien;
        //girando multiplicando por -1 la escala de x pàra que se gire horizontalmente
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
