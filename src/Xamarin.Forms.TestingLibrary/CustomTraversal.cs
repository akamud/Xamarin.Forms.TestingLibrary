using System;
using System.Collections.Generic;

namespace Xamarin.Forms.TestingLibrary
{
    public class Config
    {
        public Func<View, IEnumerable<View>> Visitor { get; set; }
        public Action<View> Init { get; set; }
        public Type Type { get; set; }

        public Config(Type type, Action<View> init, Func<View, IEnumerable<View>> visitor)
        {
            Init = init;
            Type = type;
            Visitor = visitor;
        }
    }
}
