using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    public JugadorController controlador;
    public Animator animator;
    public Joystick joystick;
    public float velocidadCorrer = 40f;
    float movimientoHorizontal;
    float ataque = 0f;
    bool saltar=false;
    bool agacharse = false;
   


    // Update is called once per frame
    /// <summary>
    /// #if (UNITY_ANDROID)
  //  movimientoHorizontal = joystick.Horizontal* velocidadCorrer;
//#elif UNITY_STANDALONE_WIN
//    movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadCorrer;
//#endif
    /// </summary>
    void Update()
    {
        //movimiento horizontal
        if(joystick.Horizontal >= .2f)
        {
            movimientoHorizontal = velocidadCorrer;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            movimientoHorizontal = -velocidadCorrer;
        }
        else
        {
            movimientoHorizontal = 0f;
        }
        animator.SetFloat("velocidad", Mathf.Abs(movimientoHorizontal));

        //salto joystick
        if (joystick.Vertical >= .62f)
        {
            saltar = true;
            animator.SetBool("saltando", true);
        }
        else if (joystick.Vertical <= -.5f)
        {
            agacharse = true;
            animator.SetBool("saltando", false);
        }
        else
        {
            agacharse = false;
        }
        
        /*
        if (Input.GetButtonDown("Jump"))
        {
            saltar = true;
            animator.SetBool("saltando", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            agacharse = true;
           
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            agacharse = false;
        }
        */

    }
    public void OnLanding()
    {
        animator.SetBool("saltando", false);
    }

    public void OnCrouching(bool estaAgachado)
    {
       // animator.SetBool("agachado", estaAgachado);
    }
    public void Atacar()
    {
        animator.SetTrigger("atacando");
        animator.SetFloat("ataque",ataque);
        ataque++;
        if (ataque > 2)
        {
            ataque = 0f;
        }
        //Debug.Log("Atacado Lanzado");
        
    }

    private void FixedUpdate()
    {
        controlador.Mover(movimientoHorizontal * Time.fixedDeltaTime, agacharse, saltar);
        saltar = false;
    }
}
