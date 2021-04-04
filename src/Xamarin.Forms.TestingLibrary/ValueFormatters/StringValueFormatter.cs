namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class StringValueFormatter : IValueFormatter
    {
        /// <inheritdoc />
        public bool CanHandle(object value) => value is string;

        /// <inheritdoc />
        /// <summary>
        /// Formats a String property showing its value or &lt;null&gt;.
        /// </summary>
        /// <returns>A string representing the object or &lt;null&gt; when the property is null.</returns>
        public string Format(object value) => !string.IsNullOrEmpty((string)value) ? value.ToString() : "<null>";
    }
}
