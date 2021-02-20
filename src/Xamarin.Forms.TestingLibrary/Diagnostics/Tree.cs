using System.Collections.Generic;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    /// <summary>
    /// Representation of the page view hierarchy in a tree.
    /// Each node represents a view in the hierarchy.
    /// </summary>
    public class Tree
    {
        internal readonly TreeNode _root;

        /// <summary>
        /// Gets the tree's child nodes.
        /// </summary>
        public List<TreeNode> Nodes => _root.Nodes;

        /// <summary>
        /// Initializes the three with the passed Xamarin.Forms Element as root.
        /// </summary>
        public Tree(Element element) : this(new DebugElement(element)) { }

        private Tree(IDebugElement element) => _root = new TreeNode(element);
    }
}
