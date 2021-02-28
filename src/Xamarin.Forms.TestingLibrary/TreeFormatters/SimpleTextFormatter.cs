using Spectre.Console;
using System.IO;
using System.Linq;
using SpectreTree = Spectre.Console.Tree;
using SpectreTreeNode = Spectre.Console.TreeNode;
using Tree = Xamarin.Forms.TestingLibrary.Diagnostics.Tree;
using TreeNode = Xamarin.Forms.TestingLibrary.Diagnostics.TreeNode;

namespace Xamarin.Forms.TestingLibrary.TreeFormatters
{
    internal class SimpleTextFormatter : ITreeFormatter
    {
        private readonly AnsiConsoleSettings _ansiConsoleSettings = new AnsiConsoleSettings
        {
            Ansi = AnsiSupport.Detect,
            Interactive = InteractionSupport.No,
            ColorSystem = ColorSystemSupport.NoColors,
            Enrichment = {UseDefaultEnrichers = false},
        };

        public string FormatTree(Tree debugTree)
        {
            using var stringBuffer = new StringWriter();
            _ansiConsoleSettings.Out = stringBuffer;
            var ansiConsole = AnsiConsole.Create(_ansiConsoleSettings);

            var consoleTree = new SpectreTree(debugTree.Root.DebugElement.Element.GetType().Name);
            foreach (var rootNode in debugTree.Nodes)
            {
                FormatTreeNode(rootNode, consoleTree);
            }

            ansiConsole.Render(consoleTree);
            return stringBuffer.ToString();
        }

        private void FormatTreeNode(TreeNode debugTreeNode, IHasTreeNodes spectreTreeNode)
        {
            var childNode = spectreTreeNode.AddNode(debugTreeNode.DebugElement.Element.GetType().Name);
            foreach (var printableProperty in debugTreeNode.DebugElement.PrintableProperties)
            {
                var valueFormatter = TestingLibraryOptions.DebugOptions.DefaultValueFormatters.First(x =>
                    x.CanHandle(printableProperty.Value));
                childNode.AddNode(EscapeMarkup($"{printableProperty.Key}: {valueFormatter.Format(printableProperty.Value)}"));
            }

            foreach (var treeNode in debugTreeNode.Nodes)
            {
                FormatTreeNode(treeNode, childNode);
            }
        }

        private string EscapeMarkup(string text) => text.Replace("[", "[[").Replace("]", "]]");
    }
}
