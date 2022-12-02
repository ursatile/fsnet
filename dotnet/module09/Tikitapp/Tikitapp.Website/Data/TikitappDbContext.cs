using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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

	private static bool IsSlug(IMutableProperty prop) {
		var info = prop.PropertyInfo;
		if (info == null) return false;
		if (info.PropertyType != typeof(string)) return false;
		return info.Name == "Slug";
	}

	private static void ConfigureSlugs(ModelBuilder builder) {
		foreach (var entity in builder.Model.GetEntityTypes()) {
			var properties = entity.GetProperties();
			foreach (var slug in properties.Where(IsSlug)) {
				slug.SetIsUnicode(false);
				slug.SetMaxLength(100);
				entity.AddIndex(slug);
			}
		}
	}
}

