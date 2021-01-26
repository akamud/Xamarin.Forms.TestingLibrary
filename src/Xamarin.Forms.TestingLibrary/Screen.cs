using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        internal Screen(TPage page) => Container = page;

        private IEnumerable<View> FindByText(string text)
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

            if (view.HasTextValueWith(text))
            {
                foundViews.Add(view);
            }

            foreach (var child in view.LogicalChildren.OfType<View>())
            {
                foundViews.AddRange(FindByText(child, text));
            }

            return foundViews;
        }

        public View? QueryByText(string text) => FindByText(text).SingleOrDefault();
        public IReadOnlyCollection<View> QueryAllByText(string text) => FindByText(text).ToList().AsReadOnly();
        public View GetByText(string text) => FindByText(text).Single();
        public IReadOnlyCollection<View> GetAllByText(string text)
        {
            var foundViews = FindByText(text).ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException("Sequence contains no elements");
        }
    }
}
