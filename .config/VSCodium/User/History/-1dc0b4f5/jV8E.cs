/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Pipes;

namespace UCM.IAV.Movimiento
{
    public class Separacion : ComportamientoAgente
    {
        /// <summary>
        /// Separa al agente
        /// </summary>
        /// <returns></returns>

        // Entidades potenciales de las que huir
        public GameObject targEmpty;
        
        // Umbral en el que se activa
        [SerializeField]
        float umbral;

        [SerializeField] 
        private float maxAcceleration; 

        // Coeficiente de reducci�n de la fuerza de repulsi�n
        [SerializeField]
        float decayCoefficient;

        private List <GameObject>targets = new List<GameObject>();

        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            prioridad = 1;
            //Vaciamos la lista en caso de borrar ratas
            if(targets.Count > targEmpty.transform.childCount)
            {
                targets.Clear();
            }
            //Añadimos modificamos las ratas en función del total encontrado en el array princiapal de ratas
            targets.Add(objetivo);
            for (int i = 0; i < targEmpty.transform.childCount; i++)
            {
                if(targets.Count <= i)
                {
                    targets.Add(targEmpty.transform.GetChild(i).gameObject);
                }
                else
                {
                    targets[i] = targEmpty.transform.GetChild(i).gameObject;
                }
            }
            
            ComportamientoDireccion compDireccion = new ComportamientoDireccion();

            for (int i = 0; i < targets.Count; i++)
            {
                Vector3 dir = transform.position - targets[i].transform.position;
                
                if (dir.magnitude < umbral)
                {
                    prioridad = 0;
                    //inverse square law
                    float strength = Mathf.Min(decayCoefficient/(dir.magnitude*dir.magnitude), maxAcceleration);
                    dir.Normalize();
                    compDireccion.lineal += strength * dir;
                }
            }
            
            return compDireccion;
        }
    }
}