using System;
using System.Globalization;

namespace Xamarin.Forms.TestingLibrary.SampleApp.Converters
{
    public class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null || (value is string str && string.IsNullOrWhiteSpace(str));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
