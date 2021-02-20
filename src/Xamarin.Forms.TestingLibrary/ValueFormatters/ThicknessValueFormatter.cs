namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    public class ThicknessValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => value is Thickness;

        public string Format(object value)
        {
            var (left, top, right, bottom) = (Thickness)value;
            return $"Left={left}, Top={top}, Right={right}, Bottom={bottom}";
        }
    }
}
