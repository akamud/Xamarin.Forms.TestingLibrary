using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.TestingLibrary.FormsProxies;

namespace Xamarin.Forms.TestingLibrary.Extensions
{
    internal static class BindableObjectExtensions
    {
        internal static IEnumerable<LocalValueEntry> GetLocalValueEntries(this BindableObject bindableObject)
        {
            var localValueEnumerator = bindableObject.GetType().GetMethod("GetLocalValueEnumerator",
                BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(bindableObject, new object[0]) as IEnumerator;

            while (localValueEnumerator?.MoveNext() == true)
            {
                var localValue = localValueEnumerator.Current;
                var localValueType = localValue!.GetType();

                yield return new LocalValueEntry(
                    localValueType.GetProperty("Property")?.GetValue(localValue) as BindableProperty,
                    localValueType.GetProperty("Value")?.GetValue(localValue),
                    (BindableContextAttributes)(localValueType.GetProperty("Attributes")?.GetValue(localValue) ??
                                                BindableContextAttributes.None));
            }
        }

        internal static string? GetTextContentValue(this BindableObject bindableObject)
        {
            var localValueEntries = bindableObject.GetLocalValueEntries().ToArray();

            return localValueEntries.FirstOrDefault(x => x.Property.PropertyName == "Text")?.Value?.ToString() ??
                   localValueEntries.FirstOrDefault(x => x.Property.PropertyName == "FormattedText")?.Value?.ToString();
        }

        internal static bool HasTextValueWith(this BindableObject bindableObject, string text) =>
            bindableObject.GetLocalValueEntries()
                .Any(x => x.Property.PropertyName == "Text" && (string)x.Value == text);

        internal static bool HasAutomationIdValueWith(this BindableObject bindableObject, string automationId) =>
            bindableObject.GetLocalValueEntries()
                .Any(x => x.Property.PropertyName == "AutomationId" && (string)x.Value == automationId);
    }
}
