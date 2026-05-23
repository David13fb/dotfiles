using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Metroidvania
{


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
            throw new NotImplementedException();
        }

        private graphIANode GenerateEntryPointNode()
        {
            return new graphIANode();
        }

    }
}
