using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.FormsProxies;

namespace Xamarin.Forms.TestingLibrary
{
    [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        internal Screen(TPage page)
        {
            Container = page;
            debugTree = new Tree($"[yellow]{Container.GetType().Name}[/]");
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

        public void ProvideBingingContext<T>(T viewModel) => Container.BindingContext = viewModel;

        public T? QueryByText<T>(string text) where T : View =>
            QueryBy<T>(x => x.HasTextValueWith(text));

        public View? QueryByText(string text) => QueryByText<View>(text);

        public IReadOnlyCollection<T> QueryAllByText<T>(string text) where T : View =>
            QueryAllBy<T>(x => x.HasTextValueWith(text));

        public IReadOnlyCollection<View> QueryAllByText(string text) => QueryAllByText<View>(text);

        public T GetByText<T>(string text) where T : View
            => GetBy<T>(x => x.HasTextValueWith(text));

        public View GetByText(string text) => GetByText<View>(text);

        public IReadOnlyCollection<T> GetAllByText<T>(string text) where T : View
            => GetAllBy<T>(x => x.HasTextValueWith(text));

        public IReadOnlyCollection<View> GetAllByText(string text) => GetAllByText<View>(text);

        public T? QueryByType<T>() where T : View => QueryBy<T>();

        public IReadOnlyCollection<T> QueryAllByType<T>() where T : View =>
            QueryAllBy<T>();

        public T GetByType<T>() where T : View => GetBy<T>();

        public IReadOnlyCollection<T> GetAllByType<T>() where T : View
            => GetAllBy<T>(exceptionMessage: "Sequence contains no elements");

        public T? QueryByAutomationId<T>(string automationId) where T : View =>
            QueryBy<T>(x => x.HasAutomationIdValueWith(automationId));

        public View? QueryByAutomationId(string automationId) => QueryByAutomationId<View>(automationId);

        public IReadOnlyCollection<T> QueryAllByAutomationId<T>(string automationId) where T : View
            => QueryAllBy<T>(x => x.HasAutomationIdValueWith(automationId));

        public IReadOnlyCollection<View> QueryAllByAutomationId(string automationId)
            => QueryAllByAutomationId<View>(automationId);

        public T GetByAutomationId<T>(string automationId) where T : View
            => GetBy<T>(x => x.HasAutomationIdValueWith(automationId));

        public View GetByAutomationId(string automationId) => GetByAutomationId<View>(automationId);

        public IReadOnlyCollection<T> GetAllByAutomationId<T>(string automationId) where T : View
            => GetAllBy<T>(x => x.HasAutomationIdValueWith(automationId));

        public IReadOnlyCollection<View> GetAllByAutomationId(string automationId)
            => GetAllByAutomationId<View>(automationId);

        private Tree debugTree;
        private Tree currentNode = null;

        private void AddView(View view)
        {
            // if (currentNode == null)
            //     AddDebugTreeNode(debugTree, view);
            // else
            //     AddDebugTreeNode(currentNode, view);
        }

        private void AddChild(View view)
        {
            // if (currentNode == null)
            //     AddDebugTreeNode(debugTree, view);
            // else
            //     AddDebugTreeNode(currentNode, view);
        }

        private void AddDebugTreeNode(Tree viewTree, View view)
        {
            var values = view.GetLocalValueEntries()
                .Where(x => x.Attributes.HasFlag(BindableContextAttributes.IsManuallySet) ||
                            x.Attributes.HasFlag(BindableContextAttributes.IsSetFromStyle));

            values.ForEach(x =>
            {
                switch (x.Value)
                {
                    case Thickness t:
                        viewTree.AddNode(
                            $"{x.Property.PropertyName}: Left={t.Left}, Top={t.Top}, Right={t.Right}, Bottom={t.Bottom}");
                        break;
                    case LayoutOptions l:
                        viewTree.AddNode($"{x.Property.PropertyName}: {l.Alignment}{(l.Expands ? "AndExpands" : "")}");
                        break;
                    case Color c:
                        viewTree.AddNode($"{x.Property.PropertyName}: {c.ToHex()}");
                        break;
                    default:
                        viewTree.AddNode($"{x.Property.PropertyName}: {x.Value}");
                        break;
                }
            });
        }

        public void Debug()
        {
            var views = Container.GetPageHierarchy<View>();
            var tree = new Tree($"[yellow]{Container.GetType().Name}[/]");

            foreach (var view in views)
            {
                var viewTree = new Tree($"[blue]{view.GetType().Name}[/]");

                var values = view.GetLocalValueEntries()
                    .Where(x => x.Attributes.HasFlag(BindableContextAttributes.IsManuallySet) ||
                                x.Attributes.HasFlag(BindableContextAttributes.IsSetFromStyle));

                values.ForEach(x =>
                {
                    switch (x.Value)
                    {
                        case Thickness t:
                            viewTree.AddNode(
                                $"{x.Property.PropertyName}: Left={t.Left}, Top={t.Top}, Right={t.Right}, Bottom={t.Bottom}");
                            break;
                        case LayoutOptions l:
                            viewTree.AddNode($"{x.Property.PropertyName}: {l.Alignment}{(l.Expands ? "AndExpands" : "")}");
                            break;
                        case Color c:
                            viewTree.AddNode($"{x.Property.PropertyName}: {c.ToHex()}");
                            break;
                        default:
                            viewTree.AddNode($"{x.Property.PropertyName}: {x.Value}");
                            break;
                    }
                });

                tree.AddNode(viewTree);
            }

            AnsiConsole.Render(tree);
        }
    }
}
