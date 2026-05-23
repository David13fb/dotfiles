using UnityEngine;

[CreateAssetMenu(fileName = "ICondition", menuName = "Scriptable Objects/ICondition")]
public class ICondition : ScriptableObject
{
    bool inverse = false;
 virtual public bool CheckCondition(){
    return false;
 }   
}
