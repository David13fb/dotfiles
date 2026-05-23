using UnityEngine;

[CreateAssetMenu(fileName = "IANodeList", menuName = "Scriptable Objects/IANodeList")]
public class IANodeList : ScriptableObject
{
     [SerializeField] private List<IANode> machineList;
}
