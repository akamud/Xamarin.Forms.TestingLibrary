using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        internal Screen(TPage page) => Container = page;

        public View? QueryByText(string text)
        {
            List<View> foundViews = new List<View>();

            foreach (var child in Container.LogicalChildren.OfType<View>())
            {
                FindByTextInternal(child, text, foundViews);
            }

            if (foundViews.Count > 1)
                throw new InvalidOperationException($"More than one element found with Text: {{{text}}}");

            return foundViews.FirstOrDefault();
        }

        private static void FindByTextInternal(View view, string text, ICollection<View> foundViews)
        {
            var queriedView = QueryTextProperty(view, text);

            if (queriedView != null)
            {
                foundViews.Add(queriedView);
            }

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                FindByTextInternal(child, text, foundViews);
            }
        }

        private static View? QueryTextProperty(View view, string text)
        {
            var localValue = view.GetLocalValueEntries()
                .FirstOrDefault(x => x.Property.PropertyName == "Text" && (string)x.Value == text);

            return localValue != null ? view : null;
        }
    }
}
