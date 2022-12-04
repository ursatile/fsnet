---
title: "9: Venues, Shows and Tickets"
layout: module
nav_order: 09
typora-copy-images-to: ./assets/images
summary: >
    We have code, data, tests, clean URLs, and a solid project structure. Let's add some more classes to our domain.
---

In this section, we're going to build out the rest of our domain model, so we can start building the features that'll demonstrate the other patterns and libraries we'll cover in the workshop.

## The Tikitapp Ticket Workflow

Tikitapp allows a **Customer** to purchase **Tickets** for **Shows**. A **Show** takes place at a **Venue**. A **Venue** represents a club, concert hall, theatre, or arena. Venues have a **Name** and a **StreetAddress**.

A show has one or more **TicketTypes**. A ticket type has a **Name** (e.g. "Seated", "Standing", "VIP Meet & Greet"), and a **Price**

When a Customer begins purchasing tickets, we create a **Basket**. A basket is owned by exactly one **Customer**, and contain zero or more **Tickets**

Once a Customer proceeds to the **Checkout** and provides their payment details, we create an **Order**, and send the customer an email confirming their order.

### Creating The Domain Model

In this module, we'll create the .NET classes representing all the entities that participate in our ticket workflow, then we'll make a few tweaks to the `TikitappDbContext`, and finally we'll generate and apply a migration to bring our database up to date with the state of our model.

#### Domain Entities

The code for each of the new domain entities is here:

```csharp
// Tikitapp.Website/Data/Entities/Basket.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/Basket.cs %}
```

```csharp
// Tikitapp.Website/Data/Entities/Order.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/Order.cs %}
```

```csharp
// Tikitapp.Website/Data/Entities/Show.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/Show.cs %}
```


```csharp
// Tikitapp.Website/Data/Entities/Ticket.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/Ticket.cs %}
```


```csharp
// Tikitapp.Website/Data/Entities/TicketType.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/TicketType.cs %}
```


```csharp
// Tikitapp.Website/Data/Entities/Venue.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/Entities/Venue.cs %}
```

#### DbContext and OnModelBuilding

Here's the modified code for the `TikitappDbContext`:

```csharp
// Tikitapp.Website/Data/TikitappDbContext.cs
{% include_relative dotnet/module09/Tikitapp/Tikitapp.Website/Data/TikitappDbContext.cs %}
```

Things to note here:

* We're using a helper method to find any string property named `Slug`, and make all those properties non-Unicode strings, 100 characters, and adding a database index for that column.
* We override the column type for `TicketType.Price` to use the SQL Server built-in type `money`, which doesn't exist in the .NET type system.
* We're adding a `.HasMany().WithOne()` relationship for the various relationships between entities in our system.
* The `Ticket.Order` property is marked as nullable (`Order?`) since a ticket might not have been ordered yet.

Finally, we'll create and apply the database migration:

```
dotnet ef migrations add CreateTablesForDomainModel
dotnet ef database update
```

