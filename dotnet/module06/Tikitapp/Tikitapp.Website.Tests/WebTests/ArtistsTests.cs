using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Tests.WebTests;

public class ArtistsTests : IClassFixture<WebApplicationFactory<Program>> {

    private readonly WebApplicationFactory<Program> factory = new();

    [Fact]
    public async Task GET_Artists_Index_Returns_List_Of_Artists() {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<TikitappDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var db = new TikitappDbContext(options);
        await db.Database.EnsureCreatedAsync();
        db.Artists.Add(new Artist { Name = "TEST ARTIST 1" });
        db.Artists.Add(new Artist { Name = "TEST ARTIST 2" });
        await db.SaveChangesAsync();

        var client = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => services.AddSingleton(db));
        }).CreateClient();

        var response = await client.GetStringAsync($"/artists");
        response.ShouldContain("TEST ARTIST 1");
        response.ShouldContain("TEST ARTIST 2");
    }
}
