using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[System.Serializable]
public class IANode
{
    [SerializeField] private string stateName = "";
    [SerializeField] private Ibehaviour mBehaviour;
    [SerializeField] private List<IANode> nextNodesList;
    
    public string GetNodeName(){ 
        return stateName;
    }
    public void ExecuteBehaviour(GameObject g){
        mBehaviour.Execute(g);
    }
    public string EvaluateConditions(GameObject Entity){
        string nextState = "NONE";
        foreach(var con in nextNodesList){
            if(con.con.CheckCondition(Entity)){
                nextState = con.nodeName;
                break;
            }
        }
        return nextState;
    }
}
