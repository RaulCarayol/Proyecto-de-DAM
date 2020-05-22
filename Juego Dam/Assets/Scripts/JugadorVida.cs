using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JugadorVida : MonoBehaviour
{
    public int vidaMaxima = 100;
    public Animator animator;
    public int vidaActual;
    public Slider barraDeVida;
    public GameManager gameManager;
    //public PolygonCollider2D limites;
    
    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
        animator.SetBool("estaMuerto", false);
    }

    // Update is called once per frame
    void Update()
    {
        barraDeVida.value = vidaActual;
    }

    public void RecibirDaño(int daño)
    {
        animator.SetTrigger("golpeado");
        vidaActual -= daño;
        if(vidaActual <= 0)
        {
            //Morir();
            animator.SetBool("estaMuerto", true);
        }
    }

     public void Morir()
    {
        gameManager.GameOver();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
