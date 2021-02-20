using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.FormsProxies;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    public class DebugElement : IDebugElement
    {
        public Element Element { get; set; }

        public IDictionary<string, object> PrintableProperties => Element.GetLocalValueEntries()
            .Where(x => TestingLibraryOptions.DebugOptions.DefaultPrintablePropertyFilter(x))
            .ToDictionary(x => x.Property.PropertyName, x => x.Value);

        public DebugElement(Element element) => Element = element;
    }
}
