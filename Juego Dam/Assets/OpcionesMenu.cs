using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesMenu : MonoBehaviour
{
    public FixedJoystick joystick;
    public Button botonAtaque;
    public Slider sliderJoystick;
    public Slider sliderBotonAtaque;

    //datos de opciones
    public float escalaJoystick = 1;
    public float escalaBotonAtaque = 1;

    // Start is called before the first frame update
    void Start()
    {
        OpcionesDatos opcionesDatos=GuardarDatos.CargarDatosOpciones();
        if(opcionesDatos!= null)
        {
            //recuperar las escalas
            escalaJoystick = opcionesDatos.escalaJoystick;
            escalaBotonAtaque = opcionesDatos.escalaBotonAtaque;
            //asignar los valores a los sliders
            sliderJoystick.value = escalaJoystick;
            sliderBotonAtaque.value = escalaBotonAtaque;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        GuardarDatos.GuardarOpciones(this);
    }

    public void EscalaJoystickCambiada()
    {
        escalaJoystick = sliderJoystick.value;
        joystick.transform.localScale = Vector3.one * escalaJoystick;
        
    }
    public void EscalaBotonCambiada()
    {
        escalaBotonAtaque = sliderBotonAtaque.value;
        botonAtaque.transform.localScale = Vector3.one * escalaBotonAtaque;
    }
}
