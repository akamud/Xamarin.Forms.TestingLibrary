using System;

namespace Xamarin.Forms.TestingLibrary.FormsProxies
{
    [Flags]
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
}