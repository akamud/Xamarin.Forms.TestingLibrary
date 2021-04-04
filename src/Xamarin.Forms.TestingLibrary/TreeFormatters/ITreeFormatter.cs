using Xamarin.Forms.TestingLibrary.Diagnostics;

namespace Xamarin.Forms.TestingLibrary.TreeFormatters
{
    /// <summary>
    /// Configures how the debug tree should be printed.
    /// <remarks>Implement this interface if you want to customize how the debug tree is printed.</remarks>
    /// </summary>
    public interface ITreeFormatter
    {
        /// <summary>
        /// Creates a string representing the debug tree and its properties.
        /// </summary>
        /// <param name="debugTree">The debug tree that was rendered.</param>
        /// <returns>A string representing the screen with all its elements and properties.</returns>
        string FormatTree(Tree debugTree);
    }
}
