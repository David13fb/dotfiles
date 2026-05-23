

namespace UCM.IAV.Movimiento
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UCM.IAV.Navegacion;
    using UnityEngine;

    public class Patrullar: ComportamientoAgente
    {
        
        private enum DIR{
        LEFT,
        RIGHT,
        UP,
        DOWN
        }
        private DIR OpositeDir(DIR d){
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
        [SerializeField] private float tileDis = 1.5f;
        [SerializeField]LayerMask wallLayer;
        Direccion lastdir = new Direccion();
        public override Direccion GetDireccion()
        {
            
                
            Direccion direccion = new Direccion();
                //Check the possible dirs
                List<DIR> posDir = new List<DIR>();
                //left
                Debug.DrawRay(transform.position, new Vector3(-1,0,0) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(1,0,0) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,0,1) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,0,-1) * tileDis, Color.red);
                if(!Physics.Raycast(transform.position,new Vector3(-1,0,0),tileDis, wallLayer)){
                posDir.Add(DIR.LEFT);
                }
                //right
                if(!Physics.Raycast(transform.position,new Vector3(1,0,0),tileDis, wallLayer)) {
                posDir.Add(DIR.RIGHT);
                
                }
                if(!Physics.Raycast(transform.position,new Vector3(0,0,1), tileDis, wallLayer)) {
                posDir.Add(DIR.UP);
                }
                if (!Physics.Raycast(transform.position,new Vector3(0,0,-1),tileDis, wallLayer)){
                    posDir.Add(DIR.DOWN);
                }
                print(posDir.Count);
                if(posDir.Count == 2){
                    if(posDir[0] == OpositeDir(posDir[1])&&lastdir != new Direccion()){
                        return lastdir;
                    }
                }
                if(posDir.Count == 4) return lastdir;
                DIR finalDir = posDir[UnityEngine.Random.Range(0, posDir.Count)];
                Vector2 dir = new Vector2(1,0);
                switch(finalDir) 
                {
                    case DIR.UP:
                    dir = new Vector2(0,1);
                    break;
                    case DIR.RIGHT:
                    dir = new Vector2(1,0);
                    break;
                    case DIR.LEFT:
                    dir = new Vector2(-1,0);
                    break;
                    case DIR.DOWN:
                    dir = new Vector2(0,-1);
                    break;
                }
                
                direccion.lineal = new Vector3(dir.x, 0, dir.y);
                direccion.lineal.Normalize();
                direccion.lineal *= agente.aceleracionMax;
                
            // Podr�amos meter una rotaci�n autom�tica en la direcci�n del movimiento, si quisi�ramos

            return direccion;
        }
    }
}
