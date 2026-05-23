using UnityEngine;

[CreateAssetMenu(fileName = "DistanceCondition", menuName = "Scriptable Objects/Con/DistanceCondition")]
enum TargetType{
    Enemy,
    NPC,
    Weapon,
    Health
}
public class DistanceCondition : ICondition
{
    
    [SerializeField] private float _maxDistance = 1.0f;

    [SerializeField] private TargetType targetType;
     override public bool CheckCondition(GameObject Entity){
       bool aux = false;
       TargetListControllerCmp targetList = Entity.GetComponent<TargetListControllerCmp>();
       switch (targetType)
       {
        case TargetType.Enemy:
        aux = _maxDistance >= targetList.GetTargetEnemyDis();
        break;
        case TargetType.NPC:
        aux = _maxDistance >= targetList.
        break;
        case TargetType.Weapon:
        aux = _maxDistance >= targetList.GetTargetWeaponDis();
        break;
        case TargetType.Health:
        aux = _maxDistance >= targetList.GetTargetHealthDis();
        break;
       }
       if(inverse) return !aux;
        return aux;
     }   
}
