using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCombateControlador : MonoBehaviour

{
    public Animator animator;
    public Transform puntoAtaque;
    public float rangoAtaque;
    public LayerMask capaEnemigos;
    public int dañoAtaque = 40;
    public float velocidadAtaques = 2f;
    public float tiempoSiguienteAtaque=0f;
    float ataque = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Atacar();
        }
    }

    public void Atacar()
    {
        if (Time.time >= tiempoSiguienteAtaque && JugadorVida.GetVidaActual() > 0)
        {
            tiempoSiguienteAtaque = Time.time + 1f / velocidadAtaques;
            //poner la animacion de atacar
            animator.SetTrigger("atacando");
            animator.SetFloat("ataque", ataque);
            //controlar los 3 ataques
            ataque++;
            if (ataque > 2)
            {
                ataque = 0f;
            }
            //Detecatar si hemos dado a un enemigo a un cierto rango
            Collider2D[] enemigosTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, capaEnemigos);

            //quitar vida al enemigo atacado
            foreach (Collider2D enemigo in enemigosTocados)
            {
                //Debug.Log("Tocado");
                if (enemigo.tag == "Minotauro")
                {
                    enemigo.GetComponent<MinotauroControlador>().RecibirDaño(dañoAtaque);
                }
                else if (enemigo.tag == "Calavera")
                {
                    enemigo.GetComponent<CalaveraControlador>().RecibirDaño(dañoAtaque);
                }
                else if (enemigo.tag == "Enemigo")
                {
                    enemigo.GetComponent<enemigo>().RecibirDaño(dañoAtaque);
                }
                
            }
        }


    }
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);

    }

}
