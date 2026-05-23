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
                aux.graphPos = outnode.GetPosition().position;
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
    // 1. Cargar el recurso desde la carpeta Resources/IA
    _ianodeListchache = Resources.Load<IANodeList>($"IA/{filename}");
    if (_ianodeListchache == null)
    {
        EditorUtility.DisplayDialog(title: "File not found", message: filename, ok: "ok");
        return;
    }
    
    // 2. Limpiar el grafo actual
    ClearGraph();

    // 3. Generar el punto de entrada (EntryPoint)
    graphIANode _entry = _iatoolwin.GenerateEntryPointNode();
    
    // Diccionario para asociar el NOMBRE del nodo con su instancia visual en el GraphView
    Dictionary<string, graphIANode> pairnode = new Dictionary<string, graphIANode>();

    // 4. Instanciar todos los nodos de estado en el GraphView
    foreach (IANode nodeData in _ianodeListchache.machineList)
    {
        if (nodeData == null || string.IsNullOrEmpty(nodeData.stateName)) continue;

        // Crear el nodo visual
        graphIANode graphNode = _iatoolwin.CreateGraphIANode(nodeData.stateName, nodeData.stateName, nodeData.mCondition, nodeData.mBehaviour);
        _iatoolwin.AddElement(graphNode);
        
        // Posicionar el nodo usando tu variable original del primer script (graphPos o pos)
        graphNode.SetPosition(new Rect(nodeData.graphPos, _iatoolwin.defaultnodeSize));
        
        // Registrar en el diccionario usando el nombre como clave única
        if (!pairnode.ContainsKey(nodeData.stateName))
        {
            pairnode.Add(nodeData.stateName, graphNode);
        }
    }

    // 5. Conectar el EntryPoint al primer nodo de la lista de datos
    if (_entry != null && _ianodeListchache.machineList.Count > 0)
    {
        var firstNodeData = _ianodeListchache.machineList[0];
        if (firstNodeData != null && pairnode.ContainsKey(firstNodeData.stateName))
        {
            var firstVisualNode = pairnode[firstNodeData.stateName];
            Linknodes(_entry.outputContainer[0].Q<Port>(), (Port)firstVisualNode.inputContainer[0]);
        }
    }

    // 6. Conectar todos los stateNodes entre sí buscando por nombre
    foreach (IANode node in _ianodeListchache.machineList)
    {
        if (node == null || !pairnode.ContainsKey(node.stateName)) continue;

        graphIANode aux = pairnode[node.stateName];

        for (int j = 0; j < node.nextNodesList.Count; j++)
        {
            IANode nodeConnect = node.nextNodesList[j];
            if (nodeConnect == null) continue;

            // CORREGIDO: Buscamos por el NOMBRE del nodo conectado, solucionando el fallo de referencia en memoria
            if (pairnode.ContainsKey(nodeConnect.stateName))
            {
                graphIANode targetNode = pairnode[nodeConnect.stateName];

                // Verificación de seguridad para evitar errores de índice fuera de rango en los puertos de salida
                if (aux.outputContainer.childCount > j)
                {
                    Linknodes(aux.outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
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
