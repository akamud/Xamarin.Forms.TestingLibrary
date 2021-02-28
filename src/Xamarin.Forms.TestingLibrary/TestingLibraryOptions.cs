using Xamarin.Forms.TestingLibrary.Options;

namespace Xamarin.Forms.TestingLibrary
{
    public static class TestingLibraryOptions
    {
        /// <summary>
        /// Controls how the <see cref="Screen{TPage}.Debug"/> behaves.
        /// </summary>
        public static DebugOptions DebugOptions { get; set; } = new DebugOptions();
    }
}
