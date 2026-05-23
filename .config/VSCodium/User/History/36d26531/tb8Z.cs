using UnityEngine;
using UnityEngine.LightTransport;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class SeguimientoPrediccion : Seguimiento
    {
        public const float futurePosTime = 3;

        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion compDireccion = new ComportamientoDireccion();

            compDireccion.lineal = target.position + (target.forward * (futurePosTime * target.GetComponent<Rigidbody>().linearVelocity.magnitude)) - transform.position;

            compDireccion.lineal.Normalize();
            compDireccion.lineal *= maxAccel;
            compDireccion.angular = 0;
            
            return compDireccion;
        }
    }
}