---
title: "4: Introducing EF Core"
layout: module
nav_order: 04
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll add Entity Framework Core (EF Core) to our application, and use it to connect to our SQL database and begin using relational data in our application code.
---
In the last section, we created an empty SQL Server database using Docker, and added a user that we can use to connect to that database from our application code.

In this section, we're going to begin creating a **domain model** -- a set of classes that we'll use to model the business processes and user interactions in our application.

#### Which comes first - the code or the data?

Entity Framework Core supports two different ways of creating your database schema and the associated .NET classes

**Code First**: you write C# classes representing your objects, along with some special C# code which configures your database. Then you create and apply something called a **migration**, which will update your database to reflect the state of your application code.

**Database First:** you create your database first, using regular SQL scripts or design tools -- or you start with an existing database. Then you use Entity Framework's tooling to generate a set of C# classes directly from your database schema.

> Entity Framework used to support another approach, which was known as **Model First:** you use a visual design tool to create your data model in a special XML-based format known as an EDMX file, and then create your SQL database *and* your C# classes from this model. Model First was deprecated years ago, and will not be supported in EF Core.

## Creating the Artist class

To keep things simple, we're going to put all our domain classes, also known as *entities*, in the same project as our MVC web application.

Create a new folder in `Tikitapp.Website` called `Data`, and a new folder inside `Data` called `Entities`

Create a new class `Artist.cs` in that folder:

```csharp
// Tikitapp.Website/Data/Entities/Artist.cs

{% include_relative dotnet/module04/Tikitapp/Tikitapp.Website/Data/Entities/Artist.cs %}
```

## Install the EF Core command-line tools

Entity Framework Core includes command-line interface (CLI) tools for creating and managing database migrations. They're supplied as an extension to the `dotnet` command; you can read more about them at [Entity Framework Core tools reference](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

```
dotnet tool install -global dotnet-ef
```

We're also going to add the EF Core Nuget packages to our application. From the `Tikitapp.Website` folder, run:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Check the installation by running `dotnet ef` -- if it worked, you'll get an ASCII art unicorn. (No, really)

```
D:\Projects\Tikitapp>dotnet ef

                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 7.0.0
```

Next, we're going to add the DbContext class that we'll use to connect to our database.

```csharp
// Tikitapp.Website/Data/TikitappDbContext.cs

{% include_relative dotnet/module04/Tikitapp/Tikitapp.Website/Data/TikitappDbContext.cs %}
```

Then we'll add a connection string to our `appsettings.json` file:

```json
{% include_relative dotnet/module04/Tikitapp/Tikitapp.Website/appSettings.json %}
```

This connection string uses the database, username and password that we specified when we created the Tikitapp database using Docker in the previous section.

Finally, we need to add Entity Framework support to our application by registering the required services in `Program.cs`

```csharp
builder.Services.AddControllersWithViews();

// Add these two lines to enable EF Core support for ASP.NET Core 
var sqlConnectionString = builder.Configuration.GetConnectionString("Tikitapp");
builder.Services.AddDbContext<TikitappDbContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();
```

## Creating our first migration

Right now, we have:

* An ASP.NET Core application that includes a very simple data schema (the `Artist` class)
* A valid `DbContext` and the associated configuration, connection string and service registration
* The Entity Framework Core tooling

What we *don't* have is a database -- or rather, we do, but it's empty.

We're going to create a migration that will set up the initial state of our database.

```bash
dotnet ef migrations add InitialCreate
```

{: .highlight }
It's a good idea to run `dotnet format` after running any of the `dotnet` tools, since the templates and code style they use will not necessarily match the style we've defined with our `.editorconfig` file.

This will add a new folder, `Migrations`, to our project, and add two files to that folder. One of them,`TikitappDbContextModelSnapshot.cs`, we can ignore; EF Core uses it internally to keep track of the state of our data model.

The other one will be called something like `20220102030506_InitialCreate.cs` -- the filename is a `yyyyMMddhhmmss` timestamp, plus the name we specified when we created the migration:

```csharp
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tikitapp.Website.Migrations {
	/// <inheritdoc />
	public partial class InitialCreate : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Artists",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Artists", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Artists");
		}
	}
}
```

Every migration has two methods - `Up` and `Down`. `Up` happens when we update the database; `Down` will be run if we need to roll back this migration.

