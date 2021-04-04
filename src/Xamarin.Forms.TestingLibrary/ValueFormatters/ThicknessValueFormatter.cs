namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class ThicknessValueFormatter : IValueFormatter
    {
        /// <inheritdoc />
        public bool CanHandle(object value) => value is Thickness;

        /// <inheritdoc />
        /// <summary>
        /// Formats a Thickness property showing its value boundaries values.
        /// </summary>
        /// <returns>A string with all the Thickness values: Left, Top, Right, Bottom.</returns>
        public string Format(object value)
        {
            var (left, top, right, bottom) = (Thickness)value;
            return $"Left={left}, Top={top}, Right={right}, Bottom={bottom}";
        }
    }
}
