using System;

namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public interface IValueFormatter
    {
        bool CanHandle(object value);

        string Format(object value);
    }
}
