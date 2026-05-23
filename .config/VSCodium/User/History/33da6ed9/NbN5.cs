using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class StateMachineComponent : MonoBehaviour
{
    [SerializeField] private string InitialNodeName = "NONE";

    private IANode actNode;
    private IANode actUpNode;

    [SerializeField] IANodeList nodeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        actNode = FindIANode(ref InitialNodeName);
    }

    // Update is called once per frame
    private void Update()
    {
        actNode.ExecuteBehaviour(this.gameObject);
        if(actUpNode.GetNodeName())
        string aux = actNode.EvaluateConditions(this.gameObject);
        if(!string.Equals(aux,"NONE")){
           actNode = FindIANode(ref InitialNodeName); 
        }
    }
    IANode FindIANode(ref string nodeName){
        IANode aux = new IANode();
        foreach(IANode node in nodeList.machineList){
            if(String.Equals(node.GetNodeName(), nodeName)){
                aux = node;
            }
        }

        return aux;
    }
    
    public void addSubState(IANode node){
        actUpNode = actNode;
        actNode = node;
    }

}
