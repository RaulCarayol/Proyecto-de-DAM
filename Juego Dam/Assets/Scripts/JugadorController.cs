using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JugadorController : MonoBehaviour
{

     public float FuerzaSalto = 500f;                         
    public float velocidadAgacharse = .36f;         
    public float suavidadMovimiento = .05f; 
     public bool controlEnAire = true;                        
    public LayerMask sueloParaElPersonaje;                        
     public Transform sueloCheck;                           
    public Transform techoCheck;                          
     public Collider2D agacharseDisableCollider;              

    const float radioSuelo = .4f;
    private bool estabaSuelo;            
    const float techoRadio = .2f; 
    private Rigidbody2D rb;
    private bool mirandoBien = true; 
    private Vector3 velocidad = Vector3.zero;


    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_estabaAgachado = false;

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
        bool m_estabaEnSuelo = estabaSuelo;
        estabaSuelo = false;

        //Mirar las colisiones con el suelo
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sueloCheck.position, radioSuelo, sueloParaElPersonaje);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            { 
                estabaSuelo = true;
                if (!m_estabaEnSuelo)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Mover(float movimiento, bool agachado, bool salto)
    {
        if (!agachado)
        {
            // mirar si hay techo para que siga agachado
            if (Physics2D.OverlapCircle(techoCheck.position, techoRadio, sueloParaElPersonaje))
            {
                agachado = true;
            }
        }

        if (estabaSuelo || controlEnAire)
        {

            // si esta agachado
            if (agachado)
            {
                if (!m_estabaAgachado)
                {
                    m_estabaAgachado = true;
                    OnCrouchEvent.Invoke(true);
                }
                // velocidad reducida cuando esta agachado
                movimiento *= velocidadAgacharse;

                // quitar un collider cuando esta agachado para poder pasar por sitios
                if (agacharseDisableCollider != null)
                    agacharseDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (agacharseDisableCollider != null)
                { agacharseDisableCollider.enabled = true; }

                if (m_estabaAgachado)
                {
                    m_estabaAgachado = false;
                    OnCrouchEvent.Invoke(false);
                }

            }

            Vector3 velocidadObjetivo = new Vector2(movimiento * 10f, rb.velocity.y);
            // suavidad
            rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavidadMovimiento);

            //si se quiere mover derecha y esta mirando  izquierda
            if (movimiento > 0 && !mirandoBien)
            {
                Girar();
            }
            // si se quiere mover izquierda y esta mirando  derecha
            else if (movimiento < 0 && mirandoBien)
            {
                Girar();
            }
        }
        
        if (estabaSuelo &&   salto)
        {
            //añadiendo fuerza vertical
            estabaSuelo = false;
            rb.AddForce(Vector3.up * FuerzaSalto);
        }
    }


    private void Girar()
    {

        mirandoBien = !mirandoBien;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
