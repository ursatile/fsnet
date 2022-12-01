using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tikitapp.Website.Tests.WebTests;

public class ArtistsTests : IClassFixture<WebApplicationFactory<Program>> {

	private readonly WebApplicationFactory<Program> factory = new();

	[Fact]
	public async Task GET_Artists_Index_Returns_List_Of_Artists() {
		var db = await TestDatabase.CreateDbContext().PopulateWithTestDataAsync();
		var client = factory.WithWebHostBuilder(builder => {
			builder.ConfigureServices(services => services.AddSingleton(db));
		}).CreateClient();
		var response = await client.GetStringAsync("/artists");
		response.ShouldContain(TestData.Artist1.Name);
		response.ShouldContain(TestData.Artist2.Name);
	}
}
