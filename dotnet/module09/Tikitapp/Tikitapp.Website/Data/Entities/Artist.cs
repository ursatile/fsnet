using System.ComponentModel.DataAnnotations;

namespace Tikitapp.Website.Data.Entities;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(100)]
	public string Slug { get; set; } = String.Empty;
	public virtual List<Show> Shows { get; set; } = new();
}

public class Venue {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(100)]
	public string Slug { get; set; } = String.Empty;
	public string StreetAddress { get; set; } = String.Empty;
	public string TimeZoneId { get; set; } = String.Empty;
	public string CultureInfoName { get; set; } = String.Empty;
	public virtual List<Show> Shows { get; set; } = new();
}

public class Show {
	public Guid Id { get; set; }
	public Venue Venue { get; set; } = null!;
	public Artist HeadlineArtist { get; set; } = null!;
	public virtual List<Artist> SupportArtists { get; set; } = new();
	public DateTimeOffset DoorsOpen { get; set; }
	public DateTimeOffset ShowStart { get; set; }
	public virtual List<TicketType> TicketTypes { get; set; } = new();
}

public class TicketType {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public string Name { get; set; } = String.Empty;
	public decimal Price { get; set; }
}

public class Basket {
	public Guid Id { get; set; }
	public virtual List<Ticket> Tickets { get; set; } = new();
}

public class Ticket {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
}

public class Customer {
	public string Name { get; set; } = String.Empty;
	public string Email { get; set; } = String.Empty;
	public string Phone { get; set; } = String.Empty;
}

public class Order {
	public Customer Customer { get; set; } = null!;
	public virtual List<Ticket> Tickets { get; set; } = new();
}







