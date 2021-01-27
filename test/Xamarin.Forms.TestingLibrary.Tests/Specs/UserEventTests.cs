using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class UserEventTests
    {
        [Test]
        public void TapShouldThrowArgumentNullExceptionWhenViewPassedIsNull()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => UserEvent.Tap(screen.QueryByText("non-existant text")!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TapShouldTriggerTapGestureRecognizerWhenAvailableAndMatchesRequiredNumberOfTaps()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            UserEvent.Tap(screen.GetByText("Tappable Label"), 2);

            screen.GetByText<Label>("True").Should().NotBeNull();
        }

        [Test]
        public void TapShouldNotTriggerTapGestureRecognizerWhenRequiredNumberOfTypesIsNotMatched()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            UserEvent.Tap(screen.GetByText("My Label"));

            screen.GetByText<Label>("False").Should().NotBeNull();
        }
    }
}
