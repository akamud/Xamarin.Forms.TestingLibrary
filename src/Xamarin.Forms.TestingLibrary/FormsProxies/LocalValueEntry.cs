using System.Diagnostics;

namespace Xamarin.Forms.TestingLibrary.FormsProxies
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
