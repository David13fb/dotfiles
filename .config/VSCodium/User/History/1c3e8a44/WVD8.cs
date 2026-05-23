using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IANodeList", menuName = "Scriptable Objects/IANodeList")]
public class IANodeList : ScriptableObject
{
     [SerializeField] public List<IANode> machineList;
}
