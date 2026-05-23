using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class graphIANode : Node
{
    public string GUID;
    public string Name;
   public ICondition conditions;
   public Ibehaviour behaviour;
   public bool EntryPoint = false;
   
}
