namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public class LayoutOptionsValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => value is LayoutOptions;

        public string Format(object value)
        {
            var layoutOptions = (LayoutOptions)value;
            return $"{layoutOptions.Alignment}{(layoutOptions.Expands ? "AndExpand" : "")}";
        }
    }
}
