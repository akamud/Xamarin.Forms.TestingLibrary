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

        public class QueryByText
        {
            [Test]
            public void ShouldReturnNullWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryByText("Non-existant text").Should().BeNull();
            }

            [Test]
            public void ShouldReturnNullWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByText("Non-existant text").Should().BeNull();
            }

            [Test]
            public void ShouldReturnViewWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByText(singleLabelText).Should().BeOfType<Label>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.QueryByText(multipleLabelText);

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one matching element");
            }

            [Test]
            public void ShouldReturnTypedViewWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByText<Label>(singleLabelText)!.Text.Should().Be(singleLabelText);
            }

            [Test]
            public void ShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByText<Label>("Other Label")!.Text.Should().Be("Other Label");
            }
        }

        public class GetByText
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetByText("Non-existant text");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByText("Non-existant text");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldReturnViewWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByText(singleLabelText).Should().BeOfType<Label>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByText(multipleLabelText);

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one matching element");
            }

            [Test]
            public void ShouldReturnTypedViewWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByText<Label>(singleLabelText)!.Text.Should().Be(singleLabelText);
            }

            [Test]
            public void ShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByText<Label>("Other Label")!.Text.Should().Be("Other Label");
            }
        }

        public class QueryAllByText
        {
            [Test]
            public void ShouldReturnEmptyCollectionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryAllByText("Non-existant text").Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnEmptyCollectionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByText("Non-existant text").Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByText(singleLabelText).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByText(multipleLabelText).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(2);
            }

            [Test]
            public void ShouldReturnCollectionWithTypedViewsWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByText<Label>(singleLabelText).Should().OnlyContain(x => x.Text == singleLabelText);
            }

            [Test]
            public void ShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByText<Label>("Other Label").Should().OnlyContain(x => x.Text == "Other Label")
                    .And.HaveCount(1);
            }
        }

        public class GetAllByText
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetAllByText("Non-existant text");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetAllByText("Non-existant text");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByText(singleLabelText).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByText(multipleLabelText).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(2);
            }

            [Test]
            public void ShouldReturnCollectionWithTypedViewsWithSameTextFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByText<Label>(singleLabelText).Should().OnlyContain(x => x.Text == singleLabelText);
            }

            [Test]
            public void ShouldFilterElementsWithSameTextButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByText<Label>("Other Label").Should().OnlyContain(x => x.Text == "Other Label")
                    .And.HaveCount(1);
            }
        }

        public class QueryByType
        {
            [Test]
            public void ShouldReturnNullWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryByType<Image>().Should().BeNull();
            }

            [Test]
            public void ShouldReturnNullWhenNoElementWithTheGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByType<Picker>().Should().BeNull();
            }

            [Test]
            public void ShouldReturnViewWithSameTypeFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByType<Image>().Should().BeOfType<Image>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.QueryByType<Label>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one element");
            }
        }

        public class QueryAllByType
        {
            [Test]
            public void ShouldReturnEmptyCollectionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryAllByType<Picker>().Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnEmptyCollectionWhenNoElementWithTheGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByType<Picker>().Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByType<Image>().Should().ContainItemsAssignableTo<Image>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneElementWithGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByType<StackLayout>().Should().ContainItemsAssignableTo<StackLayout>()
                    .And.HaveCount(2);
            }
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
