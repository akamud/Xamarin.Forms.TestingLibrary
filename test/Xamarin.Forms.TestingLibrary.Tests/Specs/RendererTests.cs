using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;
using Xamarin.Forms.TestingLibrary.Tests.Support;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class RendererTests : MockedFormsTestBase
    {
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

        [Test]
        public void RenderShouldThrowExceptionIfPassedPageIsNull()
        {
            var renderer = new Renderer<App>();

            Action act = () => renderer.Render<MainPage>(null);

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Page cannot be null. Did you forget to pass a valid Page to your Renderer?");
        }
    }
}
