using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Diagnostics;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    /// <summary>
    /// Extensions used by Xamarin.Forms.TestingLibrary to help parse the Element tree.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Returns a single string representing all Texts and FormattedTexts from all its nested children.
        /// </summary>
        /// <param name="view">The Element that contains all the text content.</param>
        /// <returns>A single string representing all Texts and FormattedTexts from all its nested children.</returns>
        public static string GetTextContent(this View view) =>
            string.Join("", GetViewHierarchy<View>(view).Select(x => x.GetTextContentValue()).Where(x => x != null));

        internal static IEnumerable<T> GetPageHierarchy<T>(this Page page, Tree? renderTree = null) where T : View =>
            page.LogicalChildren.OfType<View>().SelectMany(view => GetViewHierarchy<T>(view, renderTree?._root));

        private static IEnumerable<T> GetViewHierarchy<T>(View view, TreeNode? renderTree = null) where T : View
        {
            if (view is ListView listView)
            {
                foreach (var child in listView.TemplatedItems.SelectMany(x => x.LogicalChildren).OfType<View>())
                {
                    foreach (var nestedChild in GetViewHierarchy<View>(child, renderTree))
                    {
                        if (nestedChild.IsVisible && nestedChild is T typedChild)
                            yield return typedChild;
                    }
                }
            }
            else
            {
                foreach (var child in view.LogicalChildren.OfType<View>())
                {
                    foreach (var nestedChild in GetViewHierarchy<View>(child, renderTree))
                    {
                        if (nestedChild.IsVisible && nestedChild is T typedChild)
                            yield return typedChild;
                    }
                }
            }

            if (view.IsVisible && view is T typedView)
            {
                StoreTreeNode(renderTree, view);
                yield return typedView;
            }
        }

        private static void StoreTreeNode(TreeNode? renderTree, View view) =>
            renderTree?.AddNode(new TreeNode(new DebugElement(view)));
    }
}
