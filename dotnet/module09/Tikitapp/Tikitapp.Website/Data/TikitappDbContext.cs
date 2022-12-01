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

	
	}

	private bool IsSlug(PropertyInfo prop) => prop.PropertyType == typeof(string) && prop.Name == "Slug";

	private void ConfigureSlugs(ModelBuilder builder) {
		var entities = builder.Model.GetEntityTypes();
		var properties = entities.SelectMany(entity => entity.GetProperties());
		var slugs = properties.Where(p => IsSlug(p.PropertyInfo));
		foreach (var slug in slugs) {
			slug.SetIsUnicode(false);
			slug.SetMaxLength(100);
			slug.IsIndex();
		}
	}
}

