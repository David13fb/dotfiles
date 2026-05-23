using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;




public class IAToolWindow : GraphView
{
    public readonly Vector2 defaultnodeSize = new Vector2(400, 200);
    public IAToolWindow()
    {
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
        this.AddManipulator(contextMenuManipulator);
    }

    private void CreateNodeAtMousePosition(DropdownMenuAction action)
    {
        graphIANode newNode = CreateGraphIANode();
        newNode.SetPosition(new Rect(contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).x, contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).y, defaultnodeSize.x, defaultnodeSize.y));
        // Add the new node to the GraphView
        AddElement(newNode);
    }



    ///PRIVATE METHODS

    public graphIANode GenerateEntryPointNode()
    {
        graphIANode node = new graphIANode
        {
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = true
        };

        Port next = GeneratePort(node, Direction.Output);
        node.outputContainer.Add(next);
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(100, 200, 200, 100));
        return node;
    }
    private Port GeneratePort(graphIANode node, Direction dir, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, dir, capacity, type: typeof(float));
    }


    ///PUBLIC METHODS

    public graphIANode CreateGraphIANode(string g, string n, ICondition i, Ibehaviour b)
    {
        Debug.Log(i);
        graphIANode node = new graphIANode
        {

            GUID = g,
            Name = n,
            conditions = i,
            behaviour = b,
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
        // Set de initial value
        nombre.SetValueWithoutNotify(n); 
        node.mainContainer.Add(nombre);

        node.RefreshExpandedState();


        ObjectField con = new ObjectField()
        {
            objectType = typeof(ICondition),
            value = i,
            label = "Selecciona un ICondition",
        };

        con.RegisterValueChangedCallback(evt =>
        {

            node.conditions = evt.newValue as ICondition;
            node.RefreshExpandedState();
        });
        node.Add(con);
        node.RefreshExpandedState();
        node.RefreshPorts();

        ObjectField beh = new ObjectField()
        {
            objectType = typeof(Ibehaviour),
            value = b,
            label = "Selecciona un IBehaviour",
        };

        beh.RegisterValueChangedCallback(evt =>
        {

            node.behaviour = evt.newValue as Ibehaviour;
            node.RefreshExpandedState();
            Debug.Log("ScriptableObject agregado: ");
        });
        node.Add(beh);
        node.RefreshExpandedState();
        node.RefreshPorts();



        node.SetPosition(new Rect(position: Vector2.zero, defaultnodeSize));
        return node;
    }



    public graphIANode CreateGraphIANode()
    {
        graphIANode node = new graphIANode
        {

            GUID = Guid.NewGuid().ToString(),
            Name = "no name",
            conditions = ScriptableObject.CreateInstance<ICondition>(),
            behaviour = ScriptableObject.CreateInstance<Ibehaviour>(),
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
        // Set de initial value
        nombre.SetValueWithoutNotify(node.title);
        node.mainContainer.Add(nombre);

        node.RefreshExpandedState();

        ObjectField con = new ObjectField()
        {
            objectType = typeof(ICondition),

            label = "Selecciona un ICondition",
        };

        con.RegisterValueChangedCallback(evt =>
        {
            node.conditions = evt.newValue as ICondition;
        });
        node.Add(con);
        node.RefreshExpandedState();
        node.RefreshPorts();

        ObjectField beh = new ObjectField()
        {
            objectType = typeof(Ibehaviour),

            label = "Selecciona un IBehaviour",
        };

        beh.RegisterValueChangedCallback(evt =>
        {

            node.behaviour = evt.newValue as Ibehaviour;
            Debug.Log("ScriptableObject agregado: " + (evt.newValue as Ibehaviour));
        });
        node.Add(beh);
        node.RefreshExpandedState();
        node.RefreshPorts();



        node.SetPosition(new Rect(position: Vector2.zero, defaultnodeSize));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateGraphIANode());
    }

    public void AddChoicePort(graphIANode _node, string overwriteName = "")
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
        textField.style.minWidth = 5; 
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
        _node.outputContainer.Add(newPort); // add a new port to _node
        _node.RefreshPorts();
        _node.RefreshExpandedState();

    }

    private void RemovePort(graphIANode node, Port port)
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

}
