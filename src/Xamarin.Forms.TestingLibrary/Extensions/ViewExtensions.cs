using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    public static class PageExtensions
    {
        public static IEnumerable<T> GetPageHierarchy<T>(this Page page) where T : View =>
            page.LogicalChildren.OfType<View>().SelectMany(GetViewHierarchy<T>);

        private static IEnumerable<T> GetViewHierarchy<T>(View view) where T : View
        {
            if (view is T typedView)
                yield return typedView;

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foreach (var nestedChild in GetViewHierarchy<View>(child))
                {
                    if (nestedChild is T typedChild)
                        yield return typedChild;
                }
            }
        }
    }
}
