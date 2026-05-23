using UnityEngine;

[CreateAssetMenu(fileName = "ICondition", menuName = "Scriptable Objects/ICondition")]
public class ICondition : ScriptableObject
{
    [SerializeField] public bool inverse = false;
 virtual public bool CheckCondition(GameObject Entity){
    if(inverse) return false;
    else return true;
 }   
}
