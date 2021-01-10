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
            _app.MainPage = page;

            return new Screen<TPage>(page);
        }
    }
}
