using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OpcionesDatos
{
    //datos que queremos guardar
    public float escalaJoystick;
    public float escalaBotonAtaque;

    public OpcionesDatos (OpcionesMenu opcionesMenu)
    {
        this.escalaBotonAtaque = opcionesMenu.escalaBotonAtaque;
        this.escalaJoystick = opcionesMenu.escalaJoystick;
    }

}
