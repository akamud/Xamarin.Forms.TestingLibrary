using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;

namespace Xamarin.Forms.TestingLibrary.Tests.Specs
{
    public class ScreenTests
    {
        private const string singleLabelText = "My Label";
        private const string multipleLabelText = "Name Label";

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

            screen.QueryByText(singleLabelText).Should().BeOfType<Label>();
        }

        [Test]
        public void QueryByTextShouldThrowInvalidOperationExceptionWhenMoreThanOneTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.QueryByText(multipleLabelText);

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains more than one element");
        }

        [Test]
        public void GetByTextShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
        {
            var screen = new Renderer<App>().Render<EmptyPage>();

            Action act = () => screen.GetByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no elements");
        }

        [Test]
        public void GetByTextShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.GetByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no elements");
        }

        [Test]
        public void GetByTextShouldReturnViewWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetByText(singleLabelText).Should().BeOfType<Label>();
        }

        [Test]
        public void GetByTextShouldThrowInvalidOperationExceptionWhenMoreThanOneTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.GetByText(multipleLabelText);

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains more than one element");
        }

        [Test]
        public void QueryAllByTextShouldReturnEmptyCollectionWhenPageHasNoElements()
        {
            var screen = new Renderer<App>().Render<EmptyPage>();

            screen.QueryAllByText("Non-existant text").Should().BeEmpty();
        }

        [Test]
        public void QueryAllByTextShouldReturnEmptyCollectionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryAllByText("Non-existant text").Should().BeEmpty();
        }

        [Test]
        public void QueryAllByTextShouldReturnCollectionWithViewWhenOneViewWithTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryAllByText(singleLabelText).Should().ContainItemsAssignableTo<Label>()
                .And.HaveCount(1);
        }

        [Test]
        public void QueryAllByTextShouldReturnCollectionWithViewsWhenMoreThanOneTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryAllByText(multipleLabelText).Should().ContainItemsAssignableTo<Label>()
                .And.HaveCount(2);
        }

        [Test]
        public void GetAllByTextShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
        {
            var screen = new Renderer<App>().Render<EmptyPage>();

            Action act = () => screen.GetAllByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no elements");
        }

        [Test]
        public void GetAllByTextShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.GetAllByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no elements");
        }

        [Test]
        public void GetAllByTextShouldReturnCollectionWithViewWhenOneViewWithTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetAllByText(singleLabelText).Should().ContainItemsAssignableTo<Label>()
                .And.HaveCount(1);
        }

        [Test]
        public void GetAllByTextShouldReturnCollectionWithViewsWhenMoreThanOneTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetAllByText(multipleLabelText).Should().ContainItemsAssignableTo<Label>()
                .And.HaveCount(2);
        }
    }
}
