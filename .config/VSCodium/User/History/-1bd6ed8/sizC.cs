using UnityEngine;

[CreateAssetMenu(fileName = "Ibehaviour", menuName = "Scriptable Objects/Ibehaviour")]
public class Ibehaviour : ScriptableObject
{
    virtual public bool Execute(GameObject g){
        return true;
    }
}
