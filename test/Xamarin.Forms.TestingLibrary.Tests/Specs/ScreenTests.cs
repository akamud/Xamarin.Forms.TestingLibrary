using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;
using Xamarin.Forms.TestingLibrary.Tests.Support;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class ScreenTests : MockedFormsTestBase
    {
        [Test]
        public void QueryByTextShouldReturnNullWhenPageHasNoElements()
        {
            var screen = new Renderer<App>().Render<EmptyPage>();

            screen.QueryByText("Non-existant text").Should().BeNull();
        }

        [Test]
        public void QueryByTextShouldReturnNullWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryByText("Non-existant text").Should().BeNull();
        }

        [Test]
        public void QueryByTextShouldReturnViewWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryByText("My Label").Should().BeOfType<Label>();
        }

        [Test]
        public void QueryByTextShouldThrowInvalidOperationExceptionWhenMoreThanOneTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.QueryByText("Name Label");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("More than one element found with Text: {Name Label}");
        }
    }
}
