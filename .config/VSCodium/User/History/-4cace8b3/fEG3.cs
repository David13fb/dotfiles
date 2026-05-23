using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.EditorTools;
using System;
using System.Linq;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEditor.Search;
using System.Xml;

public class IAGraphview : GraphView
{
    public readonly Vector2 defaultnodeSize = new Vector2(400, 200);
    public IAGraphview()
    {
        //styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(path: "DialogueGraphBackground"));
        SetupZoom(0.1f, 4.0f);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        GridBackground _grid = new GridBackground();
        Insert(index: 0, _grid);
        _grid.StretchToParentSize();
        AddElement(GenerateEntryPointNode());

            var contextMenuManipulator = new ContextualMenuManipulator(evt =>
         {
        // Right-click menu items
        evt.menu.AppendAction("Create IA Node", CreateNodeAtMousePosition, DropdownMenuAction.AlwaysEnabled);
        });

    // Add the manipulator to the GraphView
    this.AddManipulator(contextMenuManipulator);
    }
    private Port GeneratePort(IANodeClass node, Direction dir, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, dir, capacity, type: typeof(float));
    }

    private IANodeClass GenerateEntryPointNode()
    {
        IANodeClass node = new IANodeClass
        { 
            GUID = Guid.NewGuid().ToString(),
            //person = "personaje",
           
           
            EntryPoint = true
        };

        Port next = GeneratePort(node, Direction.Output);
        node.outputContainer.Add(next);
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(100, 200, 200, 100));
        return node;
    }

    private void CreateNodeAtMousePosition(DropdownMenuAction action)
{
    
    // Get the mouse position in the graph view
   
       //Vector2 mousePosition = this.WorldToLocal(Event.current.mousePosition);
        
    //Create a new node at the mouse position
    IANodeClass newNode = CreateIANodeClass();
   newNode.SetPosition(new Rect(contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).x,contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).y, defaultnodeSize.x,defaultnodeSize.y));

    // Add the new node to the GraphView
    AddElement(newNode);
}
    public void CreateNode(string nodeName)
    {
        AddElement(CreateIANodeClass());
    }




     public IANodeClass CreateIANodeClass(string g, string n, IIACondition i, IIAbehaviour b,bool neg)
    {
         Debug.Log(i);
        IANodeClass node = new IANodeClass
        {
           
            GUID = g,
            Name = n,
            negative = neg,
            conditions = i,
           behaviour =b,
            EntryPoint = false
        };
     

        Port parent = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        node.inputContainer.Add(parent);

        Button button = new Button(clickEvent: () => { AddChoicePort(node); });
        node.titleContainer.Add(button);
        button.text = "New link";
        // Crear el TextField para el nombre
        TextField nombre = new TextField(label: "name");
        nombre.value = n;
        nombre.RegisterValueChangedCallback((ChangeEvent<string> evt) =>
        {
            node.title = evt.newValue;
            node.Name = evt.newValue;
            node.RefreshExpandedState();
        });
        // Inicializar el valor del TextField
        nombre.SetValueWithoutNotify(node.title); // Aquí se inicializa el valor
        node.mainContainer.Add(nombre);
        
         Toggle _toggle = new Toggle { label = "Negative" };
        _toggle.value = neg; // Inicializa el Toggle
        _toggle.RegisterValueChangedCallback(evt =>
        {
              node.negative = evt.newValue; // Actualiza el atributo booleano
            //outputPort.SetValue(node.negative);
        }); // Registra el callback

        // Agregar el Toggle al contenido del nodo
        node.mainContainer.Add(_toggle);
        node.RefreshExpandedState();
        

          ObjectField con = new ObjectField(){
            objectType = typeof(IIACondition),
            value = i,
            label = "Selecciona un IIACondition",
           };
       
        con.RegisterValueChangedCallback(evt =>
        {
            
            node.conditions= evt.newValue as IIACondition;
            node.RefreshExpandedState();
           // Debug.Log("ScriptableObject agregado: " + (evt.newValue as IIACondition)?.nombre);
        });
        node.Add(con);
        node.RefreshExpandedState();
        node.RefreshPorts();
      
         ObjectField beh = new ObjectField(){
            objectType = typeof(IIAbehaviour),
            value = b,
            label = "Selecciona un IABehaviour",
        };
       
        beh.RegisterValueChangedCallback(evt =>
        {
            
            node.behaviour= evt.newValue as IIAbehaviour;
            node.RefreshExpandedState();
           Debug.Log("ScriptableObject agregado: ");
        });
        node.Add(beh);
        node.RefreshExpandedState();
        node.RefreshPorts();
      


        node.SetPosition(new Rect(position: Vector2.zero, defaultnodeSize));
        return node;
    }




    public IANodeClass CreateIANodeClass()
    {
        IANodeClass node = new IANodeClass
        {
            
            GUID = Guid.NewGuid().ToString(),
            Name = "no name",
            conditions = ScriptableObject.CreateInstance<IIACondition>(),
           behaviour = ScriptableObject.CreateInstance<IIAbehaviour>(),
            EntryPoint = false
        };
     

        Port parent = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        node.inputContainer.Add(parent);

        Button button = new Button(clickEvent: () => { AddChoicePort(node); });
        node.titleContainer.Add(button);
        button.text = "New link";
        // Crear el TextField para el nombre
        TextField nombre = new TextField(label: "name");
        nombre.RegisterValueChangedCallback((ChangeEvent<string> evt) =>
        {
            node.title = evt.newValue;
            node.Name = evt.newValue;
            node.RefreshExpandedState();
        });
        // Inicializar el valor del TextField
        nombre.SetValueWithoutNotify(node.title); // Aquí se inicializa el valor
        node.mainContainer.Add(nombre);
           
         Toggle _toggle = new Toggle { label = "Negative" };
        _toggle.value = false; // Inicializa el Toggle
        _toggle.RegisterValueChangedCallback(evt =>
        {
              node.negative = evt.newValue; // Actualiza el atributo booleano
            //outputPort.SetValue(node.negative);
        }); // Registra el callback

        // Agregar el Toggle al contenido del nodo
        node.mainContainer.Add(_toggle);
        node.RefreshExpandedState();

          ObjectField con = new ObjectField(){
            objectType = typeof(IIACondition),
            
            label = "Selecciona un IIACondition",};
       
        con.RegisterValueChangedCallback(evt =>
        {
            
            node.conditions= evt.newValue as IIACondition;
           //  node.RefreshExpandedState();
           // Debug.Log("ScriptableObject agregado: " + (evt.newValue as IIACondition)?.nombre);
        });
        node.Add(con);
        node.RefreshExpandedState();
        node.RefreshPorts();
      
         ObjectField beh = new ObjectField(){
            objectType = typeof(IIAbehaviour),
            
            label = "Selecciona un IABehaviour", };
       
        beh.RegisterValueChangedCallback(evt =>
        {
            
            node.behaviour = evt.newValue as IIAbehaviour;
            // node.RefreshExpandedState();
            Debug.Log("ScriptableObject agregado: " + (evt.newValue as IIAbehaviour));
        });
        node.Add(beh);
        node.RefreshExpandedState();
        node.RefreshPorts();
      


        node.SetPosition(new Rect(position: Vector2.zero, defaultnodeSize));
        return node;
    }

    
    
    private void addCondition(IANodeClass node){
       
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compports = new List<Port>();
        ports.ForEach(funcCall: (port) =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compports.Add(port);
            }

        });
        return compports;
    }
    public void AddChoicePort(IANodeClass _node, string overwriteName = "")
    {

        Port newPort = GeneratePort(_node, Direction.Output);

        Label oldlabel = newPort.contentContainer.Q<Label>(name: "type");
        newPort.contentContainer.Remove(oldlabel);
        int lastPortNum = _node.outputContainer.Query(name: "connector").ToList().Count;

        string ChoicePortName = string.IsNullOrEmpty(overwriteName)
        ? $"Choice {lastPortNum + 1}" : overwriteName;
        TextField textField = new TextField
        {
            name = string.Empty,
            value = ChoicePortName
        };
        textField.style.minWidth = 5; // Mínimo ancho inicial
        textField.style.maxWidth = 30;
        textField.RegisterValueChangedCallback(evt => newPort.portName = evt.newValue);
        newPort.contentContainer.Add(child: new Label(" "));
        newPort.contentContainer.Add(textField);
        Button deleteButon = new Button(clickEvent: () => RemovePort(_node, newPort))
        {
            text = "X"
        };
        newPort.contentContainer.Add(deleteButon);
        // newPort.portName = ChoicePortName;
        _node.outputContainer.Add(newPort); // Agregamos el nuevo puerto en lugar de _node
        _node.RefreshPorts();
        _node.RefreshExpandedState();

    }

    private void RemovePort(IANodeClass node, Port port)
    {

        var targetEdge = edges
                            .Where(x => x.output.portName == port.portName && x.output.node == port.node)
                            .ToList();


        if (targetEdge.Any())
        {


            UnityEditor.Experimental.GraphView.Edge ed = targetEdge.First();
            ed.input.Disconnect(ed);
            RemoveElement(targetEdge.First());
        }
        node.outputContainer.Remove(port);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }


   



}
