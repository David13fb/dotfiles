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
        private Transform mplayerTrans;
        private Transform mtrans;

        [SerializeField] float radioHuida = 10.0f;

        [SerializeField] int limiteRatas = 3;
    
        [SerializeField] GameObject ratasArray;
 // Start is called before the first frame update
        void Start()
        {
            mplayerTrans = objetivo.transform;
            mtrans = transform;
        }
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            int actnumRats = 0;
            prioridad = 3;
            foreach(Transform t in ratasArray.GetComponentsInChildren<Transform>()){
                print(Vector3.Distance(mtrans.position,t.position));
                if(radioHuida > Vector3.Distance(mtrans.position,t.position)) actnumRats++;
                if(limiteRatas == actnumRats){
                    prioridad = 0;
                    print("Me sirve");
                    break;
                }
            }
            ComportamientoDireccion aux = new ComportamientoDireccion();
            // IMPLEMENTAR HUIR
            aux.lineal = mplayerTrans.position-mtrans.position;
            aux.lineal.Normalize();
            aux.lineal*=-1;
            return aux;
        }
    }
}
