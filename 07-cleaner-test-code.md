---
title: "7: Cleaner Test Code"
layout: module
nav_order: 07
typora-copy-images-to: ./assets/images
summary: >
    Let's clean up the design of our test database and make it easier to reuse it in other tests.
---

In the previous module, we ended up with some code duplication between our test classes.

Let's move that into a set of dedicated classes that we can reuse across different unit and integration tests.

First, here's what we want our `ArtistTests` class to look like when we're finished:

```csharp
// Tikitapp.Website.Tests/WebTests/ArtistTests.cs

{% include_relative dotnet/module07/Tikitapp/Tikitapp.Website.Tests/WebTests/ArtistsTests.cs %}
```

And here's what our `ArtistsControllerTests` class should look like:

```csharp
// Tikitapp.Website.Tests/Controllers/ArtistsControllerTests.cs

{% include_relative dotnet/module07/Tikitapp/Tikitapp.Website.Tests/Controllers/ArtistsControllerTests.cs %}
```

To make that work, we'll create three new classes in our test project.

```csharp
// Tikitapp.Website.Tests/TestDatabase.cs

{% include_relative dotnet/module07/Tikitapp/Tikitapp.Website.Tests/TestDatabase.cs %}
```

```csharp
// Tikitapp.Website.Tests/TestData.cs

{% include_relative dotnet/module07/Tikitapp/Tikitapp.Website.Tests/TestData.cs %}
```

```csharp
// Tikitapp.Website.Tests/TikitappDbContextExtensions.cs

{% include_relative dotnet/module07/Tikitapp/Tikitapp.Website.Tests/TikitappDbContextExtensions.cs %}
```

And there it is. We can create, populate, and refer to our test database, anywhere we need to, in one line of code.

That's rather nice. :sunglasses:



