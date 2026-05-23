using UnityEngine;

[CreateAssetMenu(fileName = "SubStadeBeh", menuName = "Scriptable Objects/SubStadeBeh")]
public class SubStadeBeh : Ibehaviour
{
    [SerializeField] private string InitialNodeName = "NONE";

    private IANode actNode;

    [SerializeField] IANodeList nodeList;

       override public bool Execute(GameObject g) {
        return true;
    }
}
