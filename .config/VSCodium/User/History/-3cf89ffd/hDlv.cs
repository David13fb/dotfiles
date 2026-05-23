using UnityEngine;

[CreateAssetMenu(fileName = "DebugBehaviour", menuName = "Scriptable Objects/beh/DebugBehaviour")]
public class DebugBehaviour : Ibehaviour
{
    override public bool Execute(){
        return true;
    }
}
