using FluentAssertions;
using NUnit.Framework;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs.Extensions
{
    public class ViewExtensionsTests
    {
        [Test]
        public void GetTextContentShouldReturnEmptyStringWhenNoChildrenHaveTexts()
        {
            var screen = new Renderer<App>().Render<MainPage>();
            var stackLayout = screen.GetByAutomationId<StackLayout>("emptyStack");

            var textContent = stackLayout.GetTextContent();

            textContent.Should().BeEmpty();
        }

        [Test]
        public void GetTextContentShouldReturnAllTextsByItsChildren()
        {
            var screen = new Renderer<App>().Render<MainPage>();
            var stackLayout = screen.GetByAutomationId<StackLayout>("textContentStack");

            var textContent = stackLayout.GetTextContent();

            textContent.Should().Be("Hello nested labels");
        }

        [Test]
        public void GetTextContentShouldReturnAllFormattedTextsByItsChildren()
        {
            var screen = new Renderer<App>().Render<MainPage>();
            var stackLayout = screen.GetByAutomationId<StackLayout>("formattedTextContentStack");

            var textContent = stackLayout.GetTextContent();

            textContent.Should().Be("Hello nested formatted Texts");
        }
    }
}
