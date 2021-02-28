using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms.TestingLibrary.FormsProxies;
using Xamarin.Forms.TestingLibrary.TreeFormatters;
using Xamarin.Forms.TestingLibrary.ValueFormatters;

namespace Xamarin.Forms.TestingLibrary.Options
{
    /// <summary>
    /// Controls how the <see cref="Screen{TPage}.Debug"/> behaves.
    /// <remarks>You can customize all debugging behavior using the static <see cref="TestingLibraryOptions"/>.</remarks>
    /// </summary>
    public class DebugOptions
    {
        /// <summary>
        /// Configures how the debug tree should be printed.
        /// <para>Default behavior is printing a simple tree-view using Spectre.Console.</para>
        /// <remarks>You can customize this behavior by passing your own <see cref="ITreeFormatter"/>.</remarks>
        /// </summary>
        public ITreeFormatter TreeFormatter { get; set; } = new SimpleTextFormatter();

        /// <summary>
        /// Configures where the debug tree should be printed.
        /// <para>Default behavior is printing directly into the <see cref="Console"/>.</para>
        /// <remarks>You can customize this behavior by passing your own <see cref="TextWriter"/>.</remarks>
        /// </summary>
        public TextWriter OutputTextWriter { get; set; } = Console.Out;

        /// <summary>
        /// Configures which bindable properties should be filtered out to reduce noise in the debug tree.
        /// <para>Default behavior is showing only bindable properties that are not filled with its default values,
        /// and that are not the properties: Renderer or Navigation.</para>
        /// <remarks>You can customize this behavior by passing your own filter.</remarks>
        /// </summary>
        public Func<LocalValueEntry, bool> DefaultPrintablePropertyFilter { get; set; } = x =>
            (x.Attributes.HasFlag(BindableContextAttributes.IsManuallySet) ||
             x.Attributes.HasFlag(BindableContextAttributes.IsSetFromStyle) ||
             x.Attributes == BindableContextAttributes.None) &&
            x.Property.PropertyName != "Renderer" && x.Property.PropertyName != "Navigation";

        /// <summary>
        /// Configures how to print each bindable property, trying to format it in a more meaninful way.
        /// <para>Automatically formats most used Xamarin.Forms properties</para>
        /// <remarks>You can add your own formatters and remove existing formatters by passing your own <see cref="IValueFormatter"/> List.</remarks>
        /// </summary>
        public List<IValueFormatter> DefaultValueFormatters { get; set; } =
            new List<IValueFormatter>
            {
                new ThicknessValueFormatter(),
                new ColorValueFormatter(),
                new LayoutOptionsValueFormatter(),
                new StringValueFormatter(),
                new EnumerableValueFormatter(),
                new DefaultValueFormatter()
            };
    }
}
