using UnityEngine;

[CreateAssetMenu(fileName = "AndCondition", menuName = "Scriptable Objects/Con/AndCondition")]
public class AndCondition : ICondition
{   
    
     override public bool CheckCondition(GameObject Entity){
        Debug.Log("Soy una condicion");
        if(inverse) return false;
        return true;
     }
}
