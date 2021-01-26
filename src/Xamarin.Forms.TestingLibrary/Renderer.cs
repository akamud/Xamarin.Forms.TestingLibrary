using System;

namespace Xamarin.Forms.TestingLibrary
{
    public class Renderer<TApp>
        where TApp : Application
    {
        internal readonly TApp _app;

        public Renderer() => _app = Activator.CreateInstance<TApp>();

        public Screen<TPage> Render<TPage>() where TPage : Page
            => new Screen<TPage>(Activator.CreateInstance<TPage>());

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
    }
}
