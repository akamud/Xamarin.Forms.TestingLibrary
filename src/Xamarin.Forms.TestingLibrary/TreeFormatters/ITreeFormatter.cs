using Xamarin.Forms.TestingLibrary.Diagnostics;

namespace Xamarin.Forms.TestingLibrary.TreeFormatters
{
    public interface ITreeFormatter
    {
        string FormatTree(Tree debugTree);
    }
}
