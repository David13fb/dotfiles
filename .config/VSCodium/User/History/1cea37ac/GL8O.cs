/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using UnityEngine;
using UnityEngine.UIElements;

namespace UCM.IAV.Movimiento
{

    /// <summary>
    /// Clase para modelar el comportamiento de HUIR a otro agente
    /// </summary>
    public class Huir : ComportamientoAgente
    {
        [SerializeField] private Transform mplayerTrans;
        private Transform mtrans;
 // Start is called before the first frame update
        void Start()
        {
            mtrans = transform;
        }
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override ComportamientoDireccion GetComportamientoDireccion()
        {

            ComportamientoDireccion aux = new ComportamientoDireccion();
            // IMPLEMENTAR HUIR
            aux.lineal = mplayerTrans.position-mtrans.position;
            aux.lineal.Normalize();
            aux.lineal*=-1;
            return aux;
        }
    }
}
