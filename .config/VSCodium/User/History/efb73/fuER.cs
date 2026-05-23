using Unity.FPS.Game;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthLimitCondition", menuName = "Scriptable Objects/Con/HealthLimitCondition")]
public class HealthLimitCondition : ICondition
{
    [Range(0f, 100f)] 
    [SerializeField] private float healtPercentageLimit = 50.0f;

      override public bool CheckCondition(GameObject Entity){
        bool aux = healtPercentageLimit <= Entity.GetComponent<Health>().GetRatio();
        if(inverse) return !aux;
        return aux;
     }
}
