using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IANodeList", menuName = "Scriptable Objects/IANodeList")]
public class IANodeList : ScriptableObject
{
     [SerializeField] public string InitialNodeName = "NONE";
     [SerializeField] public List<IANode> machineList;
}
