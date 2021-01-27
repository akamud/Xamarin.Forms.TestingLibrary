using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        internal Screen(TPage page) => Container = page;

        private IEnumerable<T> FindByText<T>(string text) where T : View
        {
            var foundViews = new List<T>();

            foreach (var child in Container.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(FindByText<T>(child, text));
            }

            return foundViews;
        }

        private static IEnumerable<T> FindByText<T>(View view, string text) where T : View
        {
            var foundViews = new List<T>();

            if (view is T typedView && typedView.HasTextValueWith(text))
            {
                foundViews.Add(typedView);
            }

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(FindByText<T>(child, text));
            }

            return foundViews;
        }

        public T? QueryByText<T>(string text) where T : View => FindByText<T>(text).SingleOrDefault();
        public View? QueryByText(string text) => QueryByText<View>(text);

        public IReadOnlyCollection<T> QueryAllByText<T>(string text) where T : View =>
            FindByText<T>(text).ToList().AsReadOnly();

        public IReadOnlyCollection<View> QueryAllByText(string text) => QueryAllByText<View>(text);
        public T GetByText<T>(string text) where T : View => FindByText<T>(text).Single();
        public View GetByText(string text) => GetByText<View>(text);

        public IReadOnlyCollection<T> GetAllByText<T>(string text) where T : View
        {
            var foundViews = FindByText<T>(text).ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException("Sequence contains no elements");
        }

        public IReadOnlyCollection<View> GetAllByText(string text) => GetAllByText<View>(text);
    }
}
