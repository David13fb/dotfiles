using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.FPS.Game;
using Unity.Play.Publisher.Editor;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.PlayerLoop;

public class StateMachineComponent : MonoBehaviour
{
    [SerializeField] private string InitialNodeName = "NONE";

    private IANode actNode;
    private IANode actUpNode;

    private float updateTime = 0.25f;
    private float actUpdateTime = 0.0f;
    [SerializeField] IANodeList nodeList;
    IANodeList childNodeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        ResetMachine();
      //  m_Health.OnDie += ResetMachine;
    }

    
    public void ResetMachine(){
        actNode = FindIANode(ref InitialNodeName);
       
        actUpNode = actNode;
    }

    // Update is called once per frame
    private void Update()
    {
        if(actUpdateTime>= updateTime){
            actUpdateTime = 0;
        actNode.ExecuteBehaviour(this.gameObject);
        //normal state
        if(string.Equals(actUpNode.GetNodeName() , actNode.GetNodeName())){
                string aux = actNode.EvaluateConditions(this.gameObject);
            if(!string.Equals(aux,"NONE")){
                actNode = FindIANode(ref aux); 
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
        } else actUpdateTime += Time.deltaTime;
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
    
    public void addSubState(IANode node,IANodeList list){
        actUpNode = actNode;
        actNode = node;
        childNodeList = list;
    }

}
