namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public class ColorValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => value is Color;

        public string Format(object value) => ((Color)value).ToHex();
    }
}
