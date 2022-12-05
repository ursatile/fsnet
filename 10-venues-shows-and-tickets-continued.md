---
title: "10: Venues, Shows and Tickets Continued"
layout: module
nav_order: 110
typora-copy-images-to: ./assets/images
summary: >
    Adding more controllers, views and actions to the MVC web application to view the new entities we added in the previous section.
---

In this module, we'll add two more controllers, for shows and venues, and create the views used by these controller actions.

Our two new controllers are here:

```csharp
// Tikitapp.Website/Controllers/ShowsController.cs

{% include_relative dotnet/module10/Tikitapp/Tikitapp.Website/Controllers/ShowsController.cs %}
```

```csharp
// Tikitapp.Website/Controllers/VenuesController.cs

{% include_relative dotnet/module10/Tikitapp/Tikitapp.Website/Controllers/VenuesController.cs %}
```

Here's the code for the three new views:

```html
@* Tikitapp.Website/Views/Shows/Tickets.cshtml *@

{% include_relative dotnet/module10/Tikitapp/Tikitapp.Website/Views/Shows/Tickets.cshtml %}
```

```html
@* Tikitapp.Website/Views/Venues/Index.cshtml *@

{% include_relative dotnet/module10/Tikitapp/Tikitapp.Website/Views/Venues/Index.cshtml %}
```

```html
@* Tikitapp.Website/Views/Venues/Shows.cshtml *@

{% include_relative dotnet/module10/Tikitapp/Tikitapp.Website/Views/Venues/Shows.cshtml %}
```

