---
title: "2: Testing ASP.NET Web Apps"
layout: home
nav_order: 02
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

Next, create a new folder in the `Tikitapp.Website.Tests` project called `Controllers`, create a new file in that folder called `HomeControllerTests`, and add the following class to it:

```csharp
// Tikitapp.Website.Tests/Controllers/HomeControllerTests.cs

{% include_relative dotnet/module02/Tikitapp/Tikitapp.Website.Tests/Controllers/HomeControllerTests.cs %}
```



