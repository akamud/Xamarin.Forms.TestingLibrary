using System;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Mocks;

namespace Xamarin.Forms.TestingLibrary
{
    public class Renderer<TApp>
        where TApp : Application
    {
        internal readonly TApp _app;

        public Renderer(bool skipMockFormsInit = false)
        {
            if (!skipMockFormsInit)
                MockForms.Init();
            _app = Activator.CreateInstance<TApp>();
        }

        public Screen<TPage> Render<TPage>() where TPage : Page => Render(Activator.CreateInstance<TPage>());

        public Screen<TPage> Render<TPage>(TPage page)
            where TPage : Page
        {
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Page cannot be null. Did you forget to pass a valid Page to your Renderer?");
            }

            _app.MainPage = page;

            return new Screen<TPage>(page);
        }

        public void Tap(View view, int numberOfTapsRequired = 1)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.GestureRecognizers.OfType<TapGestureRecognizer>()
                .Where(x => x.NumberOfTapsRequired == numberOfTapsRequired)
                .ForEach(x => x.SendTapped(view));
        }
    }
}
