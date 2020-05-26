using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    //Creamos una lista de elementos (ya que la pared contiene varios objetos que la hacen)
    public SpriteRenderer[] wallElements;
    float alphaValue = 1f;

    //Controlara la velocidad en la que desaparece la pared
    public float disappearRate = 1f;
    //Controlara si el personaje esta dentro del area
    bool playerEntered = false;

    //Controlara si el jugador esta dentro o fuera del area
    public bool toggleWall = false;
        
    void Update()
    {
        if (playerEntered)
        {
            //Restara el valor de alpha para que desaparezca la pared de forma progresiva
            alphaValue -= Time.deltaTime * disappearRate;

            //Para que no pueda ser menor de zero
            if(alphaValue <= 0)
            {
                alphaValue = 0;
            }

            //Dejaremos los colores iguales y cambiaremos el valor alpha de todos los objeos de la pared
            foreach (SpriteRenderer wallItem in wallElements)
            {
                
                wallItem.color = new Color(wallItem.color.r, wallItem.color.g, wallItem.color.b, alphaValue);
            }
        }
        else
        {
            //Sumara el valor de alpha para que aparezca la pared de forma progresiva
            alphaValue += Time.deltaTime * disappearRate;
            
            if (alphaValue >= 1)
            {
                alphaValue = 1;
            }

            foreach (SpriteRenderer wallItem in wallElements)
            {

                wallItem.color = new Color(wallItem.color.r, wallItem.color.g, wallItem.color.b, alphaValue);
            }
        }
        
    }

    //Funcion que te dice si esta el jugador dentro del area
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col2)
    {
        if(col2.CompareTag("Player") && toggleWall)
        {
            playerEntered = false;
        }


    
        
    }
}
