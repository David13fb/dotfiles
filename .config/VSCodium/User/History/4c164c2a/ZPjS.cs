using UnityEngine;

[CreateAssetMenu(fileName = "ICondition", menuName = "Scriptable Objects/ICondition")]
public class ICondition : ScriptableObject
{
    [SerializeField] public bool inverse = false;
 virtual public bool CheckCondition(){
    if(inverse) return false;
    else return true;
 }   
}
