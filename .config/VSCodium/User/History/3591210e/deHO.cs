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
    private string _fileName="new Behaviour";

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

}