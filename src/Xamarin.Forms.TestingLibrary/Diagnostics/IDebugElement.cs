using System.Collections.Generic;
using Xamarin.Forms.TestingLibrary.Options;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    /// <summary>
    /// Represents an element used in Screen <see cref="Screen{TPage}.Debug"/>.
    /// </summary>
    public interface IDebugElement
    {
        /// <summary>
        /// The Xamarin.Forms Element found while debugging the screen.
        /// <remarks>You can use this if you want to look at any property from the original element.</remarks>
        /// </summary>
        Element Element { get; set; }

        /// <summary>
        /// Returns all relevant Bindable Properties of the original element to be printed in <see cref="Screen{TPage}.Debug"/>.
        /// <para>While you can get the bindable properties by yourself, this property already tries to filter
        /// out properties that could cause too much "noise" in your debug tree.</para>
        /// <remarks>If you want, you can customize which properties are filtered,
        /// using <see cref="TestingLibraryOptions.DebugOptions"/>.<see cref="DebugOptions.DefaultPrintablePropertyFilter"/>.</remarks>
        /// </summary>
        IDictionary<string, object> PrintableProperties { get; }
    }
}
