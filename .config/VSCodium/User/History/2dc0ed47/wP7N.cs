using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[System.Serializable]
public class IANode
{
    [SerializeField] public string stateName = "";
    [SerializeField] public Ibehaviour mBehaviour;

    [SerializeField] public ICondition mCondition;
    [SerializeReference] public List<IANode> nextNodesList;

    public Vector2 graphPos;

    public void ExecuteBehaviour(GameObject g){
        mBehaviour.Execute(g);
    }

    public List<IANode> checkConditions(){
        List<IANode> aux = new List<IANode>();
        foreach(IANode node in nextNodesList)
        return aux;
    }
    
}
