using System;

namespace Xamarin.Forms.TestingLibrary
{
    public class Renderer<TApp>
        where TApp : Application
    {
        internal readonly TApp _app;

        public Renderer() => _app = Activator.CreateInstance<TApp>();

        public Screen Render<TPage>() where TPage : Page => Render(typeof(TPage));

        public Screen Render(Type page)
        {
            var screenPage = (Page)Activator.CreateInstance(page);

            return Render(screenPage);
        }

        public Screen Render(Page page)
        {
            _app.MainPage = page;

            return new Screen();
        }
    }
}
