using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModoInfinito : MonoBehaviour
{
    public GameObject[] objects;
    public float tiempoAparicion = 3f;
    private Vector3 posicionAparicion;
    public float velocidadSlimes = 0.5f;
    public GameObject slime;
    public float tiempoEsqueltos = 10f;
    public float velocidadEsqueleto = 0.5f;
    public GameObject esqueleto;
    public float tiempoBrujos = 20f;
    public float velocidadBrujos = 0.5f;
    public GameObject brujo;
    public float tiempoMinotauros = 30f;
    public float velocidadMinotauros = 0.5f;
    public GameObject minotauro;
    float tiempoSiguienteSlime=0;
    float tiempoSiguienteEsqueleto = 0;
    float tiempoSiguienteBrujo = 0;
    float tiempoSiguienteMinotauro = 0;
    public float distanciaMinimaAparicionRespectoAlJugador = 6;
    public float tiempoCorazones = 40f;
    public float velocidadCorazones = 0.1f;
    float tiempoSiguienteCorazon = 0;
    public GameObject corazon;
    Transform jugador;
    public Text textoTiempo;
    // Use this for initialization
    void Start()
    {
        textoTiempo.text = Time.timeSinceLevelLoad.ToString("0.0");
        jugador = GameObject.FindWithTag("Player").transform;
    }

    void Aparecer(GameObject enemigo)
    {
        bool posicionCorrecta = false;
        while (!posicionCorrecta)
        {
            posicionAparicion.x = Random.Range(-17, 17);
            if (enemigo.tag == "Minotauro")
            {
                posicionAparicion.y = -6;
            }
            else
            {
                posicionAparicion.y = -11;
            }
            posicionAparicion.y = -12;
            posicionAparicion.z = 0;
            //comprovar que no se aparezcan donde esta el jugador o muy cerca
            if (Vector3.Distance(jugador.position, posicionAparicion) > distanciaMinimaAparicionRespectoAlJugador)
            {
                posicionCorrecta = true;
            }
        }
        Instantiate(enemigo, posicionAparicion, Quaternion.identity);
    }
    void Update()
    {
        textoTiempo.text = Time.timeSinceLevelLoad.ToString("0.0");
        if (Time.timeSinceLevelLoad >= tiempoSiguienteSlime && velocidadSlimes>0)
        {
            tiempoSiguienteSlime = Time.timeSinceLevelLoad + 1f / velocidadSlimes;
            Aparecer(slime);
           
        }
        
        if (Time.timeSinceLevelLoad >= tiempoEsqueltos)
        {
            if (Time.timeSinceLevelLoad >= tiempoSiguienteEsqueleto && velocidadEsqueleto > 0)
            {
                tiempoSiguienteEsqueleto = Time.timeSinceLevelLoad + 1f / velocidadEsqueleto;
                Aparecer(esqueleto);
                velocidadSlimes -= 0.03f;
            }
        }
        if (Time.timeSinceLevelLoad >= tiempoBrujos)
        {
            if (Time.timeSinceLevelLoad >= tiempoSiguienteBrujo && velocidadBrujos > 0)
            {
                tiempoSiguienteBrujo = Time.timeSinceLevelLoad + 1f / velocidadBrujos;
                Aparecer(brujo);
               
            }
        }
        if (Time.timeSinceLevelLoad >= tiempoMinotauros && velocidadMinotauros > 0)
        {
            if (Time.timeSinceLevelLoad >= tiempoSiguienteMinotauro)
            {
                tiempoSiguienteMinotauro = Time.timeSinceLevelLoad + 1f / velocidadMinotauros;
                Aparecer(minotauro);
                velocidadBrujos -= 0.001f;
                velocidadMinotauros += 0.002f;
            }
        }
        if (Time.timeSinceLevelLoad >= tiempoCorazones && velocidadCorazones > 0)
        {
            if (Time.timeSinceLevelLoad >= tiempoSiguienteCorazon)
            {
                tiempoSiguienteCorazon = Time.timeSinceLevelLoad + 1f / velocidadCorazones;
                Aparecer(corazon);
                velocidadCorazones += 0.005f;
            }
        }
    }
}
