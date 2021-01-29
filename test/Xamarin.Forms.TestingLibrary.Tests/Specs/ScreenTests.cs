using FluentAssertions;
using NUnit.Framework;
using System;
using Xamarin.Forms.TestingLibrary.SampleApp;
using Xamarin.Forms.TestingLibrary.SampleApp.Pages;
using Xamarin.Forms.TestingLibrary.Tests.Stubs;

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
                .WithMessage("Sequence contains more than one matching element");
        }

        [Test]
        public void QueryByTextShouldReturnTypedViewWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryByText<Label>(singleLabelText)!.Text.Should().Be(singleLabelText);
        }

        [Test]
        public void QueryByTextShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryByText<Label>("Other Label")!.Text.Should().Be("Other Label");
        }

        [Test]
        public void GetByTextShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
        {
            var screen = new Renderer<App>().Render<EmptyPage>();

            Action act = () => screen.GetByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no matching element");
        }

        [Test]
        public void GetByTextShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            Action act = () => screen.GetByText("Non-existant text");

            act.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("Sequence contains no matching element");
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
                .WithMessage("Sequence contains more than one matching element");
        }

        [Test]
        public void GetByTextShouldReturnTypedViewWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetByText<Label>(singleLabelText)!.Text.Should().Be(singleLabelText);
        }

        [Test]
        public void GetByTextShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetByText<Label>("Other Label")!.Text.Should().Be("Other Label");
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
        public void QueryAllByTextShouldReturnCollectionWithTypedViewsWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryAllByText<Label>(singleLabelText).Should().OnlyContain(x => x.Text == singleLabelText);
        }

        [Test]
        public void QueryAllByTextShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.QueryAllByText<Label>("Other Label").Should().OnlyContain(x => x.Text == "Other Label")
                .And.HaveCount(1);
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

        [Test]
        public void GetAllByTextShouldReturnCollectionWithTypedViewsWithSameTextFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetAllByText<Label>(singleLabelText).Should().OnlyContain(x => x.Text == singleLabelText);
        }

        [Test]
        public void GetAllByTextShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
        {
            var screen = new Renderer<App>().Render<MainPage>();

            screen.GetAllByText<Label>("Other Label").Should().OnlyContain(x => x.Text == "Other Label")
                .And.HaveCount(1);
        }

        [Test]
        public void ProvideBindingContextShouldSetContainersBindingContextWithPassedObject()
        {
            var screen = new Renderer<App>().Render<MainPage>();
            var vm = new TestViewModel();

            screen.ProvideBingingContext(vm);

            screen.Container.BindingContext.Should().Be(vm);
        }
    }
}
