using System.ComponentModel.Design.Serialization;
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
        private void Start(){
            target = GestorJuego.instance.GetPlayerTransform();
            prioridad = 2;
        }
        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion compDireccion = new ComportamientoDireccion();

            compDireccion.lineal = target.position - transform.position;

            compDireccion.lineal.Normalize();
            compDireccion.lineal *= agente.aceleracionMax;
            compDireccion.lineal *= -1;
            compDireccion.angular = 0;
            
            return compDireccion;
        }
    }
}