using UnityEngine;

[CreateAssetMenu(fileName = "VisibleEnemyCon", menuName = "Scriptable Objects/Con/VisibleEnemyCon")]
public class VisibleEnemyCon : ICondition
{
    
    enum TargetType{
    Enemy,
    NPC,
    Weapon,
    Health
} [Range(0f, 180f)] 
    [SerializeField] private float maxAngle = 50.0f;

    [SerializeField] private TargetType targetType;
     override public bool CheckCondition(GameObject Entity){
        bool aux = false;
        GameObject target;
       TargetListControllerCmp targetList = Entity.GetComponent<TargetListControllerCmp>();
       switch (targetType)
       {
        case TargetType.Enemy:
        target = _maxDistance >= targetList.GetTargetEnemyDis();
        break;
        case TargetType.NPC:
        target = _maxDistance >= targetList.GetTargetNPCDis();
        break;
        case TargetType.Weapon:
        target = _maxDistance >= targetList.GetTargetWeaponDis();
        break;
        case TargetType.Health:
        autargetx = _maxDistance >= targetList.GetTargetHealthDis();
        break;
       }
       if(inverse) return !aux;
        return aux;
     }   
}
