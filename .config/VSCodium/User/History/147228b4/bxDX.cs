using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class StateMachineComponent : MonoBehaviour
{
    

    private IANode actNode;

    [SerializeField] IANodeList nodeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        actNode = nodeList.machineList[0];
    }

    // Update is called once per frame
    private void Update()
    {
      /*  actNode.ExecuteBehaviour(this.gameObject);
        string aux = actNode.EvaluateConditions(this.gameObject);
        if(!string.Equals(aux,"NONE")){
           actNode = FindIANode(ref InitialNodeName); 
        }*/
    }
    
}
