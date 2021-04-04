namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    /// <summary>
    /// Provides a way of customizing a specific type's formatting in the debug tree.
    /// </summary>
    public interface IValueFormatter
    {
        /// <summary>
        /// Checks if the formatter can be used for a specific property.
        /// </summary>
        /// <param name="value">The property being formatted.</param>
        /// <returns>True if the formatter can format the passed property's type. False if it can't.</returns>
        bool CanHandle(object value);

        /// <summary>
        /// Formats the passed property in a meaninful way for the debug tree.
        /// </summary>
        /// <param name="value">The property being formatted.</param>
        /// <returns>The string representation for the given property type.</returns>
        string Format(object value);
    }
}
