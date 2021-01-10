using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        internal Screen(TPage page) => Container = page;

        public View? QueryByText(string text) => FindByText(text).SingleOrDefault();

        public IEnumerable<View> FindByText(string text)
        {
            var foundViews = new List<View>();

            foreach (var child in Container.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(FindByText(child, text));
            }

            return foundViews;
        }

        private static IEnumerable<View> FindByText(View view, string text)
        {
            var foundViews = new List<View>();

            var textValue = view.GetTextValueWith(text);
            if (textValue != null)
            {
                foundViews.Add(view);
            }

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(FindByText(child, text));
            }

            return foundViews;
        }
    }
}
