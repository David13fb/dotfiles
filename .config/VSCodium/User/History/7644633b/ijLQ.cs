/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de WANDER a otro agente
    /// </summary>
    public class Merodear : ComportamientoAgente
    {
        [SerializeField]
        float tiempoMaximo = 2.0f;

        [SerializeField]
        float tiempoMinimo = 1.0f;

        float t = 3.0f;
        float actualT = 2.0f;

        ComportamientoDireccion lastDir = new ComportamientoDireccion();

        public override ComportamientoDireccion GetComportamientoDireccion(){
            
            print("MELLAMO");
            actualT -= Time.deltaTime;
            if(actualT<tiempoMinimo){
                //Cambiamos direccion
                actualT = tiempoMaximo;
                ComportamientoDireccion aux = new ComportamientoDireccion();
                
                aux.lineal = new Vector3(Random.Range(-1, 1) ,0,Random.Range(-1, 1));
                aux.lineal.Normalize();
                lastDir = aux;
                return lastDir;
            }  
            else{
                return lastDir;
            }
            
        }

    }
}
