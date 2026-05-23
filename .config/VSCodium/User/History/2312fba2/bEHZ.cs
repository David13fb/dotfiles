using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using NUnit.Framework.Interfaces;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
        graphIANode _entry = _iatoolwin.GenerateEntryPointNode();
        Dictionary<IANode,graphIANode> pairnode = new Dictionary<IANode, graphIANode>();
        var visualNodes = _iatoolwin.nodes.Cast<graphIANode>().ToList();
    
    foreach (IANode nodeData in _ianodeListchache.machineList)
    {
        // Busca el nodo visual que coincida (por nombre o GUID según tu lógica)
        graphIANode visualNode = visualNodes.FirstOrDefault(x => x.viewDataKey == nodeData.stateName || x.title == nodeData.stateName);
        if (visualNode != null)
        {
            pairnode.Add(nodeData, visualNode);
        }
    }

    // Conectar el EntryPoint al primer nodo de la lista si existe
    graphIANode entryNode = visualNodes.FirstOrDefault(x => x.EntryPoint);
    if (entryNode != null && _ianodeListchache.machineList.Count > 0)
    {
        var firstNode = pairnode[_ianodeListchache.machineList[0]];
        LinkNodes(entryNode.outputContainer[0].Q<Port>(), (Port)firstNode.inputContainer[0]);
    }

    // Conectar todos los stateNodes entre sí
    foreach (IANode node in _ianodeListchache.machineList)
    {
        if (!pairnode.ContainsKey(node)) continue;
        
        graphIANode aux = pairnode[node];
        
        for (int j = 0; j < node.nextNodesList.Count; j++)
        {
            IANode nodeConnect = node.nextNodesList[j];
            if (pairnode.ContainsKey(nodeConnect))
            {
                graphIANode targetNode = pairnode[nodeConnect];
                
                // Conecta el puerto de salida 'j' al puerto de entrada 0 del nodo destino
                LinkNodes(aux.outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                
                // Opcional: Aplica la posición guardada si tus nodos de datos la contienen
                // targetNode.SetPosition(new Rect(nodeConnect.pos, _iatoolwin.defaultnodeSize));
            }
        }
    }
        
    }

    void ClearGraph()
    {   
        _iatoolwin.DeleteElements(_iatoolwin.graphElements.ToList());
    }

    private void Linknodes(Port _output,Port _input){
       var temp = new UnityEditor.Experimental.GraphView.Edge();
       temp.output = _output;
       temp.input = _input;
       temp.input.Connect(temp);
       temp.output.Connect(temp);
       _dialogueGV.Add(temp);
    }

}
