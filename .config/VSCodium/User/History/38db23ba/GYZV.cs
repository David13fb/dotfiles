
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Girar : ComportamientoAgente
    {
        [SerializeField]
        float maxTime = 2.0f;

        [SerializeField]
        float minTime = 1.0f;

        float t = 3.0f;
        float actualT = 2.0f;

        Direccion lastDir = new Direccion();

        public override Direccion GetDireccion(){
            if (t >= actualT)
            {
                Direccion direccion = new Direccion();

                Vector2 dir = Random.insideUnitCircle.normalized;

                direccion.lineal = new Vector3(dir.x, 0, dir.y);
                direccion.lineal.Normalize();
               // direccion.lineal *= agente.aceleracionMax;

                lastDir = direccion;

                actualT = Random.Range(minTime, maxTime);

                t = 0.0f;
            }
            else{
                t += Time.deltaTime;
            }

            return lastDir;
        }
}
