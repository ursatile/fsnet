namespace Tikitapp.Website.Data.Entities;

public class Ticket {
	public Guid Id { get; set; }
	public Basket Basket { get; set; } = null!;
    public Order? Order { get; set; } = null!;
	public Show Show { get; set; } = null!;
}