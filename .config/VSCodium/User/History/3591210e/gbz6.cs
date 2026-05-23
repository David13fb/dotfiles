using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Profiling;

public class GraphIAEditor : EditorWindow
{ 
    private IAToolWindow _iatoolwin;
    private string _fileName="new IANodeList";

    [MenuItem("Tools/IA/IAGraph")]
    public static void OpenIAToolWindow(){
        var window = GetWindow<GraphIAEditor>();
        window.titleContent = new GUIContent(text: "GraphIAEditor");
    }

    void OnEnable()
    {
        ConstructIAToolWindow();
        GenerateToolBar();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        rootVisualElement.Remove(_iatoolwin);
    }

    private void ConstructIAToolWindow()
    {
        
        _iatoolwin = new IAToolWindow{
            name = "IAToolWindow"
        };
        _iatoolwin.StretchToParentSize();
        rootVisualElement.Add(_iatoolwin);
    }

    private void GenerateToolBar()
    {
        Toolbar toolbar = new Toolbar();
        TextField _filetextfield = new TextField(label:"File Name:");
        _filetextfield.SetValueWithoutNotify(_fileName);
        _filetextfield.MarkDirtyRepaint();
        _filetextfield.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(_filetextfield);

        toolbar.Add(child: new Button(clickEvent: ()=>RequestDataOperation(true)){text = "Save Data"});
        toolbar.Add(child: new Button(clickEvent: ()=>RequestDataOperation(false)){text = "Load Data"});

        Button nodeCreateBt = new Button(clickEvent:()=>{
            _iatoolwin.CreateNode("IA node");
        });
        nodeCreateBt.text = "Create Node";
        toolbar.Add(nodeCreateBt);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName)){
            EditorUtility.DisplayDialog(title:"invalid name",message: "meter nombre",ok:"ok");
            return;
        }
        IAToolSaveUtility saveUtility =  IAToolSaveUtility.GetInstace(_iatoolwin);
         if(save){
            saveUtility.SaveGraph(_fileName);
        }
        else if (!save){
            //saveUtility.LoadGraph(_fileName);
        }
    }

}