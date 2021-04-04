using Xamarin.Forms.TestingLibrary.Options;

namespace Xamarin.Forms.TestingLibrary
{
    /// <summary>
    /// Controls how the TestingLibrary behaves.
    /// <remarks>You can customize some behaviors in the TestingLibrary using this static configuration.</remarks>
    /// </summary>
    public static class TestingLibraryOptions
    {
        /// <summary>
        /// Controls how the <see cref="Screen{TPage}.Debug"/> behaves.
        /// </summary>
        public static DebugOptions DebugOptions { get; set; } = new DebugOptions();
    }
}
