using System;
using System.Collections.Generic;
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

        public void Provide(Config config)
        {
            Custom.Teste = new List<Config>();

            Custom.Teste.Add(config);
        }
    }

    public static class Custom
    {
        public static List<Config> Teste { get; set; }
    }
}
