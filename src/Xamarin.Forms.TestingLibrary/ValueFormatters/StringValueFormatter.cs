namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public class StringValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => value is string;

        public string Format(object value) => !string.IsNullOrEmpty((string)value) ? value.ToString() : "<null>";
    }
}
