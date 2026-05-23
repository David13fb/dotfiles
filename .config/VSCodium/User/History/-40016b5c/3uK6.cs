

namespace UCM.IAV.Movimiento
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UCM.IAV.Navegacion;
    using Unity.PlasticSCM.Editor.WebApi;
    using UnityEngine;

    public class Patrullar : ComportamientoAgente
    {

        private enum DIR
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            NONE
        }
        private DIR OpositeDir(DIR d)
        {
            DIR aux = DIR.UP;
            switch (d)
            {
                case DIR.UP:
                    aux = DIR.DOWN;
                    break;
                case DIR.LEFT:
                    aux = DIR.RIGHT;
                    break;
                case DIR.DOWN:
                    aux = DIR.UP;
                    break;
                case DIR.RIGHT:
                    aux = DIR.LEFT;
                    break;

            }
            return aux;
        }
        [SerializeField] private float tileDis = 0.75f;
        [SerializeField] LayerMask wallLayer;
        Direccion lastdir = new Direccion();
        private bool firstStep = true;
        DIR currentDir = DIR.NONE;

        float maxTime = 0.3f;
        float actualT = 0.3f;
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();

            // 1. Detectar opciones disponibles (Raycast en 4 direcciones)
            List<DIR> posDir = new List<DIR>();
            if (!Physics.Raycast(transform.position, Vector3.left, tileDis, wallLayer)) posDir.Add(DIR.LEFT);
            if (!Physics.Raycast(transform.position, Vector3.right, tileDis, wallLayer)) posDir.Add(DIR.RIGHT);
            if (!Physics.Raycast(transform.position, Vector3.forward, tileDis, wallLayer)) posDir.Add(DIR.UP);
            if (!Physics.Raycast(transform.position, Vector3.back, tileDis, wallLayer)) posDir.Add(DIR.DOWN);

            // 2. Lógica de decisión
            // Si es el primer paso, o estamos en una intersección (más de 2 caminos) 
            // o llegamos a un callejón sin salida (solo 1 camino).
            bool isIntersection = posDir.Count > 2;
            bool isDeadEnd = posDir.Count == 1;
            bool mustChoose = firstStep || isIntersection || isDeadEnd;

            // Si estamos en un pasillo (2 caminos), solo cambiamos si el actual está bloqueado
            if (posDir.Count == 2 && !posDir.Contains(currentDir))
            {
                mustChoose = true;
            }

            if (mustChoose)
            {
                // Para que no de la vuelta 180º en una intersección a menos que sea obligatorio
                if (posDir.Count > 1 && currentDir != DIR.NONE)
                {
                    posDir.Remove(OpositeDir(currentDir));
                }

                currentDir = posDir[UnityEngine.Random.Range(0, posDir.Count)];
                firstStep = false;
            }
            Vector2 dir = new Vector2();
            switch (currentDir)
            {
                case DIR.UP:
                dir = Vector2.up;
                break;
                 case DIR.DOWN:
                dir = Vector2.down;
                break;
                 case DIR.LEFT:
                dir = Vector2.left;
                break;
                 case DIR.RIGHT:
                dir = Vector2.right;
                break;
                
            }
            // 3. Aplicar movimiento
            Vector3 vDir = DirToVector(currentDir);
            direccion.lineal = vDir * agente.aceleracionMax;

            // Opcional: Orientar al agente hacia donde mira
            if (vDir != Vector3.zero) transform.forward = vDir;

            return direccion;
        }
    }
}
