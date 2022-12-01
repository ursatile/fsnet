---
title: "6: Testing with DbContext"
layout: module
nav_order: 06
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll see how to create unit and integration tests for controllers and actions that use the DbContext.
---

## Unit Testing with the InMemoryDatabase Provider

To test controller actions which rely on a DbContext, we can use the InMemoryDatabase provider which ships with EF Core.

First, we need to install the Nuget package that includes this provider - we'll add this package to our Tikitapp.Website.Tests project:

```
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

We can create a lightweight, in-memory version of our database like this:

```csharp
var options = new DbContextOptionsBuilder<TikitappDbContext>()
	.UseInMemoryDatabase(databaseName: "some-database-name")
	.Options;
db = new TikitappDbContext(options);
db.Database.EnsureCreated();
```

It's not a real database, but it's close enough that we can use this in our test code to verify that our controllers and actions are doing the right thing.

```csharp
// Tikitapp.Website.Tests/Controllers/ArtistsControllerTests.cs

{% include_relative dotnet/module06/Tikitapp/Tikitapp.Website.Tests/Controllers/ArtistsControllerTests.cs %}
```

Note how we're creating specific test cases `testArtist1` and `testArtist2`, so that we can refer to these instances' names and IDs when asserting that our tests returned the right thing.

## Integration Testing with the InMemoryDatabase Provider

We can use the `InMemoryDatabase` provider together with the `WebApplicationFactory` to create end-to-end integration tests which cover our application's configuration, services, routing, etc., but still rely on the test version of our database.

To do this, we'll need to intercept the `factory.CreateClient()` method and inject a piece of code that will override the default configuration and replace the "real" `TikitappDbContext` with our in-memory database:

```csharp
var client = factory.WithWebHostBuilder(builder => {
	builder.ConfigureServices(services => services.AddSingleton(db));
}).CreateClient();
```

The full test class looks like this:

```csharp
// Tikitapp.Website.Tests/WebTests/ArtistTests.cs

{% include_relative dotnet/module06/Tikitapp/Tikitapp.Website.Tests/WebTests/ArtistsTests.cs %}
```

