---
title: "1: Getting Started"
layout: module
nav_order: 01
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll use the dotnet SDK to create a .NET 6 solution and set up the ASP.NET Core MVC web application and an xUnit test project we'll be using throughout the workshop.
---
In this workshop, we're going to create a web application called Tikitapp, for selling concert tickets.

We'll start by creating a new .NET solution, an ASP.NET MVC web application, and an xUnit test project.

First, check you have the .NET SDK installed. Typing `dotnet --version` at a command prompt should give you a version number beginning with `6.0`:

```bash
$ dotnet --version
6.0.306
```

Now, we're going to use the `dotnet new` command to create our solution and project files:

First, run:

```bash
dotnet new sln -o Tikitapp
```

That'll create a new folder called `Tikitapp`, containing an empty solution file `Tikitapp.sln`.

Next, we'll add some projects to our solution - an ASP.NET MVC web application called `Tikitapp.Website`, and an xUnit test project called `Tikitapp.Website.Tests`:

```bash
cd Tikitapp
dotnet new mvc -o Tikitapp.Website
dotnet new xunit -o Tikitapp.Website.Tests
dotnet sln add Tikitapp.Website
dotnet sln add Tikitapp.Website.Tests
```

Finally, let's check we can build, test, and run our application.

To build the solution, open the `Tikitapp` directory and type `dotnet build`:

```
D:\Projects\Tikitapp>dotnet build

Microsoft (R) Build Engine version 17.2.2+038f9bae9 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  Tikitapp.Website.Tests -> D:\Projects\Tikitapp\Tikitapp.Website.Tests\bin\Debug\net6.0\Tikitapp.Website.Tests.dll
  Tikitapp.Website -> D:\Projects\Tikitapp\Tikitapp.Website\bin\Debug\net6.0\Tikitapp.Website.dll

<span color="green">Build succeeded.</span>
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.93
```

To run our web application, we need to specify which project to start:

```bash
D:\Projects\Tikitapp>dotnet run --project Tikitapp.Website

Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7183
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5083
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: D:\Projects\Tikitapp\Tikitapp.Website\
```

Ctrl-clicking (or cmd-click on macOS) the `https://` URL in the output should open the website in your default browser:

![image-20221130154618189](D:\Projects\github\ursatile\fsnet\assets\images\image-20221130154618189.png)

Finally, check that we can run our xUnit test project:

```
D:\Projects\Tikitapp>dotnet test
  Determining projects to restore...
  All projects are up-to-date for restore.
  Tikitapp.Website.Tests -> D:\Projects\Tikitapp\Tikitapp.Website.Tests\bin\Debug\net6.0\Tikitapp.W
  ebsite.Tests.dll
Test run for D:\Projects\Tikitapp\Tikitapp.Website.Tests\bin\Debug\net6.0\Tikitapp.Website.Tests.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.2.0 (x64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: < 1 ms - Tikitapp.Website.Tests.dll (net6.0)
```

So far, so good - but before we start adding features to our application, there's a few things we're going to change.

## Code Formatting with EditorConfig

We're going to use a `.editorconfig` file to manage the formatting and layout of the source code in our application. 

{: .note }
EditorConfig is a way to maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. You can read more about it at [editorconfig.org](https://editorconfig.org)

You can find the  `.editorconfig` file we'll use here: [.editorconfig](dotnet/module02/Tikitapp/.editorconfig)

Add this file to your solution folder alongside your `Tikitapp.sln` file. 

{: .note }
**For Windows Users:** To create an `.editorconfig` file within Windows Explorer, you need to create a file named `.editorconfig.` (note the trailing dot), which Windows Explorer will automatically rename to `.editorconfig` for you.

After adding the .editorconfig file, reformat all the files in your project to match the project's new formatting settings.

Install the `dotnet-format` tool:

```
dotnet tool install --global dotnet-format
```

Then, from the command line:

```bash
D:\Projects\Tikitapp>dotnet format
```

That will reformat all the `.cs` files in the solution to conform to the code style specified in `.editorconfig`

{: .highlight }

> The `.editorconfig` used in this  workshop uses tabs for indentation, not spaces. I used to prefer spaces for indentation. Then I read Adam Tuttle's article  "[Tabs vs Spaces: It's an Accessibility Issue](https://adamtuttle.codes/blog/2021/tabs-vs-spaces-its-an-accessibility-issue/)", and that completely changed my mind. I can use tabs. No big deal. But there are developers out there for whom tabs vs spaces is a Big Deal. Developers with visual impairments who use an extra-large font size, who set their tab width to 1 character. Developers using Braille displays, for whom a tab only occupies a single Braille cell. So now I use tabs wherever I can.

## Review and Recap

In this module, we've:

* Used the `dotnet` command line tooling to create a new .NET solution
* Created an ASP.NET MVC web application and an xUnit test project
* Added our projects to our solution
* Checked that we can build, run, and test our application
* Added a `.editorconfig` file to manage the code style and formatting in our project
* Used the `dotnet format` tool to reformat the code in our solution to match our code style.



