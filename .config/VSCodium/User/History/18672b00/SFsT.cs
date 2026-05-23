using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "TrueCondition", menuName = "Scriptable Objects/Con/TrueCondition")]
public class TrueCondition : ICondition
{
     override public bool CheckCondition(){
        Debug.Log("Soy una condicion");
        if(inverse) return false;
        return true;
     }
}
