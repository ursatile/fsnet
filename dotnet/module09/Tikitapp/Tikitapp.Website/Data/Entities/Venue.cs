namespace Tikitapp.Website.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Venue {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(100)]
	public string Slug { get; set; } = String.Empty;
	public string StreetAddress { get; set; } = String.Empty;
    public string CountryCode { get;set;} = string.Empty;
	public virtual List<Show> Shows { get; set; } = new();
}