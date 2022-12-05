---
title: "8: Using Slugs for Nicer URLs"
layout: module
nav_order: 108
typora-copy-images-to: ./assets/images
summary: >
    GUIDs are awesome, but they're ugly. Let's make our URLs look prettier by adding URL slugs.
---

If we click through to view details of a particular artist right now, we get a URL that looks like this:

http://tikitapp.ursatile.com/Artists/View/0000001A-0000-0000-0000-000BDA4C94B5

That works, but great websites aren't just functional, they're also beautiful -- and that isn't a very pretty URL.

This would be much nicer:

http://tikitapp.ursatile.com/artists/view/javas-crypt

* All the URL segments are lowercase
* The artist is identified by a human-readable string (known as a **slug**), not a GUID

First, we'll write a test that covers this new behaviour. We're going to use a feature of xUnit here called `MemberData`, that will let us generate test cases from our test data code:

```csharp
public static IEnumerable<object> TestArtists() {
	yield return new[] { TestData.Artist1 };
	yield return new[] { TestData.Artist2 };
}

[Theory]
[MemberData(nameof(TestArtists))]
public async Task GET_Artist_By_Slug_Returns_Artist(Artist artist) {
	var db = await TestDatabase.CreateDbContext().PopulateWithTestDataAsync();
	var client = factory.WithWebHostBuilder(builder => {
		builder.ConfigureServices(services => services.AddSingleton(db));
	}).CreateClient();
	var response = await client.GetStringAsync($"/artists/view/{artist.Slug}");
	response.ShouldContain(artist.Name);
}
```

That test should fail - which makes sense; we haven't actually implemented the feature yet.

Let's add a `Slug` property to our `Artist` class:

```csharp
// Tikitapp.Website/Data/Entities/Artist.cs

{% include_relative dotnet/module08/Tikitapp/Tikitapp.Website/Data/Entities/Artist.cs %}
```

We'll need to update some of our test classes to reflect this new convention:

```csharp
// Tikitapp.Website.Tests/TestData.cs

{% include_relative dotnet/module08/Tikitapp/Tikitapp.Website.Tests/TestData.cs %}
```

We're not going to allow Unicode in slugs, so we can use a regular `varchar` column to store them in our database. We also want our slug to be an indexed column in our database so that retrieving artists based on a URL slug is a fast operation, so let's add some more code to `OnModelCreating` in our DbContext:

```diff
protected override void OnModelCreating(ModelBuilder builder) {
	builder.Entity<Artist>(entity => {
		entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
+		entity.Property(e => e.Slug).IsUnicode(false);
+		entity.HasIndex(e => e.Slug);
	});
}
```

Generate a database migration to apply the changes to our data schema:

```
dotnet ef migrations add AddSlugToArtist
```

Then we're going to edit the migration to include a SQL statement that will generate slugs based on artist names:

```csharp 
// Tikitapp.Website/Migrations/20221201173448_AddSlugToArtist.cs

{% include_relative dotnet/module08/Tikitapp/Tikitapp.Website/Migrations/20221201173448_AddSlugToArtist.cs %}
```

We'll update `Artists/Index.cshtml` to include the `Slug` when creating URLs:

```html
@foreach (var artist in Model) {
    <li>
        <a href="@Url.Action("Shows", "Artists", new { id = artist.Slug })">@artist.Name</a>
    </li>
}
```

{: .note }
We still need to specify `id = artist.Slug`, because ASPNET Core's routing assumes that the string parameter will be passed in a field called `id`. We *could* change this to `slug` by defining a new route for it, but this approach keeps things simpler.

Finally, we're going to configure our application to generate lowercased URLs when we use `Url.Action` and other helper methods, by adding a line to the top of our `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add this line BEFORE AddControllersWithViews() to use lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();
```

Done. Our `/artists` page now links to artists via a clean, readable, lowercased URL.



