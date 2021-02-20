using System.Collections.Generic;

namespace Xamarin.Forms.TestingLibrary.Diagnostics
{
    public interface IDebugElement
    {
        Element Element { get; set; }
        IDictionary<string, object> PrintableProperties { get; }
    }
}
