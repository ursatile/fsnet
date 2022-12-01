---
title: "2: Testing ASP.NET Web Apps"
layout: module
nav_order: 02
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll add some tests to our application to demonstrate two different testing styles that work well with ASP.NET Core web applications.
---
In the last section, we set up our basic ASP.NET MVC web application and created a test project for it.

At the moment, there's only a single test in our solution, and it doesn't really do very much:

```csharp
// UnitTest1.cs

{% include_relative dotnet/module01/Tikitapp/Tikitapp.Website.Tests/UnitTest1.cs %}
```

Before we go any further, we're going to add a few tests that'll ensure that we haven't broken the basic features of our app start as we start adding new features.

First, we're going to install a package called [Shouldly](https://www.nuget.org/packages/Shouldly), which will give us nicer assertions and friendlier error messages than the built-in assertion methods that ship with xUnit.

```
D:\Projects\Tikitapp>cd Tikitapp.Website.Tests
D:\Projects\Tikitapp\Tikitapp.Website.Tests>dotnet add package Shouldly
```

## Testing strategies for .NET web applications

There is no "one size fits all" solution for testing applications. Any kind of automated testing is better than none, and a codebase which is well covered by tests will always be easier to manage and maintain in the long term than a codebase with no testing.

We're going to use two different testing styles in this workshop, which I'm going to refer to as "unit tests" and "web tests".

### Testing Controller Actions in ASP.NET MVC

One of the great advantages of the model/view/controller (MVC) pattern is that all our controller actions are just regular .NET code. Sure, when our website is running in production, our actions run as part of the ASP.NET request-response pipeline that drives our web application, but there's no reason we can't just run them as regular .NET methods.

We're going to add a test that verifies that the `HomeController.Index()` method in our application returns a valid `ViewResult` -- after all, if something we do later breaks our application homepage, it would be good to know about that before we deploy our app into production.

First, we need to add a reference so that our test project can see our website project:

```
D:\Projects\Tikitapp\Tikitapp.Website.Tests>dotnet add reference ..\Tikitapp.Website\
```

#### Global Usings

C# 10, which ships with .NET 6, introduces a new feature called **global usings**.

If there's a namespace that's widely used in our project, then instead of having to put a `using` directive at the top of each individual file, we can add a `global using` directive, which the compiler will apply to every file in our project at build time.

We're going to use this feature in our test project to make commonly-used namespaces available to all our testing code:

```csharp
// Tikitapp.Website.Tests/Usings.cs

global using Xunit;
global using Tikitapp.Website.Controllers;
global using Shouldly;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging.Abstractions;
```

#### Controller Tests

Next, create a new folder in the `Tikitapp.Website.Tests` project called `Controllers`, create a new file in that folder called `HomeControllerTests`, and add the following class to it:

```csharp
// Tikitapp.Website.Tests/Controllers/HomeControllerTests.cs

{% include_relative dotnet/module02/Tikitapp/Tikitapp.Website.Tests/Controllers/HomeControllerTests.cs %}
```

Run your application tests using `dotnet test`:

```
D:\Projects\Tikitapp>dotnet test
```

### Testing with the WebApplicationFactory

Unit tests like the one above are small, simple, and fast -- but they don't give you the whole picture. All they'll tell you is that your controller action works. They don't test routing, serialization, authentication, or any number of other details which could indicate a problem with your application.

Fortunately, .NET provides an alternative: the `WebApplicationFactory`. This provides a way to spin up a test version of our web application that we can use in our tests.

{: .information }
You'll sometimes hear these kinds of tests referred to as **integration tests**, since they're testing that the various parts of your application are correctly integrated.

Add a new package to your test project - note that since the release of .NET 7, we'll need to specify `--version 6` to install the version of `Microsoft.AspNetCore.Mvc.Testing` that's compatible with .NET 6:

```
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 6
```

We'll also add that namespace to our `Usings.cs` file:

```csharp
// Tikitapp.Website.Tests/Usings.cs

{% include_relative dotnet/module02/Tikitapp/Tikitapp.Website.Tests/Usings.cs %}
```

To use the `WebApplicationFactory`, our test project needs to be able to see the `Program` class from our web app -- but in .NET 6, the `Program` class is implicit, because we're using top-level statements to configure and run our web application, so there isn't anywhere to add the `public` keyword.

We could add a partial class declaration to our `Program.cs` file:

```csharp
public partial class Program { }
```

Alternatively, we can edit our project file and make internal classes visible to our test project. Edit `Tikitapp.Website.csproj` so that it looks like this:

```xml
<!-- Tikitapp.Website.csproj -->

{% include_relative dotnet/module02/Tikitapp/Tikitapp.Website/Tikitapp.Website.csproj %}
```

Now we can create test fixtures which use `WebApplicationFactory` and our `Program` class to create a test server and run tests against it which use the full HTTP request/response pipeline.

{: .note }
By declaring our test methods as `async`, we can use `async` and `await` inside our test code; it doesn't make an individual test any quicker, but it will make it faster to run multiple tests in parallel by using asynchronous I/O so that tests don't block each other.

```csharp
// Tikitap.Website.Tests/WebTests.cs

{% include_relative dotnet/module02/Tikitapp/Tikitapp.Website.Tests/WebTests.cs %}
```

## Exercise: Testing ASP.NET Web Apps

The default ASP.NET MVC template includes a placeholder page for your site's privacy policy -- it's exposed by the `HomeController.Privacy()` method and the `Views/Home/Privacy.cshtml` view file.

* Add a controller test that verifies that the `Privacy()` action returns a valid `ViewResult`.
* Modify the `Privacy.cshtml` view to include the following paragraph:
```
<p>This site is published under the <a href="https://en.wikipedia.org/wiki/MIT_License">MIT License</a>.</p>
```

* Add a new test method to the `WebTests` class which will make a request to `/Home/Privacy`, and verify that the resulting page includes the text `"https://en.wikipedia.org/wiki/MIT_License"` -- the URL of the Wikipedia page about the MIT license.

## Review and Recap

In this section, we've learned about two different strategies for adding test coverage to an ASP.NET Core MVC web application:

* Unit tests allow us to test the specific behaviour of an individual controller action.
* Integration tests allow us to test the entire request/response pipeline, including routing, serialization, and rendering views to HTML.
