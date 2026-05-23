using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "DebugBehaviour", menuName = "Scriptable Objects/beh/DebugBehaviour")]
public class DebugBehaviour : Ibehaviour
{
    override public bool Execute(){
        Debug.Log("Soy un estado");
        return true;
    }
}
