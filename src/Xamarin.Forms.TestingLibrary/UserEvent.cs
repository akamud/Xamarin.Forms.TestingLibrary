using System;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.TestingLibrary
{
    public static class UserEvent
    {
        public static void Tap(View view, int numberOfTapsRequired = 1)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.GestureRecognizers.GetGesturesFor<TapGestureRecognizer>(x =>
                    x.NumberOfTapsRequired == numberOfTapsRequired)
                .ForEach(x => x.SendTapped(view));
        }
    }
}
