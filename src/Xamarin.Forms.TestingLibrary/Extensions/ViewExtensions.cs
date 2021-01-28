using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    public static class ViewExtensions
    {
        public static IEnumerable<T> Find<T>(this Page view, Func<T, bool> predicate) where T : View
        {
            var foundViews = new List<T>();

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(Find(child, predicate));
            }

            return foundViews;
        }

        private static IEnumerable<T> Find<T>(View view, Func<T, bool> predicate) where T : View
        {
            var foundViews = new List<T>();

            if (view is T typedView && predicate(typedView))
            {
                foundViews.Add(typedView);
            }

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(Find(child, predicate));
            }

            return foundViews;
        }
    }
}
