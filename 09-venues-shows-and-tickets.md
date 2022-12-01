---
title: "9: Venues, Shows and Tickets"
layout: module
nav_order: 08
typora-copy-images-to: ./assets/images
summary: >
    We have code, data, tests, clean URLs, and a solid project structure. Let's add some more classes to our domain.
---

In this section, we're going to build out the rest of our domain model, so we can start building the features that'll demonstrate the other patterns and libraries we'll cover in the workshop.

## The Tikitapp Ticket Workflow

Tikitapp allows a **Customer** to purchase **Tickets** for **Shows**. A **Show** takes place at a **Venue**. 

A show has one or more **TicketTypes**. 

A **Venue** represents a club, concert hall, theatre, or arena. Venues have a **Name**, a **StreetAddress**.

Venues also have a **TimeZoneId** and a **CultureInfoName**; these properties allow us to manage scheduling ticket sales, and localizing dates, times and currency information.

* `TimeZoneId` is the name of a zone defined in the [IANA Time Zone Database](https://www.iana.org/time-zones). Names are strings, in the format `"Europe/London"`, `"Africa/Cairo"`
* `CultureInfoName` is the name of a culture defined and supported by .NET's [`System.Globalization.CultureInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.name?view=net-6.0) class. A valid CultureInfoName looks like `en-GB` (English / Great Britain), `da-DK` (Danish / Denmark), or `en-US` (English / United States); the second part of the CultureInfoName will normally be a valid ISO3166 country code.

When a Customer begins purchasing tickets, we create a **Basket**. A basket is owned by exactly one **Customer**, and contain zero or more **Tickets**

Once a Customer proceeds to the **Checkout** and provides their payment details, we create an **Order**, and send the customer an email confirming their order.









