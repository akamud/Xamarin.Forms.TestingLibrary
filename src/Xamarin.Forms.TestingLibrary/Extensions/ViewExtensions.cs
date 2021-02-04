using Spectre.Console;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.TestingLibrary.FormsProxies;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    public static class PageExtensions
    {
        public static IEnumerable<T> GetPageHierarchy<T>(this Page page) where T : View
        {
            var tree = new Tree($"[yellow]{page.GetType().Name}[/]");
            var hierarchy = page.LogicalChildren.OfType<View>().SelectMany(x => GetViewHierarchy<T>(x, tree)).ToList();
            AnsiConsole.Render(tree);

            return hierarchy;
        }

        public static string GetTextContent(this View view)
            => string.Join("", GetViewHierarchy<Label>(view, new Tree(view.GetType().Name)).Select(x => x.Text ?? x.FormattedText));

        private static IEnumerable<T> GetViewHierarchy<T>(View view, Tree tree) where T : View
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

            if (view is ListView listView)
            {
                foreach (var child in listView.TemplatedItems.SelectMany(x => x.LogicalChildren).OfType<View>())
                {
                    foreach (var nestedChild in GetViewHierarchy<View>(child, viewTree))
                    {
                        if (nestedChild is T typedChild)
                            yield return typedChild;
                    }
                }
            }
            else
            {
                foreach (var child in view.LogicalChildren.OfType<View>())
                {
                    foreach (var nestedChild in GetViewHierarchy<View>(child, viewTree))
                    {
                        if (nestedChild is T typedChild)
                            yield return typedChild;
                    }
                }
            }

            if (view is T typedView)
                yield return typedView;
        }
    }
}
