using UnityEngine;

[CreateAssetMenu(fileName = "VisibleEnemyCon", menuName = "Scriptable Objects/Con/VisibleEnemyCon")]
public class VisibleEnemyCon : ICondition
{
    
    enum TargetType{
    Enemy,
    NPC,
    Weapon,
    Health
}
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
        aux = _maxDistance >= targetList.GetTargetNPCDis();
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
