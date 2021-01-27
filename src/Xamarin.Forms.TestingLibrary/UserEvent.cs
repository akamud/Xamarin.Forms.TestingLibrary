using System;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.TestingLibrary
{
    public static class UserEvent
    {
        public static void Tap(View view, int numberOfTapsRequired = 1)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.GestureRecognizers.OfType<TapGestureRecognizer>()
                .Where(x => x.NumberOfTapsRequired == numberOfTapsRequired)
                .ForEach(x => x.SendTapped(view));
        }
    }
}
