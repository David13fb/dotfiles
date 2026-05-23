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
    
}
