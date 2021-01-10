using System;

namespace Xamarin.Forms.TestingLibrary
{
    public class Renderer<TApp>
        where TApp : Application
    {
        private readonly TApp _app;

        public Renderer(TApp app)
        {
            this._app = app;
        }

        public Screen Render<TPage>() where TPage : Page => Render(typeof(TPage));

        public Screen Render(Type page)
        {
            var screenPage = Activator.CreateInstance(page);
            _app.MainPage = (Page)screenPage;

            return new Screen();
        }

        public Screen Render(Page page)
        {
            _app.MainPage = page;

            return new Screen();
        }
    }
}
