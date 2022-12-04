namespace Tikitapp.Website.Data.Entities;

public class Customer {
    public Guid Id { get; set; }
	public string Name { get; set; } = String.Empty;
	public string Email { get; set; } = String.Empty;
	public string Phone { get; set; } = String.Empty;
    public List<Order> Orders { get;set;} = new();
}