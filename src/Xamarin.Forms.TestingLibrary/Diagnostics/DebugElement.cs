using System.Collections.Generic;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    public class DebugElement : IDebugElement
    {
        public Element Element { get; set; }
        public IDictionary<string, string> PrintableProperties { get; set; }

        public DebugElement(Element element)
        {
            Element = element;
        }
    }
}
