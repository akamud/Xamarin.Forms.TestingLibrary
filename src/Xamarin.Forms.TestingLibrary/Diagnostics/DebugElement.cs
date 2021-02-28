using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.Options;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    /// <inheritdoc />
    public class DebugElement : IDebugElement
    {
        /// <inheritdoc />
        public Element Element { get; set; }

        /// <inheritdoc />
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
