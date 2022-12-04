namespace Tikitapp.Website.Data.Entities;

public class Order {
    public Guid Id { get; set; }
	public Customer Customer { get; set; } = null!;
	public virtual List<Ticket> Tickets { get; set; } = new();
}