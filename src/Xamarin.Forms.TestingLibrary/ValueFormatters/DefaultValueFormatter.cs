namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class DefaultValueFormatter : IValueFormatter
    {
        /// <inheritdoc />
        public bool CanHandle(object value) => true;

        /// <inheritdoc />
        /// <summary>
        /// Formats a any property that doesn't have a specific formatter specified.
        /// </summary>
        /// <returns>A string representation of the object or <null> when it is null.</returns>
        public string Format(object value) => value?.ToString() ?? "<null>";
    }
}
