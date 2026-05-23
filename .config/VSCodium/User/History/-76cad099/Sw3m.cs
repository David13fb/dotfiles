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
       switch (targetType)
       {
        case TargetType.Enemy:
        break;
        
       }
     }   
}
