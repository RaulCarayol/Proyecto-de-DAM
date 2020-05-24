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
    bool estaMuerto;
    //public PolygonCollider2D limites;
    
    // Start is called before the first frame update
    void Start()
    {
        estaMuerto = false;
        vidaActual = vidaMaxima;
        animator.SetBool("estaMuerto", false);
        barraDeVida.maxValue = vidaMaxima;
    }
    // Update is called once per frame
    void Update()
    {
        barraDeVida.value = vidaActual;
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        if (vidaActual <= 0)
        {
            animator.SetBool("estaMuerto", true);
        }
        if (!estaMuerto)
        {
            animator.SetTrigger("golpeado");
        }

    }

    public void RecibirVida(int vida)
    {
        vidaActual += vida;
    }

     public void Morir()
    {
        gameManager.GameOver();
    }
}
