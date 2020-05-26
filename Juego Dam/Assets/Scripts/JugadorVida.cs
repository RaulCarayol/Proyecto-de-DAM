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
    public static int vida;
    public Slider barraDeVida;
    public GameManager gameManager;
    bool estaMuerto;
    bool todaviaNoSeHaMuerto = false;

    public static int GetVidaActual()
    {
        return vida;
    }

    //public PolygonCollider2D limites;

    // Start is called before the first frame update
    void Start()
    {
        estaMuerto = false;
        vidaActual = vidaMaxima;
        vida = vidaActual;
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
        vida = vidaActual;
        if (!estaMuerto)
        {
            if (vidaActual <= 0)
            {
                animator.SetBool("estaMuerto", true);
                estaMuerto = true;
            }
            animator.SetTrigger("golpeado");
        }
        if(estaMuerto && !todaviaNoSeHaMuerto)
        {
            animator.SetTrigger("morirse");
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
