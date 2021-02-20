namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public class DefaultValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => true;

        public string Format(object value) => value?.ToString() ?? "<null>";
    }
}
