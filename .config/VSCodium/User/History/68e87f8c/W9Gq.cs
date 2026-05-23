/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public bool mino = false;
    void Start()
    {
       // Establece changeSize al OnValueChanged del Dropdown
        if(!mino)
            gameObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { UCM.IAV.Movimiento.GameManager.instance.ChangeSize(); });
       

    }
}
