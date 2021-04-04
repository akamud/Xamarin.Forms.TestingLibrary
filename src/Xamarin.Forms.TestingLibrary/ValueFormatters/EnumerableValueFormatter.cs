using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.TestingLibrary.ValueFormatters
{
    internal class EnumerableValueFormatter : IValueFormatter
    {
        /// <summary>
        /// The number of items to include when formatting this object.
        /// </summary>
        /// <remarks>The default value is 5.</remarks>
        protected virtual int MaxItems { get; } = 5;

        /// <inheritdoc />
        public virtual bool CanHandle(object value) => value is IEnumerable;

        private static ICollection<T> ConvertOrCastToCollection<T>(IEnumerable source) =>
            source as ICollection<T> ?? source.Cast<T>().ToList();

        /// <inheritdoc />
        /// <summary>
        /// Formats a Enumerable property iterating through its elements.
        /// </summary>
        /// <returns>A list with the string representation of the Enumerable items.</returns>
        public string Format(object value)
        {
            var enumerable = ConvertOrCastToCollection<object>((IEnumerable)value);

            if (enumerable.Any())
            {
                string postfix = string.Empty;

                if (enumerable.Count > MaxItems)
                {
                    postfix = $", …{enumerable.Count - MaxItems} more…";
                    enumerable = enumerable.Take(MaxItems).ToArray();
                }

                return @$"{{{string.Join(", ", enumerable.Select(item =>
                {
                    var valueFormatter = TestingLibraryOptions.DebugOptions.DefaultValueFormatters
                        .First(x => x.CanHandle(item));
                    return valueFormatter.Format(item);
                }))}{postfix}}}";
            }

            return "{empty}";
        }
    }
}
