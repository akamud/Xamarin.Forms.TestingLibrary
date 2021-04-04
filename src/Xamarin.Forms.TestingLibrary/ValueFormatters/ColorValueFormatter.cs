namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class ColorValueFormatter : IValueFormatter
    {
        /// <inheritdoc />
        public bool CanHandle(object value) => value is Color;

        /// <inheritdoc />
        /// <summary>
        /// Formats a Color property showing its Hex value.
        /// </summary>
        /// <returns>The Hex representation of the passed Color.</returns>
        public string Format(object value) => ((Color)value).ToHex();
    }
}
