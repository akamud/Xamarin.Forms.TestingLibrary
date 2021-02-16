using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    /// <summary>
    /// Extensions used by Xamarin.Forms.TestingLibrary to help parse the View tree.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Returns a single string representing all Texts and FormattedTexts from all its nested children.
        /// </summary>
        /// <param name="view">The View that contains all the text content.</param>
        /// <returns>A single string representing all Texts and FormattedTexts from all its nested children.</returns>
        public static string GetTextContent(this View view) =>
            string.Join("", GetViewHierarchy<View>(view).Select(x => x.GetTextContentValue()).Where(x => x != null));

        internal static IEnumerable<T> GetPageHierarchy<T>(this Page page) where T : View =>
            page.LogicalChildren.OfType<View>().SelectMany(GetViewHierarchy<T>);

        private static IEnumerable<T> GetViewHierarchy<T>(View view) where T : View
        {
            if (view is ListView listView)
            {
                foreach (var child in listView.TemplatedItems.SelectMany(x => x.LogicalChildren).OfType<View>())
                {
                    foreach (var nestedChild in GetViewHierarchy<View>(child))
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
                    foreach (var nestedChild in GetViewHierarchy<View>(child))
                    {
                        if (nestedChild.IsVisible && nestedChild is T typedChild)
                            yield return typedChild;
                    }
                }
            }

            if (view.IsVisible && view is T typedView)
                yield return typedView;
        }
    }
}
