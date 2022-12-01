using System.ComponentModel.DataAnnotations;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
}
