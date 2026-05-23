using UnityEngine;

[CreateAssetMenu(fileName = "DistanceCondition", menuName = "Scriptable Objects/Con/DistanceCondition")]
enum TargetType
public class DistanceCondition : ICondition
{
    
    [SerializeField] private float _maxDistance = 1.0f;



     override public bool CheckCondition(GameObject Entity){
       
     }   
}
