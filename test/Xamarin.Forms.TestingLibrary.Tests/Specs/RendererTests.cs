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

            renderer._app.MainPage.Should().NotBeNull();
        }

        [Test]
        public void RenderShouldSetAppMainPageToPagePassed()
        {
            var renderer = new Renderer<App>();
            var mainPage = new MainPage();
            renderer.Render(mainPage);

            renderer._app.MainPage.Should().Be(mainPage);
        }

        [Test]
        public void RenderShouldReturnScreenWithPageInstanceAsContainerWhenPageTypeIsPassedAsGenericType()
        {
            var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            screen.Container.Should().NotBeNull();
        }

        [Test]
        public void RenderShouldReturnScreenWithPageInstanceAsContainerWhenPageIsPassedAsParameter()
        {
            var renderer = new Renderer<App>();
            var mainPage = new MainPage();
            var screen = renderer.Render(mainPage);

            screen.Container.Should().Be(mainPage);
        }
    }
}
