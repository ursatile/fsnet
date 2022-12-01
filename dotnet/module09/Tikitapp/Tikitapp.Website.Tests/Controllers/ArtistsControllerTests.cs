namespace Tikitapp.Website.Tests.Controllers;

public class ArtistsControllerTests {

	private readonly NullLogger<ArtistsController> logger = new();

	[Fact]
	public async void Artists_Index_Returns_List_Of_Artists() {
		var db = await TestDatabase.CreateDbContext().PopulateWithTestDataAsync();
		var controller = new ArtistsController(logger, db);
		var result = controller.Index() as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as IList<Artist>;
		model.ShouldNotBeNull();
		model.Count.ShouldBe(2);
	}

	[Fact]
	public async void Artists_View_Returns_Artist() {
		var db = await TestDatabase.CreateDbContext().PopulateWithTestDataAsync();
		var controller = new ArtistsController(logger, db);
		var result = controller.View(TestData.Artist1.Slug) as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as Artist;
		model.ShouldNotBeNull();
		model.Name.ShouldBe(TestData.Artist1.Name);
	}
}
