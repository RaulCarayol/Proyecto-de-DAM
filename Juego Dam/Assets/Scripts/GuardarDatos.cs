
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GuardarDatos {
 
    public static void GuardarJugador(JugadorVida jugador,
            GameManager gameManager, string nivel)
    {
        //creamos el encargado de transformar los datos en binario
        BinaryFormatter formateador = new BinaryFormatter();
        //lugar donde se va a guardar los datos, usamos aplication.persistance
        //para que funcione en cualquier sistema y no solo en pc o movil
        string localizacion = Application.persistentDataPath + "/jugador.dam";
        //hace falta pasar la localizacion y el modo, en este caso Create porque vamos a guardar/crear el archivo
        FileStream stream = new FileStream(localizacion, FileMode.Create);
        //datos del jugador
        JugadorDatos datos = new JugadorDatos(jugador, gameManager, nivel);
        //guardamos los datos
        formateador.Serialize(stream, datos);
        //es una buena practica cerrar al final
        stream.Close();
        
    }

    public static JugadorDatos CargarDatosJugador()
    {
        string localizacion = Application.persistentDataPath + "/jugador.dam";
        //comprovar si existe el archivo
        if (File.Exists(localizacion))
        {
            BinaryFormatter formateador = new BinaryFormatter();

            //hace falta pasar la localizacion y el modo, en este caso Open para abrir el archivo
            FileStream stream = new FileStream(localizacion, FileMode.Open);
            //datos del jugador
            JugadorDatos datos = formateador.Deserialize(stream) as JugadorDatos;
            stream.Close();

            return datos;
        }
        else
        {
            Debug.LogWarning("No se ha encontrado el archivo de datos del jugador");
            return null;
        }
    }
}