This migration has not been applied to our database yet: it's a chance to look at what *will* happen when we apply it, and check it's doing the right thing. It's... almost right. 

:white_check_mark: EF Core has inferred from the property name `Id` that this should be the primary key on our `Artist` table
:white_check_mark: EF Core has mapped the `Guid` to an SQL `uniqueidentifier`
:white_check_mark: Both the `Id` and `Name` columns are `nullable: false` (remember, we're in .NET 6 here, where *nothing* is nullable unless you ask nicely)

There's one detail here we want to change. The `Name` column is going to be created as `type: "nvarchar(max)"` - that's an infinitely long Unicode string. Unicode is fine, but we probably don't want the names of our artists to be infinitely long.

Choosing sensible field lengths  is vitally important when creating a data schema, and the best way to find sensible lengths is to look for real-world outliers. The `Artist` table in our model is to store artists - bands, musicians, orchestras. A quick Google search suggests that the longest band names out there are:

* The Silver Mount Zion Memorial Orchestra and Tra-La-La Band *(60 letters)*
* Tim and Sam's Tim and the Sam Band with Tim and Sam *(52 characters)*
* …And You Will Know Us By The Trail Of The Dead *(48 letters)*
* The Presidents of the United States of America *(46 letters)*
* Richard Cheese & Lounge Against The Machine *(43 letters)*

So 100 Unicode characters -- `nvarchar(100)` -- looks like a pretty good choice for our artist name column.

There are several different ways to control the SQL schema that's generated when we apply our migration:

#### Edit the migration

Migrations are just C# code; once they're generated, they're part of our project and we can edit them as much as we need to. We could just replace `nvarchar(max)` with `nvarchar(100)` -- as long as we do this before we apply the migration, we'll get the correct column length when the table is created.

#### Override OnModelBuilding

Rather than editing the migration, we can explicitly define our field types and properties by overriding the `OnModelCreating` method in `TikitappDbContext`:

```csharp
protected override void OnModelCreating(ModelBuilder builder) {
	builder.Entity<Artist>(entity => {
		// Specify a column size for the Name property
		entity.Property(e => e.Name).HasMaxLength(100);
	});
}
```

Once that's done, we can remove the migration with `dotnet ef migrations remove`, and then create it again by running `dotnet ef migrations add InitialCreate` again.

#### Use a data annotation

The `System.ComponentModel.DataAnnotations` namespace provides a bunch of attributes we can put on our class properties, including the `MaxLength` attribute:

```csharp
using System.ComponentModel.DataAnnotations;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
}
```

In this case, we're going to use the `MaxLength` attribute for the `Name` property, since this can also be useful for frontend model validation.

Once we've updated the code, we need to remove and re-create our migration:

```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
```

Finally, we need to apply the migration and update our database:

```
dotnet ef database update
```

That'll connect to the database and run the migration -- which creates the `Artist` table, complete with the `nvarchar(100)` column we talked about. It'll also create a tracking table called `__EFMigrationsHistory`, and insert a row into this table recording the fact that this migration has been applied.

![image-20221201004348884](D:\Projects\github\ursatile\fsnet\assets\images\image-20221201004348884.png)

## Using Migrations to Populate Data

As well as updating our database schema, we can use migrations to run SQL statements to insert or update data in our database. We're going to add another migration to populate our Artist table with some sample data.

```bash
dotnet ef migrations add InsertSampleArtistRecords
```

Then we'll edit our new migration to insert some Artist records.

{: .note }

We're specifying the values for the GUIDs in our SQL, so that we can tell at a glance whether a record is one of our sample data records or if it's real data from real users:

```csharp
// Tikitapp.Website/Migrations/20221201170934_InsertSampleArtistRecords.cs

{% include_relative dotnet/module04/Tikitapp/Tikitapp.Website/Migrations/20221201170934_InsertSampleArtistRecords.cs %}
```

Finally, apply our migration:

```
dotnet ef database update
```

We now have a database with 26 example artist records in it.

## Review and Recap

* Entity Framework Core (EF Core) is a set of tools and libraries for connecting .NET applications to relational databases
* The *Code First* approach allows us to define our data schema and structure in C# code
* EF Core provides tooling that will capture the state of our C# data model and translate that into *migrations*, which we can then apply to our database
* We can control the exact data scheme using annotations, by overriding the `OnModelBuilding` method on our data context, or by editing the migration files before they're applied to the database





