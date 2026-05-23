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
using UnityEngine.Scripting.APIUpdating;
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

        bool _move = false;
        ComportamientoDireccion lastDir = new ComportamientoDireccion();

        public override ComportamientoDireccion GetComportamientoDireccion(){
            
            if(GestorJuego.instance.IsPlayingFlute()){
                prioridad = 0;
            }
            else prioridad = 3;
            
            actualT -= Time.deltaTime;
            if(actualT<tiempoMinimo){
                //Cambiamos direccion
                _move = !_move;
                actualT = tiempoMaximo;
                ComportamientoDireccion aux = new ComportamientoDireccion();
                if(_move){
                    aux.lineal = new Vector3(Random.Range(-1.0f, 1.0f) ,0,Random.Range(-1.0f, 1.0f));
                    aux.lineal.Normalize();
                    

                }
                else aux.lineal = new Vector3(0,0,0);
                lastDir = aux;
                return lastDir;
            }  
            else{
                return lastDir;
            }
            
        }

    }
}
