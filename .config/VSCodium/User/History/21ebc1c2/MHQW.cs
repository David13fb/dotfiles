using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[Serializable]
struct ConNode{
    public ICondition con;
    public string nodeName;
}
public class IANode
{
    [SerializeField] private string stateName = "";
    [SerializeField] private Ibehaviour mBehaviour;
    [SerializeField] private List<ConNode> nextNodesList;
    
    public string GetNode(){ 
        return stateName;
    }
    public void ExecuteBehaviour(){
        mBehaviour.Execute();
    }
    public string EvaluateConditions(){
        string nextState = "NONE";
        foreach(var con in nextNodesList){
            if(con.)
        }
        return nextState;
    }
}
