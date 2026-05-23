using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class StateMachineComponent : MonoBehaviour
{
    [SerializeField] private string InitialNodeName = "NONE";

    private IANode actNode;
    private IANode actUpNode;

    private Health m_Health;
    [SerializeField] IANodeList nodeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        m_Health = GetComponent<Health>();
        ResetMachine();
      //  m_Health.OnDie += ResetMachine;
    }

    
    private void ResetMachine(){
        actNode = FindIANode(ref InitialNodeName);
        Debug.LogError("SOY CONCHA ENTRO");
        actUpNode = actNode;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Estado actual " + actNode.GetNodeName());
        actNode.ExecuteBehaviour(this.gameObject);
        //normal state
        if(string.Equals(actUpNode.GetNodeName() , actNode.GetNodeName())){
                string aux = actNode.EvaluateConditions(this.gameObject);
            if(!string.Equals(aux,"NONE")){
                actNode = FindIANode(ref aux); 
                 print("Estado cambiado a " +actNode.GetNodeName());
                actUpNode = actNode;
            }
        }
        //sub state
        else{
            string aux = actUpNode.EvaluateConditions(this.gameObject);
            string aux2 = actNode.EvaluateConditions(this.gameObject);
            if(!string.Equals(aux,"NONE")){
                 actNode = FindIANode(ref aux); 
                actUpNode = actNode;
            }
            else if(!string.Equals(aux2,"NONE")){
                 actNode = FindIANode(ref aux2); 
            }
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
