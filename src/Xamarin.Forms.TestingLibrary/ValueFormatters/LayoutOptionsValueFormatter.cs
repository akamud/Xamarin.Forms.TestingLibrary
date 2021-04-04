namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class LayoutOptionsValueFormatter : IValueFormatter
    {
        /// <inheritdoc />
        public bool CanHandle(object value) => value is LayoutOptions;

        /// <inheritdoc />
        /// <summary>
        /// Formats a LayoutOptions property correctly.
        /// </summary>
        /// <returns>The LayoutOptions chosen.</returns>
        public string Format(object value)
        {
            var layoutOptions = (LayoutOptions)value;
            return $"{layoutOptions.Alignment}{(layoutOptions.Expands ? "AndExpand" : "")}";
        }
    }
}
