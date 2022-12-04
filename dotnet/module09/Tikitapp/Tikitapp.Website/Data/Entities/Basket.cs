namespace Tikitapp.Website.Data.Entities;

public class Basket {
	public Guid Id { get; set; }
	public virtual List<Ticket> Tickets { get; set; } = new();
}