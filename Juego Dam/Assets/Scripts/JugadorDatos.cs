using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class JugadorDatos
{
    //datos que queremos guardar
    public int monedas;
    public int vida;
    public int vidaMaxima;
    public float[] ultimoCheckPoint;
    public string ultimoNivel;

    public JugadorDatos(JugadorVida jugador, GameManager gameManager,string nivel)
    {
        monedas = gameManager.monedas;
        vida = jugador.vidaActual;
        vidaMaxima = jugador.vidaMaxima;
        ultimoNivel = nivel;

        ultimoCheckPoint = new float[3];
        ultimoCheckPoint[0] = gameManager.checkpoint.x;
        ultimoCheckPoint[1] = gameManager.checkpoint.y;
        ultimoCheckPoint[2] = gameManager.checkpoint.z;
    }

}
