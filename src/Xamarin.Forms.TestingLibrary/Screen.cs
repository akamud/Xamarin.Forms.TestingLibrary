using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Diagnostics;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    /// <summary>
    /// The screen that represents the rendered page.
    /// </summary>
    /// <typeparam name="TPage">The Xamarin.Forms page.</typeparam>
    [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class Screen<TPage> where TPage : Page
    {
        /// <summary>
        /// The original Xamarin.Forms page that acts as a container for the queries and assertions.
        /// </summary>
        public TPage Container { get; }

        internal Screen(TPage page)
        {
            Container = page;
        }

        private T? QueryBy<T>(Func<T, bool>? predicate = null) where T : View
            => predicate != null
                ? Container.GetPageHierarchy<T>().SingleOrDefault(predicate)
                : Container.GetPageHierarchy<T>().SingleOrDefault();

        private IReadOnlyCollection<T> QueryAllBy<T>(Func<T, bool>? predicate = null) where T : View
            => predicate != null
                ? Container.GetPageHierarchy<T>().Where(predicate).ToList().AsReadOnly()
                : Container.GetPageHierarchy<T>().ToList().AsReadOnly();

        private T GetBy<T>(Func<T, bool>? predicate = null) where T : View =>
            predicate != null
                ? Container.GetPageHierarchy<T>().Single(predicate)
                : Container.GetPageHierarchy<T>().Single();

        private IReadOnlyCollection<T> GetAllBy<T>(Func<T, bool>? predicate = null,
            string exceptionMessage = "Sequence contains no matching element") where T : View
        {
            var foundViews = predicate != null
                ? Container.GetPageHierarchy<T>().Where(predicate).ToList()
                : Container.GetPageHierarchy<T>().ToList();

            return foundViews.Count > 0
                ? foundViews.AsReadOnly()
                : throw new InvalidOperationException(exceptionMessage);
        }

        /// <summary>
        /// Convinience method that substitutes the page's BindingContext to allow mocking the ViewModel
        /// and controlling the page behavior.
        /// </summary>
        /// <typeparam name="T">The type of the ViewModel object</typeparam>
        /// <param name="viewModel">The ViewModel that will be used as the page's BindingContext</param>
        public void ProvideBingingContext<T>(T viewModel) => Container.BindingContext = viewModel;

        /// <summary>
        /// Returns the only <typeparamref name="T"/> on the screen matching the given <paramref name="text"/>
        /// or null if there are no matching elements.
        /// Throws an exception if there is more than one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <param name="text">The text that the view should have on the screen.</param>
        /// <returns>The single <typeparamref name="T"/> matching the <paramref name="text"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element.
        /// </exception>
        public T? QueryByText<T>(string text) where T : View =>
            QueryBy<T>(x => x.HasTextValueWith(text));

        /// <summary>
        /// Returns the only view on the screen matching the given <paramref name="text"/>
        /// or null if there are no matching elements.
        /// Throws an exception if there is more than one matching element on the screen.
        /// </summary>
        /// <param name="text">The text that the view should have on the screen.</param>
        /// <returns>The single view matching the <paramref name="text"/>.</returns>
        /// <para>For a typed version, use <see cref="QueryByText{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element.
        /// </exception>
        public View? QueryByText(string text) => QueryByText<View>(text);

        /// <summary>
        /// Returns all <typeparamref name="T"/> on the screen matching the given <paramref name="text"/>
        /// or an empty collection if there are no matching elements.
        /// </summary>
        /// <typeparam name="T">The type of the expected views.</typeparam>
        /// <param name="text">The text that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="text"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        public IReadOnlyCollection<T> QueryAllByText<T>(string text) where T : View =>
            QueryAllBy<T>(x => x.HasTextValueWith(text));

        /// <summary>
        /// Returns all views on the screen matching the given <paramref name="text"/>
        /// or an empty collection if there are no matching elements.
        /// </summary>
        /// <param name="text">The text that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="text"/>.</returns>
        /// <para>For a typed version, use <see cref="QueryAllByText{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        public IReadOnlyCollection<View> QueryAllByText(string text) => QueryAllByText<View>(text);

        /// <summary>
        /// Returns the only <typeparamref name="T"/> on the screen matching the given <paramref name="text"/>.
        /// Throws an exception if there is not exactly one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <param name="text">The text that the view should have on the screen.</param>
        /// <returns>The single <typeparamref name="T"/> matching the <paramref name="text"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element. -or- There are no matching elements.
        /// </exception>
        public T GetByText<T>(string text) where T : View
            => GetBy<T>(x => x.HasTextValueWith(text));

        /// <summary>
        /// Returns the only view on the screen matching the given <paramref name="text"/>.
        /// Throws an exception if there is not exactly one matching element on the screen.
        /// </summary>
        /// <param name="text">The text that the view should have on the screen.</param>
        /// <returns>The single view matching the <paramref name="text"/>.</returns>
        /// <para>For a typed version, use <see cref="GetByText{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element. -or- There are no matching elements.
        /// </exception>
        public View GetByText(string text) => GetByText<View>(text);

        /// <summary>
        /// Returns all <typeparamref name="T"/> on the screen matching the given <paramref name="text"/>.
        /// Throws an exception if there is not at least one matching element on the screen.
        /// </summary>
        /// <param name="text">The text that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="text"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen does not contain at least one matching element.
        /// </exception>
        public IReadOnlyCollection<T> GetAllByText<T>(string text) where T : View
            => GetAllBy<T>(x => x.HasTextValueWith(text));

        /// <summary>
        /// Returns all views on the screen matching the given <paramref name="text"/>.
        /// Throws an exception if there is not at least one matching element on the screen.
        /// </summary>
        /// <param name="text">The text that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="text"/>.</returns>
        /// <para>For a typed version, use <see cref="GetAllByText{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen does not contain at least one matching element.
        /// </exception>
        public IReadOnlyCollection<View> GetAllByText(string text) => GetAllByText<View>(text);

        /// <summary>
        /// Returns the only view on the screen matching the given type <typeparamref name="T"/>
        /// or null if there are no matching elements.
        /// Throws an exception if there is more than one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <returns>The single view matching the type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element.
        /// </exception>
        public T? QueryByType<T>() where T : View => QueryBy<T>();

        /// <summary>
        /// Returns all views matching the type <typeparamref name="T"/> on the screen
        /// or an empty collection if there are no matching elements.
        /// </summary>
        /// <typeparam name="T">The type of the expected views.</typeparam>
        /// <returns>A IReadOnlyCollection of views matching the type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        public IReadOnlyCollection<T> QueryAllByType<T>() where T : View =>
            QueryAllBy<T>();

        /// <summary>
        /// Returns the only view matching the type <typeparamref name="T"/> on the screen.
        /// Throws an exception if there is not exactly one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <returns>The single view matching the type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element. -or- There are no matching elements.
        /// </exception>
        public T GetByType<T>() where T : View => GetBy<T>();

        /// <summary>
        /// Returns all views matching the type <typeparamref name="T"/> on the screen.
        /// Throws an exception if there is not at least one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected views.</typeparam>
        /// <returns>A IReadOnlyCollection of views matching the type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen does not contain at least one matching element.
        /// </exception>
        public IReadOnlyCollection<T> GetAllByType<T>() where T : View
            => GetAllBy<T>(exceptionMessage: "Sequence contains no elements");

        /// <summary>
        /// Returns the only <typeparamref name="T"/> on the screen matching the given <paramref name="automationId"/>
        /// or null if there are no matching elements.
        /// Throws an exception if there is more than one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <param name="automationId">The AutomationId that the view should have on the screen.</param>
        /// <returns>The single <typeparamref name="T"/> matching the <paramref name="automationId"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element.
        /// </exception>
        public T? QueryByAutomationId<T>(string automationId) where T : View =>
            QueryBy<T>(x => x.HasAutomationIdValueWith(automationId));

        /// <summary>
        /// Returns the only view on the screen matching the given <paramref name="automationId"/>
        /// or null if there are no matching elements.
        /// Throws an exception if there is more than one matching element on the screen.
        /// </summary>
        /// <param name="automationId">The AutomationId that the view should have on the screen.</param>
        /// <returns>The single view matching the <paramref name="automationId"/>.</returns>
        /// <para>For a typed version, use <see cref="QueryByAutomationId{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element.
        /// </exception>
        public View? QueryByAutomationId(string automationId) => QueryByAutomationId<View>(automationId);

        /// <summary>
        /// Returns all <typeparamref name="T"/> on the screen matching the given <paramref name="automationId"/>
        /// or an empty collection if there are no matching elements.
        /// </summary>
        /// <typeparam name="T">The type of the expected views.</typeparam>
        /// <param name="automationId">The AutomationId that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="automationId"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        public IReadOnlyCollection<T> QueryAllByAutomationId<T>(string automationId) where T : View
            => QueryAllBy<T>(x => x.HasAutomationIdValueWith(automationId));

        /// <summary>
        /// Returns all views on the screen matching the given <paramref name="automationId"/>
        /// or an empty collection if there are no matching elements.
        /// </summary>
        /// <param name="automationId">The AutomationId that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="automationId"/>.</returns>
        /// <para>For a typed version, use <see cref="QueryAllByAutomationId{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        public IReadOnlyCollection<View> QueryAllByAutomationId(string automationId)
            => QueryAllByAutomationId<View>(automationId);

        /// <summary>
        /// Returns the only <typeparamref name="T"/> on the screen matching the given <paramref name="automationId"/>.
        /// Throws an exception if there is not exactly one matching element on the screen.
        /// </summary>
        /// <typeparam name="T">The type of the expected view.</typeparam>
        /// <param name="automationId">The AutomationId that the view should have on the screen.</param>
        /// <returns>The single <typeparamref name="T"/> matching the <paramref name="automationId"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element. -or- There are no matching elements.
        /// </exception>
        public T GetByAutomationId<T>(string automationId) where T : View
            => GetBy<T>(x => x.HasAutomationIdValueWith(automationId));

        /// <summary>
        /// Returns the only view on the screen matching the given <paramref name="automationId"/>.
        /// Throws an exception if there is not exactly one matching element on the screen.
        /// </summary>
        /// <param name="automationId">The AutomationId that the view should have on the screen.</param>
        /// <returns>The single view matching the <paramref name="automationId"/>.</returns>
        /// <para>For a typed version, use <see cref="GetByAutomationId{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen contains more than one matching element. -or- There are no matching elements.
        /// </exception>
        public View GetByAutomationId(string automationId) => GetByAutomationId<View>(automationId);

        /// <summary>
        /// Returns all <typeparamref name="T"/> on the screen matching the given <paramref name="automationId"/>.
        /// Throws an exception if there is not at least one matching element on the screen.
        /// </summary>
        /// <param name="automationId">The AutomationId that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="automationId"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen does not contain at least one matching element.
        /// </exception>
        public IReadOnlyCollection<T> GetAllByAutomationId<T>(string automationId) where T : View
            => GetAllBy<T>(x => x.HasAutomationIdValueWith(automationId));

        /// <summary>
        /// Returns all views on the screen matching the given <paramref name="automationId"/>.
        /// Throws an exception if there is not at least one matching element on the screen.
        /// </summary>
        /// <param name="automationId">The AutomationId that the views should have on the screen.</param>
        /// <returns>A IReadOnlyCollection of views matching the <paramref name="automationId"/>.</returns>
        /// <para>For a typed version, use <see cref="GetAllByAutomationId{T}(string)"/></para>
        /// <exception cref="System.ArgumentNullException">
        /// Screen does not have a valid view hierarchy.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The screen does not contain at least one matching element.
        /// </exception>
        public IReadOnlyCollection<View> GetAllByAutomationId(string automationId)
            => GetAllByAutomationId<View>(automationId);

        public void Debug()
        {
            var renderedHierarchy = new Tree(Container);
            Container.GetPageHierarchy<View>(renderedHierarchy).ToList();

            PrintNode(renderedHierarchy._root);
        }

        private void PrintNode(TreeNode treeNode)
        {
            Console.WriteLine(treeNode.DebugElement.Element.GetType().Name);

            foreach (var childNode in treeNode.Nodes)
            {
                PrintNode(childNode);
            }
        }
    }
}
