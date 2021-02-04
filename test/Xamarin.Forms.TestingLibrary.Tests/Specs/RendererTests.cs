using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class RendererTests
    {
        [Test]
        public void RenderShouldSetAppMainPageToGenericTypePassed()
        {
            var renderer = new Renderer<App>();
            renderer.Render<MainPage>();

            renderer._app.MainPage.Should().NotBeNull();
        }

        [Test]
        public void RenderWithGenericParameterShouldSetAppMainPageToPagePassed()
        {
            var renderer = new Renderer<App>();
            renderer.Render<MainPage>();

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

        [Test]
        public void ConstructorShouldNotCallMockFormsInitWhenSkipIsTrue()
        {
            Device.PlatformServices = null;
            Device.Info = null;

            Action act = () => new Renderer<App>(true);

            act.Should().Throw<Exception>()
                .WithInnerException<InvalidOperationException>()
                .WithMessage("You must call Xamarin.Forms.Forms.Init(); prior to using this property.");
        }

        [Test]
        public void TapShouldThrowArgumentNullExceptionWhenViewPassedIsNull()
        {
            var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            Action act = () => renderer.Tap(screen.QueryByText("non-existant text")!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TapShouldTriggerTapGestureRecognizerWhenAvailableAndMatchesRequiredNumberOfTaps()
        {
            var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            renderer.Tap(screen.GetByText("Tappable Label"), 2);

            screen.GetByText<Label>("True").Should().NotBeNull();
        }

        [Test]
        public void TapShouldNotTriggerTapGestureRecognizerWhenRequiredNumberOfTypesIsNotMatched()
        {
            var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            renderer.Tap(screen.GetByText("My Label"));

            screen.GetByText<Label>("False").Should().NotBeNull();
        }
    }
}
