using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Seguimiento : ComportamientoAgente
    {
        public Transform target;
        public float maxAccel;

        private void Start(){
            target = GestorJuego.Instantiate().GetPlayerTransform();

        }
        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion compDireccion = new ComportamientoDireccion();

            compDireccion.lineal = target.position - transform.position;

            compDireccion.lineal.Normalize();
            compDireccion.lineal *= maxAccel;
            compDireccion.angular = 0;
            
            return compDireccion;
        }
    }
}