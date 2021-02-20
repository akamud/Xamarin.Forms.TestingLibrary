using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms.TestingLibrary.FormsProxies;
using Xamarin.Forms.TestingLibrary.TreeFormatters;
using Xamarin.Forms.TestingLibrary.ValueFormatters;

namespace Xamarin.Forms.TestingLibrary.Options
{
    public class DebugOptions
    {
        public ITreeFormatter TreeFormatter { get; set; } = new SimpleTextFormatter();
        public TextWriter OutputTextWriter { get; set; } = Console.Out;

        public Func<LocalValueEntry, bool> DefaultPrintablePropertyFilter { get; set; } = x =>
            (x.Attributes.HasFlag(BindableContextAttributes.IsManuallySet) ||
            x.Attributes.HasFlag(BindableContextAttributes.IsSetFromStyle)) &&
            x.Property.PropertyName != "Renderer" && x.Property.PropertyName != "Navigation";

        public List<IValueFormatter> DefaultValueFormatters { get; set; } =
            new List<IValueFormatter>
            {
                new ThicknessValueFormatter(),
                new ColorValueFormatter(),
                new LayoutOptionsValueFormatter(),
                new DefaultValueFormatter()
            };
    }
}
