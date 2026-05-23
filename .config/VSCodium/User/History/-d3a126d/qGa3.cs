using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using Edge = Unity.VisualScripting.Edge;

public class IASaveUtility
{
    private IANodeList _dialoguechache;
    private IAGraphview _dialogueGV;
    
    private List<UnityEditor.Experimental.GraphView.Edge> edges => _dialogueGV.edges.ToList();

    private List<IANodeClass> nodes => _dialogueGV.nodes.ToList().Cast<IANodeClass>().ToList();
    public static IASaveUtility GetInstace(IAGraphview graphview)
    {
        return new IASaveUtility
        {
            _dialogueGV = graphview
        };
    }

    public void SaveGraph(string filename)
    {
         if (!edges.Any()) return;
        IAContainerScriptableObject diaContScrObj = ScriptableObject.CreateInstance<IAContainerScriptableObject>();
        UnityEditor.Experimental.GraphView.Edge[] conectedPorts = edges.Where(x => x.input.node != null).ToArray();
        for (int i = 0; i < conectedPorts.Length; ++i)
        {
            IANodeClass outnode = conectedPorts[i].output.node as IANodeClass;
            IANodeClass innode = conectedPorts[i].input.node as IANodeClass;
            diaContScrObj.linknodelist.Add(new IANodeLinkData
            {
                BasedNodeGuid = outnode.GUID,
                portName = conectedPorts[i].output.portName,
                childNodeGuid = innode.GUID

            });
           
        }
        foreach (IANodeClass dialogueNode in nodes.Where(node => !node.EntryPoint))
        {
            diaContScrObj.nodelist.Add(new IANodeDataClass
            {
                
                GUID = dialogueNode.GUID,
                Name = dialogueNode.Name,
                negative = dialogueNode.negative,
                conditions = dialogueNode.conditions,
                behaviour = dialogueNode.behaviour,
                pos = dialogueNode.GetPosition().position

            });

        }
        //diaContScrObj.onCreate();
        if (!AssetDatabase.IsValidFolder(path: "Assets/Resources/EnemyIA"))
            AssetDatabase.CreateFolder(parentFolder: "Assets/Resources", newFolderName: "EnemyIA");
        AssetDatabase.CreateAsset(diaContScrObj, path: $"Assets/Resources/EnemyIA/{filename}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string filename)
    {


        _dialoguechache = Resources.Load<IAContainerScriptableObject>("EnemyIA/" + filename);
        if (_dialoguechache == null)
        {
            EditorUtility.DisplayDialog(title: "File not found", message: filename, ok: "ok");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();


    }
    void CreateNodes()
    {
        foreach (IANodeDataClass nodedata in _dialoguechache.nodelist)
        {
            IANodeClass aux = _dialogueGV.CreateIANodeClass(nodedata.GUID,nodedata.Name,  nodedata.conditions,nodedata.behaviour, nodedata.negative);
          
           
            _dialogueGV.AddElement(aux);

            List<IANodeLinkData> nodeports = _dialoguechache.linknodelist.Where(x => x.BasedNodeGuid == nodedata.GUID).ToList();
            nodeports.ForEach(x => _dialogueGV.AddChoicePort(aux, x.portName));
        }

    }
    void ClearGraph()
    {
        nodes.Find(x => x.EntryPoint).GUID = _dialoguechache.linknodelist[0].BasedNodeGuid;
        foreach (IANodeClass node in nodes)
        {
            if (node.EntryPoint) continue;
            edges.Where(x => x.input.node == node).ToList().ForEach(edge => _dialogueGV.RemoveElement(edge));
            _dialogueGV.RemoveElement(node);

        }


    }
    void ConnectNodes(){
        for(int i = 0;i< nodes.Count;i++){
           List<IANodeLinkData> conections = _dialoguechache.linknodelist.Where(x=>x.BasedNodeGuid==nodes[i].GUID).ToList();
           for(int j = 0;j< conections.Count;j++)
           {
                string targetnodeGUID = conections[j].childNodeGuid;
                IANodeClass targetnode = nodes.First(x => x.GUID == targetnodeGUID);
                Linknodes(nodes[i].outputContainer[j].Q<Port>(),(Port)targetnode.inputContainer[0]);
                targetnode.SetPosition(newPos: new Rect(
                    _dialoguechache.nodelist.First(x =>x.GUID==targetnodeGUID).pos,
                    size: _dialogueGV.defaultnodeSize
                ));
           }
        }
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
