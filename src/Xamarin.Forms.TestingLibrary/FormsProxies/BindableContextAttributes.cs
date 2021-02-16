using System;

namespace Xamarin.Forms.TestingLibrary.FormsProxies
{
    [Flags]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum BindableContextAttributes
    {
        None = 0,
        IsManuallySet = 1 << 0,
        IsBeingSet = 1 << 1,
        IsDynamicResource = 1 << 2,
        IsSetFromStyle = 1 << 3,
        IsDefaultValue = 1 << 4,
        IsDefaultValueCreated = 1 << 5,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
