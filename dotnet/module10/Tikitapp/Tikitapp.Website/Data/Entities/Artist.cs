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