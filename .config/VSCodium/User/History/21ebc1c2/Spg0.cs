using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Serializable]
struct ConNode{
    ICondition con;
    string nodeName;
}
public class IANode
{
    [SerializeField] private string stateName = "";
    [SerializeField] private Ibehaviour mBehaviour;
    [SerializeField] private List<ConNode> nextNodesList;
    
}
