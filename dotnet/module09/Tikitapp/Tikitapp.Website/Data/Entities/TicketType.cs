namespace Tikitapp.Website.Data.Entities;

public class TicketType {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public string Name { get; set; } = String.Empty;
	public decimal Price { get; set; }
}