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
}