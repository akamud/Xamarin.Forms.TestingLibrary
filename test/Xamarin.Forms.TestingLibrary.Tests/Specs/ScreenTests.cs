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
        private const string singleLabelAutomationId = "Single AutomationId";
        private const string multipleLabelAutomationId = "Multiple AutomationId";

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

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, Text = "NotVisible"}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryByText("NotVisible").Should().BeNull();
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

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, Text = "NotVisible"}};
                var screen = new Renderer<App>().Render(testView);

                Action act = () => screen.GetByText("NotVisible");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
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

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, Text = "NotVisible"}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryAllByText("NotVisible").Should().BeEmpty();
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
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetAllByText("Non-existant text");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithTextIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByText(singleLabelText).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(1);

                screen.Debug();
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

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testPage = new ContentPage {Content = new Label {IsVisible = false, Text = "NotVisible"}};
                var screen = new Renderer<App>().Render(testPage);

                Action act = () => screen.GetAllByText<Label>("NotVisible");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }
        }

        public class QueryByType
        {
            [Test]
            public void ShouldReturnNullWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryByType<ImageButton>().Should().BeNull();
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

                screen.QueryByType<ImageButton>().Should().BeOfType<ImageButton>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.QueryByType<Label>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one element");
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryByType<Label>().Should().BeNull();
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

                screen.QueryAllByType<ImageButton>().Should().ContainItemsAssignableTo<ImageButton>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneElementWithGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByType<StackLayout>().Should().ContainItemsAssignableTo<StackLayout>()
                    .And.HaveCount(5);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsNestedInsideListView()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByType<Image>().Should().ContainItemsAssignableTo<Image>()
                    .And.HaveCount(3);
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryAllByType<Label>().Should().BeEmpty();
            }
        }

        public class GetByType
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetByType<Picker>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByType<Picker>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldReturnViewWithSameTypeFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByType<ImageButton>().Should().BeOfType<ImageButton>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByType<Label>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one element");
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false}};
                var screen = new Renderer<App>().Render(testView);

                Action act = () => screen.GetByType<Label>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }
        }

        public class GetAllByType
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetAllByType<Picker>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetAllByType<Picker>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByType<ImageButton>().Should().ContainItemsAssignableTo<ImageButton>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneTypeIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByType<StackLayout>().Should().ContainItemsAssignableTo<StackLayout>()
                    .And.HaveCount(5);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsNestedInsideListView()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByType<Image>().Should().ContainItemsAssignableTo<Image>()
                    .And.HaveCount(3);
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false}};
                var screen = new Renderer<App>().Render(testView);

                Action act = () => screen.GetAllByType<Label>();

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no elements");
            }
        }

        public class QueryByAutomationId
        {
            [Test]
            public void ShouldReturnNullWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryByAutomationId("Non-existant automation id").Should().BeNull();
            }

            [Test]
            public void ShouldReturnNullWhenNoElementWithTheGivenAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByAutomationId("Non-existant automation id").Should().BeNull();
            }

            [Test]
            public void ShouldReturnViewWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByAutomationId(singleLabelAutomationId).Should().BeOfType<Label>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.QueryByAutomationId(multipleLabelAutomationId);

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one matching element");
            }

            [Test]
            public void ShouldReturnTypedViewWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByAutomationId<Label>(singleLabelAutomationId)!.AutomationId.Should().Be(singleLabelAutomationId);
            }

            [Test]
            public void ShouldFilterElementsWithSameAutomationIdButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryByAutomationId<Label>("Other Label Automation")!.AutomationId.Should().Be("Other Label Automation");
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, AutomationId = "AutomationId"}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryByAutomationId("AutomationId").Should().BeNull();
            }
        }

        public class QueryAllByAutomationId
        {
            [Test]
            public void ShouldReturnEmptyCollectionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                screen.QueryAllByAutomationId("Non-existant automationId").Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnEmptyCollectionWhenNoElementWithTheGivenAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByAutomationId("Non-existant automationId").Should().BeEmpty();
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByAutomationId(singleLabelAutomationId).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByAutomationId(multipleLabelAutomationId).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(2);
            }

            [Test]
            public void ShouldReturnCollectionWithTypedViewsWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByAutomationId<Label>(singleLabelAutomationId).Should()
                    .OnlyContain(x => x.AutomationId == singleLabelAutomationId);
            }

            [Test]
            public void ShouldFilterElementsWithSameAutomationIdButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.QueryAllByAutomationId<Label>("Other Label Automation").Should()
                    .OnlyContain(x => x.AutomationId == "Other Label Automation").And.HaveCount(1);
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, AutomationId = "AutomationId"}};
                var screen = new Renderer<App>().Render(testView);

                screen.QueryAllByAutomationId("AutomationId").Should().BeEmpty();
            }
        }

        public class GetByAutomationId
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetByAutomationId("Non-existant automationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByAutomationId("Non-existant automationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldReturnViewWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByAutomationId(singleLabelAutomationId).Should().BeOfType<Label>();
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenMoreThanOneAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetByAutomationId(multipleLabelAutomationId);

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains more than one matching element");
            }

            [Test]
            public void ShouldReturnTypedViewWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByAutomationId<Label>(singleLabelAutomationId)!.AutomationId.Should()
                    .Be(singleLabelAutomationId);
            }

            [Test]
            public void ShouldFilterElementsWithSameAutomationIdButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetByAutomationId<Label>("Other Label Automation")!.AutomationId.Should().Be("Other Label Automation");
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testView = new ContentPage {Content = new Label {IsVisible = false, AutomationId = "NotVisibleAutomationId"}};
                var screen = new Renderer<App>().Render(testView);

                Action act = () => screen.GetByAutomationId("NotVisibleAutomationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }
        }

        public class GetAllByAutomationId
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenPageHasNoElements()
            {
                var screen = new Renderer<App>().Render<EmptyPage>();

                Action act = () => screen.GetAllByAutomationId("Non-existant automationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldThrowInvalidOperationExceptionWhenNoElementWithTheGivenAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                Action act = () => screen.GetAllByAutomationId("Non-existant automationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
            }

            [Test]
            public void ShouldReturnCollectionWithViewWhenOneViewWithAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByAutomationId(singleLabelAutomationId).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldReturnCollectionWithViewsWhenMoreThanOneAutomationIdIsFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByAutomationId(multipleLabelAutomationId).Should().ContainItemsAssignableTo<Label>()
                    .And.HaveCount(2);
            }

            [Test]
            public void ShouldReturnCollectionWithTypedViewsWithSameAutomationIdFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByAutomationId<Label>(singleLabelAutomationId).Should().OnlyContain(x => x.AutomationId == singleLabelAutomationId);
            }

            [Test]
            public void ShouldFilterElementsWithSameAutomationIdButDifferentTypesFoundInPageHierarchy()
            {
                var screen = new Renderer<App>().Render<MainPage>();

                screen.GetAllByAutomationId<Label>("Other Label Automation").Should()
                    .OnlyContain(x => x.AutomationId == "Other Label Automation")
                    .And.HaveCount(1);
            }

            [Test]
            public void ShouldFilterElementsWithIsVisibleFalse()
            {
                var testPage = new ContentPage {Content = new Label {IsVisible = false, AutomationId = "AutomationId"}};
                var screen = new Renderer<App>().Render(testPage);

                Action act = () => screen.GetAllByAutomationId("AutomationId");

                act.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Sequence contains no matching element");
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
