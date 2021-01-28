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
            Container.Find<T>(x => x.HasTextValueWith(text)).SingleOrDefault();

        public View? QueryByText(string text) => QueryByText<View>(text);

        public IReadOnlyCollection<T> QueryAllByText<T>(string text) where T : View =>
            Container.Find<T>(x => x.HasTextValueWith(text)).ToList().AsReadOnly();

        public IReadOnlyCollection<View> QueryAllByText(string text) => QueryAllByText<View>(text);

        public T GetByText<T>(string text) where T : View => Container.Find<T>(x => x.HasTextValueWith(text)).Single();

        public View GetByText(string text) => GetByText<View>(text);

        public IReadOnlyCollection<T> GetAllByText<T>(string text) where T : View
        {
            var foundViews = Container.Find<T>(x => x.HasTextValueWith(text)).ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException("Sequence contains no elements");
        }

        public IReadOnlyCollection<View> GetAllByText(string text) => GetAllByText<View>(text);
    }
}
