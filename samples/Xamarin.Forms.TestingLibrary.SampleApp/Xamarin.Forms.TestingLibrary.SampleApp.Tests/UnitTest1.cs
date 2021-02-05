using FluentAssertions;
using NUnit.Framework;
using Xamarin.Forms.TestingLibrary.Extensions;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;
using Xamarin.Forms.TestingLibrary.SampleApp.ViewModels;

namespace Xamarin.Forms.TestingLibrary.SampleApp.Tests
{
    public class Tests
    {
        private Renderer<App> _renderer;

        [SetUp]
        public void Setup() => _renderer = new Renderer<App>();

        [Test]
        public void ShouldShowLoginMessageWhenUserNameIsNull()
        {
            var viewModel = new Example1PageViewModel
            {
                UserName = null
            };
            var screen = _renderer.Render<Example1Page>();
            screen.ProvideBingingContext(viewModel);

            screen.GetByType<StackLayout>().GetTextContent().Should().Be("Login");
        }

        [Test]
        public void ShouldShowWelcomeMessageWhenUserNameIsNotNull()
        {
            var viewModel = new Example1PageViewModel
            {
                UserName = "Marvin"
            };
            var screen = _renderer.Render<Example1Page>();
            screen.ProvideBingingContext(viewModel);

            screen.GetByType<StackLayout>().GetTextContent().Should().Be("Welcome back, Marvin!");
        }
    }
}
