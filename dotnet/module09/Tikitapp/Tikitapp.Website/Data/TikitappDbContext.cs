using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Data;

public class TikitappDbContext : DbContext {

	public TikitappDbContext(DbContextOptions<TikitappDbContext> options)
		: base(options) { }

	public virtual DbSet<Artist> Artists => Set<Artist>();
	public virtual DbSet<Venue> Venues => Set<Venue>();
	public virtual DbSet<Show> Shows => Set<Show>();
	public virtual DbSet<Basket> Baskets => Set<Basket>();
	public virtual DbSet<TicketType> TicketTypes => Set<TicketType>();
	public virtual DbSet<Ticket> Tickets => Set<Ticket>();

	protected override void OnModelCreating(ModelBuilder builder) {

		ConfigureSlugs(builder);

		builder.Entity<Artist>(entity => {
			entity.HasMany(e => e.Shows).WithOne(e => e.Artist);
		});
		builder.Entity<Venue>(entity => {
			entity.Property(e => e.TimeZoneId).HasMaxLength(50).IsUnicode(false);
			entity.Property(e => e.CultureInfoName).HasMaxLength(50).IsUnicode(false);
			entity.HasMany(e => e.Shows).WithOne(e => e.Venue);
		});
		builder.Entity<TicketType>(entity => {
			entity.Property(e => e.Price).HasColumnType("money");
		});
		builder.Entity<Basket>(entity => {
			entity.HasMany(e => e.Tickets).WithOne(e => e.Basket);
		});
	}

	// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
	private bool IsSlug(PropertyInfo prop) => prop != null && prop.PropertyType == typeof(string) && prop.Name == "Slug";

	private void ConfigureSlugs(ModelBuilder builder) {
		var entities = builder.Model.GetEntityTypes();
		var properties = entities.SelectMany(entity => entity.GetProperties());
		var slugs = properties.Where(p => IsSlug(p.PropertyInfo));
		Console.WriteLine(slugs);
		foreach (var slug in slugs) {
			Console.WriteLine(slug.DeclaringEntityType.Name);
			slug.SetIsUnicode(false);
			slug.SetMaxLength(100);
			slug.IsIndex();
		}
	}
}

