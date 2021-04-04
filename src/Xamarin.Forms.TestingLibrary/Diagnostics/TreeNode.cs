using System.Collections.Generic;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    /// <summary>
    /// Represents a tree node, used for rendering the Debug tree.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// Gets the nodes in this tree node.
        /// </summary>
        public List<TreeNode> Nodes { get; } = new List<TreeNode>();

        /// <summary>
        /// Gets the IDebugElement in the tree node.
        /// </summary>
        public IDebugElement DebugElement { get; }

        /// <summary>
        /// Initializes a tree node with a IDebugElement representing the Xamarin.Forms DebugElement
        /// </summary>
        /// <param name="debugElement"></param>
        public TreeNode(IDebugElement debugElement)
        {
            DebugElement = debugElement;
        }

        /// <summary>
        /// Adds a tree node.
        /// </summary>
        public void AddNode(TreeNode treeNode) => Nodes.Add(treeNode);
    }
}
