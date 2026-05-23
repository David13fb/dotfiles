using UnityEngine;

[CreateAssetMenu(fileName = "ICondition", menuName = "Scriptable Objects/ICondition")]
public class ICondition : ScriptableObject
{
 virtual public bool CheckCondition(){
    return false;
 }   
}
