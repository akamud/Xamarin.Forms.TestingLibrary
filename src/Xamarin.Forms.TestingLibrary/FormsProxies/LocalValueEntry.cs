using System.Diagnostics;

namespace Xamarin.Forms.TestingLibrary.FormsProxies
{
    [DebuggerDisplay("{" + nameof(Property) + "}: {" + nameof(Value) + "}")]
    public class LocalValueEntry
    {
        public LocalValueEntry(BindableProperty property, object value, BindableContextAttributes attributes)
        {
            Property = property;
            Value = value;
            Attributes = attributes;
        }

        public BindableProperty Property { get; }
        public object Value { get; }
        public BindableContextAttributes Attributes { get; }
    }
}