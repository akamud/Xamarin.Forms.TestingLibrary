using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.Options;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    /// <summary>
    /// Represents an element used in Screen <see cref="Screen{TPage}.Debug"/>.
    /// </summary>
    public class DebugElement : IDebugElement
    {
        /// <summary>
        /// The Xamarin.Forms Element found while debugging the screen.
        /// <remarks>You can use this if you want to look at any property from the original element.</remarks>
        /// </summary>
        public Element Element { get; set; }

        /// <summary>
        /// Returns all relevant Bindable Properties of the original element to be printed in <see cref="Screen{TPage}.Debug"/>.
        /// <para>While you can get the bindable properties by yourself, this property already tries to filter
        /// out properties that could cause too much "noise" in your debug tree.</para>
        /// <remarks>If you want to control which properties are filtered, you can customize this
        /// behavior using <see cref="TestingLibraryOptions.DebugOptions"/>.<see cref="DebugOptions.DefaultPrintablePropertyFilter"/>.</remarks>
        /// </summary>
        public IDictionary<string, object> PrintableProperties =>
            Element.GetLocalValueEntries()
                .Where(x => TestingLibraryOptions.DebugOptions.DefaultPrintablePropertyFilter(x))
                .ToDictionary(x => x.Property.PropertyName, x => x.Value);

        /// <summary>
        /// Creates a <see cref="DebugElement"/> containing the original Xamarin.Forms Element.
        /// </summary>
        /// <param name="element"></param>
        public DebugElement(Element element) => Element = element;
    }
}
