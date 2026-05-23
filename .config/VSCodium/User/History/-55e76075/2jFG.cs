using UnityEngine;

[CreateAssetMenu(fileName = "AndCondition", menuName = "Scriptable Objects/Con/AndCondition")]
public class AndCondition : ICondition
{   
    
    [SerializeField] ICondition conditionA;

    [SerializeField] ICondition conditionB;
     override public bool CheckCondition(GameObject Entity){
        bool aux = conditionA.CheckCondition(Entity) && conditionB.CheckCondition(Entity);
        if(inverse) return false;
        return true;
     }
}
