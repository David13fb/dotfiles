
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Llegada : ComportamientoAgente
    {        
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            return direccion;
           
        }

    }
}
