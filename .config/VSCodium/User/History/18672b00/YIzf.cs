using UnityEngine;

[CreateAssetMenu(fileName = "TrueCondition", menuName = "Scriptable Objects/Con/TrueCondition")]
public class TrueCondition : ICondition
{
     override public bool CheckCondition(){
        if(inverse) return false;
        return true;
     }
}
