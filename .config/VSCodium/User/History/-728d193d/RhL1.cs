using UnityEngine;

[CreateAssetMenu(fileName = "Patata", menuName = "Scriptable Objects/Patata")]
public class Patata : ScriptableObject
{
    public int numPatatas = 0;
    public void AddPatata(int a){
        numPatatas+=a;
    }
}
