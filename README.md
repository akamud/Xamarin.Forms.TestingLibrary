# Xamarin.Forms.TestingLibrary

![Build](https://github.com/akamud/Xamarin.Forms.TestingLibrary/workflows/Build/badge.svg)
[![codecov](https://codecov.io/gh/akamud/Xamarin.Forms.TestingLibrary/branch/main/graph/badge.svg?token=ZSOS6JW6D4)](https://codecov.io/gh/akamud/Xamarin.Forms.TestingLibrary)
[![CodeFactor](https://www.codefactor.io/repository/github/akamud/xamarin.forms.testinglibrary/badge?s=bc6f084fefbb510c0c74058c3bfcfe6354559dff)](https://www.codefactor.io/repository/github/akamud/xamarin.forms.testinglibrary)

A testing library to make components testing for Xamarin.Forms easier, inspired by [Testing Library](https://testing-library.com/), [Flutter's Widget Testing](https://flutter.dev/docs/cookbook/testing/widget/introduction) and others.

**This is in pre-release, the public APIs *may* change until the first 1.0 release.**

## Getting Started

You must add this **only** to your Test project. It does not depend on any testing framework, so it doesn't matter if you are using nUnit, xUnit, or any other framework.

[![NuGet](https://img.shields.io/nuget/v/Xamarin.Forms.TestingLibrary.svg?style=flat)](https://img.shields.io/nuget/v/Xamarin.Forms.TestingLibrary.svg?style=flat)

[NuGet package](https://www.nuget.org/packages/Xamarin.Forms.TestingLibrary/) available:
```
PM> Install-Package Xamarin.Forms.TestingLibrary -Version 0.1.0-pre
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

## Documentation

Check out the documentation [here](https://github.com/akamud/Xamarin.Forms.TestingLibrary/wiki).

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

## How it works

This project relies heavily on [Jonathan Peppers](https://github.com/jonathanpeppers) [Xamarin.Forms.Mocks](https://github.com/jonathanpeppers/Xamarin.Forms.Mocks) project. That's how we mock the Forms engine to actually work outside any platform (iOS, Android, etc.).

Other than that, most of the public APIs are using the same strategy: loading all the pages/views children and iterating and applying filters on them. It really is that simple. Xamarin.Forms.TestingLibrary is nothing more than a bunch of helpers that you could have written yourself. We are just giving you a shortcut.

## Limitations

Xamarin.Forms.TestingLibrary certainly **does not** substitute UI tests. There are a lot of scenarios where using Xamarin.Forms.TestingLibrary is not the best fit. I do believe using this project will help you reduce your UI tests, and that is a good thing, because UI tests, by its nature, are slow, flaky and expensive to run.

All the rendering in Xamarin.Forms.TestingLibrary is emulated, and while all the Xamarin.Forms engine is running, no actual [Platform Renderer](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/) is doing any work. This means that your control can behave correctly, but your platform renderer might be broken for a variety of reasons, and the only way to make sure everything is working is having at least a few UI tests running on a real device for your platforms (iOS, Android, Tizen, etc.).

## Roadmap

This is a very initial release, I've been doing a bunch of proof-of-concepts for new features. While I already have a bunch of ideas, I felt like releasing this initial version so I can get all the feedback I can. With that in mind, I certainly have these planned:

- .NET MAUI support - while MAUI is still in its early stages, I'm studying how this could work with MAUI and I plan on having .NET MAUI support on day 1.
- Assertions APIs - Create an assertion library on top of [FluentAssertions](https://fluentassertions.com/) to provide more readable assertions.
- How does this behave with MVVM frameworks? - I did some tests with Prism, but I still have to investigate if there are any limitations on working with all the other frameworks out there. If you find any, please open an issue.
- See what's missing compared to [Flutter's Widget Testing](https://flutter.dev/docs/cookbook/testing/widget/introduction), [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/), [iOS KIF](https://github.com/kif-framework/KIF) and [Android Robolectric](http://robolectric.org/). All these frameworks have similar philosophy, so we can borrow some ideas that could work in Xamarin.Forms.TestingLibrary.

## Contributing

If you find any shortcomings or you have any idea on how to expand this project, please open an issue so we can discuss its evolution :)
