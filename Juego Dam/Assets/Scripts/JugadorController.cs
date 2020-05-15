﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JugadorController : MonoBehaviour
{

    [SerializeField] private float m_FuerzaSalto = 600f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_velocidadAgacharse = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_SuavidadMovimiento = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_ControlEnAire = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] public LayerMask m_SueloParaElPersonaje;                          // A mask determining what is ground to the character
    [SerializeField] public Transform m_SueloCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] public Transform m_TechoCheck;                          // A position marking where to check for ceilings
    [SerializeField] public Collider2D m_AgacharseDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .4f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_estabaAgachado = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }


    private void FixedUpdate()
    {
        bool m_estabaEnSuelo = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_SueloCheck.position, k_GroundedRadius, m_SueloParaElPersonaje);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            { 
                m_Grounded = true;
                if (!m_estabaEnSuelo)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Mover(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_TechoCheck.position, k_CeilingRadius, m_SueloParaElPersonaje))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_ControlEnAire)
        {

            // If crouching
            if (crouch)
            {
                if (!m_estabaAgachado)
                {
                    m_estabaAgachado = true;
                    OnCrouchEvent.Invoke(true);
                }
                // Reduce the speed by the crouchSpeed multiplier
                move *= m_velocidadAgacharse;

                // Disable one of the colliders when crouching
                if (m_AgacharseDisableCollider != null)
                    m_AgacharseDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_AgacharseDisableCollider != null)
                { m_AgacharseDisableCollider.enabled = true; }

                if (m_estabaAgachado)
                {
                    m_estabaAgachado = false;
                    OnCrouchEvent.Invoke(false);
                }

            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_SuavidadMovimiento);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        //Debug.Log(jump+""+m_Grounded);
        // If the player should jump...
        if (m_Grounded &&   jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(Vector3.up * m_FuerzaSalto);
            //Debug.Log("saltado");
            //jump = false;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
