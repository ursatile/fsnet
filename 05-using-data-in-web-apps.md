---
title: "5: Using Data in Web Apps"
layout: module
nav_order: 05
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll see how to connect our MVC application to our database using EF Core, how to use data in our controllers and views, and how to test controllers which rely on an EF Core DbContext.
---
So far, we've created an ASP.NET Core MVC web application and a test project, added an Entity Framework data context, set up a SQL database using Docker, and used EF migrations to create and populate our first data table.

Time to build our first data-driven page.

First, we'll create the `ArtistsController`:

```csharp
// Tikitapp.Website/Controllers/ArtistsController.cs

{% include_relative dotnet/module05/Tikitapp/Tikitapp.Website/Controllers/ArtistsController.cs %}
```

Next, we'll create two new views -- one to display a list of artists, the other shows details of a particular artist.

Here's the code for `Index.cshtml`:

```html
@* Tikitapp/Tikitapp.Website/Views/Artists/Index.cshtml *@

{% include_relative dotnet/module05/Tikitapp/Tikitapp.Website/Views/Artists/Index.cshtml %}
```

And here's the code for `View.cshtml`

```html
@* Tikitapp/Tikitapp.Website/Views/Artists/View.cshtml *@

{% include_relative dotnet/module05/Tikitapp/Tikitapp.Website/Views/Artists/View.cshtml %}
```

Finally, let's add a link to Artists to the top-level navigation on our site. Open `Views/Shared/_Layout.cshtml`, find the `<ul class="navbar-nav">` element, and add this snippet to it:

```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-controller="Artists" asp-action="Index">Artists</a>
</li>
```

Now, run your application. You should be able to browse to `/Artists`, see a list of artists, and click on an artist to see more details about that artist:

![image-20221201110615333](D:\Projects\github\ursatile\fsnet\assets\images\image-20221201110615333.png)

