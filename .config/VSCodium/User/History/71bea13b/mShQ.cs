using System;
using UnityEditor.Experimental.GraphView;
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
        graphIANode newNode = CreateIANodeClass();
        newNode.SetPosition(new Rect(contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).x, contentViewContainer.WorldToLocal(action.eventInfo.localMousePosition).y, defaultnodeSize.x, defaultnodeSize.y));
        // Add the new node to the GraphView
        AddElement(newNode);
    }

    

    ///PRIVATE METHODS

    private graphIANode GenerateEntryPointNode()
    {
        graphIANode node = new graphIANode
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
    private Port GeneratePort(graphIANode node, Direction dir, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, dir, capacity, type: typeof(float));
    }


    ///PUBLIC METHODS

    public graphIANode CreategraphIANode()
    {
        throw new NotImplementedException();
    }
    public void CreateNode(string nodeName)
    {
        AddElement(CreateIANodeClass());
    }

}
