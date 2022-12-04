using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Tests;

public class TestDatabase {
	public static TikitappDbContext CreateDbContext() {
		var dbName = Guid.NewGuid().ToString(); // use a unique database name each time
		var options = new DbContextOptionsBuilder<TikitappDbContext>()
			.UseInMemoryDatabase(databaseName: dbName)
			.Options;
		return new TikitappDbContext(options);
	}
}
