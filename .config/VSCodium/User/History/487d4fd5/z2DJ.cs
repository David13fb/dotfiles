
using UnityEngine;
using UnityEngine.UIElements;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Perseguir : ComportamientoAgente
    {    
        private void Start(){
            GetComponent<Rigidbody>().WakeUp();
            print("Recien Levantadito");
        }
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            direccion.lineal = objetivo.transform.position-transform.position;
            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;
            return direccion;
        }

    }
}
