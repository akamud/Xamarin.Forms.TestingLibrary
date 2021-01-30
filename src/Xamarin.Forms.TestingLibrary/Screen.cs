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

        public void ProvideBingingContext<T>(T viewModel) => Container.BindingContext = viewModel;

        public T? QueryByText<T>(string text) where T : View =>
            Container.GetPageHierarchy<T>().SingleOrDefault(x => x.HasTextValueWith(text));

        public View? QueryByText(string text) => QueryByText<View>(text);

        public IReadOnlyCollection<T> QueryAllByText<T>(string text) where T : View =>
            Container.GetPageHierarchy<T>().Where(x => x.HasTextValueWith(text)).ToList().AsReadOnly();

        public IReadOnlyCollection<View> QueryAllByText(string text) => QueryAllByText<View>(text);

        public T GetByText<T>(string text) where T : View =>
            Container.GetPageHierarchy<T>().Single(x => x.HasTextValueWith(text));

        public View GetByText(string text) => GetByText<View>(text);

        public IReadOnlyCollection<T> GetAllByText<T>(string text) where T : View
        {
            var foundViews = Container.GetPageHierarchy<T>().Where(x => x.HasTextValueWith(text)).ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException("Sequence contains no matching element");
        }

        public IReadOnlyCollection<View> GetAllByText(string text) => GetAllByText<View>(text);

        public T? QueryByType<T>() where T : View => Container.GetPageHierarchy<T>().SingleOrDefault();

        public IReadOnlyCollection<T> QueryAllByType<T>() where T : View =>
            Container.GetPageHierarchy<T>().ToList().AsReadOnly();

        public T GetByType<T>() where T : View => Container.GetPageHierarchy<T>().Single();

        public IReadOnlyCollection<T> GetAllByType<T>() where T : View
        {
            var foundViews = Container.GetPageHierarchy<T>().ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException("Sequence contains no elements");
        }
    }
}
