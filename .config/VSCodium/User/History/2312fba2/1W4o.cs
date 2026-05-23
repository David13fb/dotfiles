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
using UnityEngine.UIElements;

public class IAToolSaveUtility
{
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
        Dictionary<String, IANode> conections = new Dictionary<String, IANode>();
        UnityEditor.Experimental.GraphView.Edge[] conectedPorts = edges.Where(x => x.input.node != null).ToArray();

        for (int i = 1; i < conectedPorts.Length; ++i)
        {
            graphIANode outnode = conectedPorts[i].output.node as graphIANode;
            graphIANode innode = conectedPorts[i].input.node as graphIANode;
            if (outnode == null || innode == null) continue;
            if (!conections.ContainsKey(outnode.Name))
            {
                IANode aux = new IANode();
                aux.nextNodesList = new List<IANode>();
                aux.stateName = outnode.Name;
                aux.mBehaviour = outnode.behaviour;
                aux.mCondition = outnode.conditions;
                conections.Add(outnode.Name, aux);
                aux.graphPos = outnode.GetPosition().position;
            }
            if (!conections.ContainsKey(innode.Name))
            {

                IANode aux = new IANode();
                aux.nextNodesList = new List<IANode>();
                aux.stateName = innode.Name;
                aux.mBehaviour = innode.behaviour;
                aux.mCondition = innode.conditions;
                conections.Add(innode.Name, aux);
                aux.graphPos = innode.GetPosition().position;
            }

            conections[outnode.Name].nextNodesList.Add(conections[innode.Name]);
            if (outnode.EntryPoint) nodeList.InitialNodeName = innode.Name;
        }
        nodeList.machineList = conections.Values.ToList();
        AssetDatabase.CreateAsset(nodeList, path: $"Assets/Resources/IA/{filename}.asset");
        AssetDatabase.SaveAssets();
    }

public void LoadGraph(string filename)
{
    _ianodeListchache = Resources.Load<IANodeList>($"IA/{filename}");
    if (_ianodeListchache == null)
    {
        EditorUtility.DisplayDialog(title: "File not found", message: filename, ok: "ok");
        return;
    }
    ClearGraph();

    // 1. Generar e introducir físicamente el EntryPoint en el grafo
    graphIANode _entry = _iatoolwin.GenerateEntryPointNode();
    _iatoolwin.AddElement(_entry); // ¡CRÍTICO! Debe añadirse al Grafo antes de conectar cables
    
    Dictionary<string, graphIANode> pairnode = new Dictionary<string, graphIANode>();

    // 2. Instanciar los nodos de estado
    foreach (IANode nodeData in _ianodeListchache.machineList)
    {
        if (nodeData == null || string.IsNullOrEmpty(nodeData.stateName)) continue;

        graphIANode graphNode = _iatoolwin.CreateGraphIANode(nodeData.stateName, nodeData.stateName, nodeData.mCondition, nodeData.mBehaviour);
        _iatoolwin.AddElement(graphNode);
        
        graphNode.SetPosition(new Rect(nodeData.graphPos, _iatoolwin.defaultnodeSize));
        
        if (!pairnode.ContainsKey(nodeData.stateName))
        {
            pairnode.Add(nodeData.stateName, graphNode);
        }

        // Generar los puertos de salida según las conexiones guardadas
        if (nodeData.nextNodesList != null)
        {
            for (int k = 0; k < nodeData.nextNodesList.Count; k++)
            {
                _iatoolwin.AddChoicePort(graphNode); 
            }
        }
    }

    // 3. Conectar el EntryPoint al primer nodo (Nodo A)
    if (_entry != null && _ianodeListchache.machineList.Count > 0)
    {
        var firstNodeData = _ianodeListchache.machineList[0];
        if (firstNodeData != null && pairnode.ContainsKey(firstNodeData.stateName))
        {
            var firstVisualNode = pairnode[firstNodeData.stateName];
            
            // Forzar puerto de salida en el EntryPoint si no viene por defecto
            if (_entry.outputContainer.childCount == 0)
            {
                _iatoolwin.AddChoicePort(_entry);
            }
            
            Port outputPort = _entry.outputContainer[0].Q<Port>();
            Port inputPort = firstVisualNode.inputContainer[0].Q<Port>();
            
            if(outputPort != null && inputPort != null)
            {
                Linknodes(outputPort, inputPort);
            }
        }
    }

    // 4. Conectar todos los stateNodes entre sí (A -> B)
    foreach (IANode node in _ianodeListchache.machineList)
    {
        if (node == null || !pairnode.ContainsKey(node.stateName)) continue;

        graphIANode aux = pairnode[node.stateName];

        for (int j = 0; j < node.nextNodesList.Count; j++)
        {
            IANode nodeConnect = node.nextNodesList[j];
            if (nodeConnect == null) continue;

            if (pairnode.ContainsKey(nodeConnect.stateName))
            {
                graphIANode targetNode = pairnode[nodeConnect.stateName];

                if (aux.outputContainer.childCount > j)
                {
                    Port outputPort = aux.outputContainer[j].Q<Port>();
                    Port inputPort = targetNode.inputContainer[0].Q<Port>();

                    if (outputPort != null && inputPort != null)
                    {
                        Linknodes(outputPort, inputPort);
                    }
                }
            }
        }
    }
}

    void ClearGraph()
    {
        _iatoolwin.DeleteElements(_iatoolwin.graphElements.ToList());
    }

    void Linknodes(Port _output, Port _input)
    {
        if (_output == null || _input == null) return;

        var temp = new UnityEditor.Experimental.GraphView.Edge();
        temp.output = _output;
        temp.input = _input;
        temp.input.Connect(temp);
        temp.output.Connect(temp);
        _iatoolwin.Add(temp);
    }

}
