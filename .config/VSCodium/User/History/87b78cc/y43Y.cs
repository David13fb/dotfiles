using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SubStadeBeh", menuName = "Scriptable Objects/SubStadeBeh")]
public class SubStadeBeh : Ibehaviour
{
    [SerializeField] private string InitialNodeName = "NONE";


    [SerializeField] IANodeList nodeList;

       override public bool Execute(GameObject g) {;
        foreach(IANode node in nodeList.machineList){
            if(String.Equals(node.GetNodeName(), InitialNodeName)){
               g.GetComponent<StateMachineComponent>().addSubState(ref node);
            }
        }
        return true;
    }
}
