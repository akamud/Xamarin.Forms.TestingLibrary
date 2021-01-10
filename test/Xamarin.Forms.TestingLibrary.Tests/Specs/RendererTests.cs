using FluentAssertions;
using NUnit.Framework;
using Xamarin.Forms.Mocks;
using Xamarin.Forms.TestingLibrary.SampleApp;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class RendererTests
    {
        [SetUp]
        public void SetUp() => MockForms.Init();

        [Test]
        public void RenderShouldSetAppMainPageToGenericTypePassed()
        {
            var renderer = new Renderer<App>();
            renderer.Render<MainPage>();

            renderer._app.MainPage.Should().BeOfType<MainPage>();
        }

        [Test]
        public void RenderShouldSetAppMainPageToTypePassed()
        {
            var renderer = new Renderer<App>();
            renderer.Render(typeof(MainPage));

            renderer._app.MainPage.Should().BeOfType<MainPage>();
        }

        [Test]
        public void RenderShouldSetAppMainPageToPagePassed()
        {
            var renderer = new Renderer<App>();
            var mainPage = new MainPage();
            renderer.Render(mainPage);

            renderer._app.MainPage.Should().Be(mainPage);
        }
    }
}
