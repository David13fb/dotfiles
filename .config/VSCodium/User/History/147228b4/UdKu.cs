using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class StateMachineComponent : MonoBehaviour
{
    

    private List<IANode> actNodes;

    [SerializeField] IANodeList nodeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        actNodes.Add(nodeList.machineList[0]);
    }

    // Update is called once per frame
    private void Update()
    {

        foreach(IANode node in nodeList.machineList) node.ExecuteBehaviour(this.gameObject);
    }
    
}
