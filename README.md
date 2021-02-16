# Xamarin.Forms.TestingLibrary

![Build](https://github.com/akamud/Xamarin.Forms.TestingLibrary/workflows/Build/badge.svg)
[![codecov](https://codecov.io/gh/akamud/Xamarin.Forms.TestingLibrary/branch/main/graph/badge.svg?token=ZSOS6JW6D4)](https://codecov.io/gh/akamud/Xamarin.Forms.TestingLibrary)
[![CodeFactor](https://www.codefactor.io/repository/github/akamud/xamarin.forms.testinglibrary/badge?s=bc6f084fefbb510c0c74058c3bfcfe6354559dff)](https://www.codefactor.io/repository/github/akamud/xamarin.forms.testinglibrary)

A testing library to make components testing for Xamarin.Forms easier, inspired by [Testing Library](https://testing-library.com/), [Flutter's Widget Testing](https://flutter.dev/docs/cookbook/testing/widget/introduction) and others.

## Getting Started

You must add this **only** to your Test project. It does not depend on any testing framework, so it doesn't matter if you are using nUnit, xUnit, or any other framework.

![](https://img.shields.io/nuget/v/Xamarin.Forms.TestingLibrary.svg?style=flat)  
[NuGet package](https://www.nuget.org/packages/Xamarin.Forms.TestingLibrary/) available:
```
PM> Install-Package Xamarin.Forms.TestingLibrary
```

Writing your first test is easy, you just need to create a `Renderer` for your Xamarin.Forms App and render a Xamarin.Forms Page:

```csharp
[Test]
public void MyFirstTestingLibraryTest()
{
    var renderer = new Renderer<App>();
    // Sets the App's main page to the page you passed and tries to render it.
    var screen = renderer.Render<Example1Page>();

    // Here your page will be rendered with all the triggers,
    // converters and bindings working as the real app would be.
    var loginButton = screen.GetByText<Button>("Login");
    
    // Do any assertion you want
    Assert.NotNull(loginButton);
}
```

If you need to provide a ViewModel specific for a test - so you can control the page's behavior or to provide a dependency - you can use the helper `ProvideBingingContext` on the `Screen`:

```csharp
[Test]
public void MyTestWithMockedDependencies()
{
    var renderer = new Renderer<App>();
    var screen = renderer.Render<Example1Page>();
    // your mock provided by your favorite mocking framework
    var mockedService = A.Fake<ILoginService>();
    // your viewModel specific for this test
    var testViewModel = new Example1PageViewModel(mockedService);
    // your viewModel will be used as the page's binding context
    screen.ProvideBindingContext(testViewModel);

    // Your actions or asserts that would call your mockedService
}
```

You can write tests to make sure your screen is rendered depending on the ViewModel's state, you can trigger gestures on a View and test the ViewModel is filled correctly, you can test that the page reacts correctly to the result of a service call, etc. You should be able to test any behavior as if the user was using your app. 

## Why isn't unit testing enough?

You surely can do a very complete test suite for your Xamarin.Forms app, but I feel like there is room for improvement. Unit testing your ViewModels can cover your code and run very fast, but they don't touch your user interfaces. UI Tests, on the other hand, can cover your user interfaces, but are very flaky and slow to run.

**Xamarin.Forms.TestingLibrary** sits in the middle. It aims to cover more ground than a simple ViewModel unit test, by handling your View code too, while keeping its speed and not having all the flakiness of a UI test. 

Take this scenario, we have an app with this XAML:

```xml
<StackLayout>
    <Label Text="Login"
            IsVisible="False">
        <Label.Triggers>
            <DataTrigger Binding="{Binding UserName, Converter={StaticResource IsNullOrEmptyConverter}}"
                            Value="True"
                            TargetType="Label">
                <Setter Property="IsVisible" Value="True" />
            </DataTrigger>
        </Label.Triggers>
    </Label>

    <Label IsVisible="False">
        <Label.Triggers>
            <DataTrigger Binding="{Binding UserName, Converter={StaticResource IsNullOrEmptyConverter}}"
                            Value="False"
                            TargetType="Label">
                <Setter Property="IsVisible" Value="True" />
            </DataTrigger>
        </Label.Triggers>
        <Label.FormattedText>
            <FormattedString>
                <Span Text="Welcome back, " />
                <Span Text="{Binding UserName}" />
                <Span Text="!" />
            </FormattedString>
        </Label.FormattedText>
    </Label>
</StackLayout>
```

We could easily write a unit test that guarantees that our ViewModel is loading the `UserName` correctly, we can also write a unit test for our `IsNullOrEmptyConverter` that makes sure it is behaving as expected, but those tests still don't guarantee the more important aspect of this view: is the correct message shown when the user is logged in/logged out? After all, at the end of the day, that's what matters: **is the user seeing the expected result?**

While the unit tests can still guarantee lots of important aspects, without some kind of integration test, they can't answer the above question because it never tests the way we connected the converters, triggers and binding expressions we declared.

That's where **Xamarin.Forms.TestingLibrary** comes in, it provides an API focused on the user interaction: it doesn't matter how you did it, what matters is that the user should be seeing the correct message, and it does this by running a slightly mocked Xamarin.Forms engine to make sure everything is behaving correctly as in the "real life". 

We could write these tests for the example above that would make our automated tests much more close to what the user actually sees, and provide a more confident test suite.

```csharp
public class Tests
{
    // This is the Xamarin.Forms.TestingLibrary renderer,
    // responsible for rendering your pages.
    private Renderer<App> _renderer;

    // Ideally, it can be created once per test suite.
    // And it is the most expensive operation.
    [SetUp]
    public void Setup() => _renderer = new Renderer<App>();

    // These run almost instantly, just as a unit test. 
    // But it is testing that your VM and View are correctly connected
    // with all the Xamarin.Forms engine.
    [Test]
    public void ShouldShowLoginMessageWhenUserNameIsNull()
    {
        var viewModel = new Example1PageViewModel
        {
            UserName = null
        };
        var screen = _renderer.Render<Example1Page>();
        screen.ProvideBingingContext(viewModel);

        Assert.AreEqual("Login", screen.GetByType<StackLayout>().GetTextContent());
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

        // It doesn't matter *HOW* we did it, what matters is that the user 
        // will see the correct message when logged in.
        Assert.AreEqual("Welcome back, Marvin!", screen.GetByType<StackLayout>().GetTextContent());
    }
}
```

As you can see, it is very focused on the user's perspective, so we load the page and get the visible text from the screen.

## Documentation

While I'm still writing more documentation, you can certainly learn a lot by taking a look at the tests I wrote for the [sample app](https://github.com/akamud/Xamarin.Forms.TestingLibrary/tree/main/samples/Xamarin.Forms.TestingLibrary.SampleApp/Xamarin.Forms.TestingLibrary.SampleApp.Tests) and for the [project itself](https://github.com/akamud/Xamarin.Forms.TestingLibrary/tree/main/test/Xamarin.Forms.TestingLibrary.Tests).

### Queries APIs

The queries APIs are probably where you'll spend most of your time, since the project's philosophy is to write a test as close as possible yo what a user see and does, these are the ways you can query the view hierarchy:

- `*ByText` - Filters the views based on their `Text` content.
    - Very useful to find labels and buttons.
- `*ByAutomationId` - Filters the views based on their `AutomationId` content.
    - Very useful when there is no easy way to differentiate elements on the screen. Also useful to make sure your app is accessible for people who use screen readers.
- `*ByType` - Filters the views based only on their type.
    - Useful when there are few elements of the same type on the screen.

#### Types of Queries

All the above queries can be combined with operators related to the number of elements that you expect on the screen. For example: if you are sure there is only 1 element with a given text on the screen, you can use `GetByText`. These are the available types of queries at the moment:

- `Get*` - Returns exactly 1 matching element, throws an exception if no element, or more than 1 element is found.
- `Query*` - Returns exactly 1 matching element, returns null if no element is found. Throws an exception if more than 1 element is found.
- `GetAll*` - Returns all matching elements on the screen, throws an exception if no element is found.
- `QueryAll*` - Returns all matching elements on the screen, returns an empty array if no element is found.

To make it easier to find the correct type of query, refer to this table:

| Type of Query  | 0 Matches | 1 Match | 2+ Matches |
| -------------- | --------- | ------- | ---------- |
| GetBy*  | Throws Exception | Returns the View | Throws exception |
| QueryBy*  | Returns `null` | Returns the View | Throws exception |
| GetAllBy*  | Throws Exception | Returns a List with the Views | Returns a List with the Views |
| QueryAllBy*  | Returns an empty List | Returns a List with the Views | Returns a List with the Views |

The single queries return a Xamarin.Forms [View](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.view?view=xamarin-forms) object, and the multiple queries return is a .NET [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-5.0), so you can do anything you want with the result, like checking for a specific property (e.g.: `IsVisible`) or do more filtering (e.g.: `First`, `Last`, `Skip`, etc.).

### Emulating interactions

Right now the only implemented interaction is the `Tap` gesture. If your View has a `TapGestureRecognizer`, you can trigger a `Tap` using the `Renderer`:

```csharp
[Test]
public void TapShouldSetTheLabelTextToTrueWhenTappedTwice()
{
    var renderer = new Renderer<App>();
    var screen = renderer.Render<MainPage>();
    // Just to show that there is a Label representing if the user Tapped.
    Assert.NotNull(screen.GetByText<Label>("False"));

    // Tries to tap on the Button with text "Tappable Button" twice.
    renderer.Tap(screen.GetByText("Tappable Button"), 2);

    // The "Tappable Button" triggers a command that updates the other Label property
    Assert.NotNull(screen.GetByText<Label>("True"));
}
```

## How it works

This project lies heavily on [Jonathan Peppers](https://github.com/jonathanpeppers) [Xamarin.Forms.Mocks](https://github.com/jonathanpeppers/Xamarin.Forms.Mocks) project. That's how we mock the Forms engine to actually work outside any platform (iOS, Android, etc.).

Other than that, most of the public APIs are using the same strategy: loading all the pages/views children and iterating and applying filters on them. It really is that simple. Xamarin.Forms.TestingLibrary is nothing more than a bunch of helpers that you could have written yourself. We are just giving you a shortcut.

## Limitations

Xamarin.Forms.TestingLibrary certainly **does not** substitute UI tests. There are a lot of scenarios where using Xamarin.Forms.TestingLibrary is not the best fit. I do believe using this project will help you reduce your UI tests, and that is a good thing, because UI tests, by its nature, are slow, flaky and expensive to run.

All the rendering in Xamarin.Forms.TestingLibrary is emulated, and while all the Xamarin.Forms engine is running, no actual [Platform Renderer](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/) is doing any work. This means that your control can behave correctly, but your platform renderer might be broken for a variety of reasons, and the only way to make sure everything is working is having at least a few UI tests running on a real device for your platforms (iOS, Android, Tizen, etc.).

## Roadmap

This is a very initial release, I've been doing a bunch of proof-of-concepts for new features. While I already have a bunch of ideas, I felt like releasing this initial version so I can get all the feedback I can. With that in mind, I certainly have these planned:

- Debugging the rendered screen - While I have a working example of this, I decided to put it in the next release because I wanted more time to think of how this will work.
- .NET MAUI support - while MAUI is still in its early stages, I'm studying how this could work with MAUI and I plan on having .NET MAUI support on day 1.
- Assertions APIs - Create an assertion library on top of [FluentAssertions](https://fluentassertions.com/) to provide more readable assertions.
- How does this behave with MVVM frameworks? - I did some tests with Prism, but I still have to investigate if there are any limitations on working with all the other frameworks out there. If you find any, please open an issue.
- See what's missing compared to [Flutter's Widget Testing](https://flutter.dev/docs/cookbook/testing/widget/introduction), [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/), [iOS KIF](https://github.com/kif-framework/KIF) and [Android Robolectric](http://robolectric.org/). All these frameworks have similar philosophy, so we can borrow some ideas that could work in Xamarin.Forms.TestingLibrary.

## Wanna help?

If you find any shortcomings or you have any idea on how to expand this project, please open an issue so we can discuss its evolution :)