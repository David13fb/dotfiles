using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[System.Serializable]
public struct ConNode{
    public ICondition con;
    public string nodeName;
}
[System.Serializable]
public class IANode
{
    [SerializeField] private string stateName = "";
    [SerializeField] private Ibehaviour mBehaviour;
    [SerializeField] private List<ConNode> nextNodesList;
    
    public string GetNodeName(){ 
        return stateName;
    }
    public void ExecuteBehaviour(ref GameObject g){
        mBehaviour.Execute(ref g);
    }
    public string EvaluateConditions(){
        string nextState = "NONE";
        foreach(var con in nextNodesList){
            if(con.con.CheckCondition()){
                nextState = con.nodeName;
                break;
            }
        }
        return nextState;
    }
}
