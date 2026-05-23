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
        GameObject target = new GameObject();
       TargetListControllerCmp targetList = Entity.GetComponent<TargetListControllerCmp>();
       switch (targetType)
       {
        case TargetType.Enemy:
        target = targetList.GetTargetEnemy();
        break;
        case TargetType.NPC:
        target = targetList.GetTargetNPC();
        break;
        case TargetType.Weapon:
        target = targetList.GetTargetWeapon();
        break;
        case TargetType.Health:
        target = targetList.GetTargetHealth();
        break;
       }
       Vector3 forwardEntity = Entity.transform.forward;
       Vector3 dirVector = new  Vector3(target.transform.position-Entity.transform.position);
       if(inverse) return !aux;
        return aux;
     }   
}
