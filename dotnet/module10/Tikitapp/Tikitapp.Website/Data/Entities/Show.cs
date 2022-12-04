namespace Tikitapp.Website.Data.Entities;

public class Show {
	public Guid Id { get; set; }
	public Venue Venue { get; set; } = null!;
	public Artist Artist { get; set; } = null!;
	public DateTimeOffset DoorsOpen { get; set; }
	public DateTimeOffset ShowStart { get; set; }
	public virtual List<TicketType> TicketTypes { get; set; } = new();
}