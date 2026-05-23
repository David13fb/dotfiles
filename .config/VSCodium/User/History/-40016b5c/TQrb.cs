

namespace UCM.IAV.Movimiento
{
    using System.Collections.Generic;
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
        [SerializeField] private float tileDis = 1.0f;
        [SerializeField] LayerMask wallLayer;
        Direccion lastdir = new Direccion();
        private bool firstStep = true;
        DIR currentDir = DIR.NONE;

        float maxTime = 1.0f;
        float actualT = 0.0f;

        private void Start(){
            GetComponent<Rigidbody>().WakeUp();
        }
        public override Direccion GetDireccion()
        {
            if(actualT<0){

            Direccion direccion = new Direccion();

            // 1. Detectar opciones disponibles (Raycast en 4 direcciones)
               Debug.DrawRay(transform.position, new Vector3(-1,0,0) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(1,0,0) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,0,1) * tileDis, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,0,-1) * tileDis, Color.red);
            List<DIR> posDir = new List<DIR>();
            if (!Physics.Raycast(transform.position, Vector3.left, tileDis, wallLayer)) posDir.Add(DIR.LEFT);
            if (!Physics.Raycast(transform.position, Vector3.right, tileDis, wallLayer)) posDir.Add(DIR.RIGHT);
            if (!Physics.Raycast(transform.position, Vector3.forward, tileDis, wallLayer)) posDir.Add(DIR.UP);
            if (!Physics.Raycast(transform.position, Vector3.back, tileDis, wallLayer)) posDir.Add(DIR.DOWN);

            bool intersection = posDir.Count > 2;
            bool oneOps = posDir.Count == 1;
            bool changeDir = firstStep || intersection || oneOps;

            if (posDir.Count == 2 && !posDir.Contains(currentDir))
            {
                changeDir = true;
            }

            if (changeDir)
            {
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
            
            direccion.lineal = new Vector3(dir.x, 0, dir.y);
            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;
            lastdir = direccion;
            actualT = maxTime;
            return direccion;
            }
            else{
//                print(lastdir.lineal);
                actualT -= Time.deltaTime;
                return lastdir;
            }
        }
    }
}
