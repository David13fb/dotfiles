using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using NUnit.Framework.Interfaces;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class IAToolSaveUtility{
private IANodeList _ianodeListchache;
    private IAToolWindow _iatoolwin;
    
    private List<UnityEditor.Experimental.GraphView.Edge> edges => _iatoolwin.edges.ToList();

    private List<graphIANode> nodes => _iatoolwin.nodes.ToList().Cast<graphIANode>().ToList();
    public static IAToolSaveUtility GetInstace(IAToolWindow graphview)
    {
        return new IAToolSaveUtility
        {
            _iatoolwin = graphview
        };
    }


    public void SaveGraph(string filename)
    {
        if (!edges.Any()) return;
        IANodeList nodeList = ScriptableObject.CreateInstance<IANodeList>();
        Dictionary<String,IANode> conections = new Dictionary<String,IANode>();
        UnityEditor.Experimental.GraphView.Edge[] conectedPorts = edges.Where(x => x.input.node != null).ToArray();
       
         for (int i = 1; i < conectedPorts.Length; ++i)
        {
            graphIANode outnode = conectedPorts[i].output.node as graphIANode;
            graphIANode innode = conectedPorts[i].input.node as graphIANode;
            if (outnode == null || innode == null) continue; 
            if(!conections.ContainsKey(outnode.Name)){
                
                IANode aux = new IANode();
                aux.nextNodesList = new List<IANode>();
                aux.stateName = outnode.Name;
                aux.mBehaviour = outnode.behaviour;
                aux.mCondition = outnode.conditions;
                conections.Add(outnode.Name,aux);
            }
            if(!conections.ContainsKey(innode.Name)){
                
                IANode aux = new IANode();
                aux.nextNodesList = new List<IANode>();
                aux.stateName = innode.Name;
                aux.mBehaviour = innode.behaviour;
                aux.mCondition = innode.conditions;
                conections.Add(innode.Name,aux);
            }
            
            conections[outnode.Name].nextNodesList.Add(conections[innode.Name]);
            if(outnode.EntryPoint) nodeList.InitialNodeName = innode.Name;
        }
        nodeList.machineList = conections.Values.ToList();
        AssetDatabase.CreateAsset(nodeList, path: $"Assets/Resources/IA/{filename}.asset");
        AssetDatabase.SaveAssets();
    }
    

    public void LoadGraph(string filename){
        _ianodeListchache = Resources.Load<IANodeList>(path: $"Assets/Resources/IA/{filename}");
        if(_ianodeListchache == null){
            EditorUtility.DisplayDialog(title: "File not found", message: filename, ok: "ok");
            return;
        }
        ClearGraph();
        
        foreach(IANode node in _ianodeListchache.machineList){
            graphIANode = 

        }


        bool[] _visit = new bool[_ianodeListchache.machineList.Count];
        bool[] _proccess = new bool[_ianodeListchache.machineList.Count];
        List<IANode> _processNodes = new List<IANode>();
    }

    void ClearGraph()
    {   
        _iatoolwin.DeleteElements(_iatoolwin.graphElements.ToList());
    }
}
