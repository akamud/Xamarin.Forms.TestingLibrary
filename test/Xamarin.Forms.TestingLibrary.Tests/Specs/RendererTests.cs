using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Controls;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class RendererTests
    {
        [Test]
        public void RenderShouldSetAppMainPageToGenericTypePassed()
        {
            using var renderer = new Renderer<App>();
            renderer.Render<MainPage>();

            renderer._app.MainPage.Should().NotBeNull();
        }

        [Test]
        public void RenderWithGenericParameterShouldSetAppMainPageToPagePassed()
        {
            using var renderer = new Renderer<App>();
            renderer.Render<MainPage>();

            renderer._app.MainPage.Should().BeOfType<MainPage>();
        }

        [Test]
        public void RenderShouldSetAppMainPageToPagePassed()
        {
            using var renderer = new Renderer<App>();
            var mainPage = new MainPage();
            renderer.Render(mainPage);

            renderer._app.MainPage.Should().Be(mainPage);
        }

        [Test]
        public void RenderShouldReturnScreenWithPageInstanceAsContainerWhenPageTypeIsPassedAsGenericType()
        {
            using var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            screen.Container.Should().NotBeNull();
        }

        [Test]
        public void RenderShouldReturnScreenWithPageInstanceAsContainerWhenPageIsPassedAsParameter()
        {
            using var renderer = new Renderer<App>();
            var mainPage = new MainPage();
            var screen = renderer.Render(mainPage);

            screen.Container.Should().Be(mainPage);
        }

        [Test]
        public void RenderShouldThrowExceptionIfPassedPageIsNull()
        {
            using var renderer = new Renderer<App>();

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
            using var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            Action act = () => renderer.Tap(screen.QueryByText("non-existant text")!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TapShouldTriggerTapGestureRecognizerWhenAvailableAndMatchesRequiredNumberOfTaps()
        {
            using var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            renderer.Tap(screen.GetByText("Tappable Label"), 2);

            screen.GetByText<Label>("True").Should().NotBeNull();
        }

        [Test]
        public void TapShouldNotTriggerTapGestureRecognizerWhenRequiredNumberOfTypesIsNotMatched()
        {
            using var renderer = new Renderer<App>();
            var screen = renderer.Render<MainPage>();

            renderer.Tap(screen.GetByText("My Label"));

            screen.GetByText<Label>("False").Should().NotBeNull();
        }

        [Test]
        public void RenderShouldCorrectlyRenderViewsThatDependOnRendererProperty()
        {
            using var renderer = new Renderer<App>();
            var testPage = new ContentPage
            {
                Content = new CustomStackLayout()
            };
            var screen = renderer.Render(testPage);

            screen.GetByText<Label>("CustomControlText").Should().NotBeNull();
        }
    }
}
