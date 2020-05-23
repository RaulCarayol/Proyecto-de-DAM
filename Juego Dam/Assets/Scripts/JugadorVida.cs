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
    bool estaMuerto = false;
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
        if (!estaMuerto)
        {
            animator.SetTrigger("golpeado");
        }
        vidaActual -= daño;
        if(vidaActual <= 0)
        {
            //Morir();
            estaMuerto = true;
            animator.SetBool("estaMuerto", true);
        }
    }

    public void RecibirVida(int vidaExtra)
    {
        vidaActual += vidaExtra;
    }

    public void Morir()
    {
        gameManager.GameOver();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
