using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Tests.Controllers;

public class ArtistsControllerTests {
    private static Guid TestGuid(char c) => Guid.Parse(String.Empty.PadLeft(32, c));

    private readonly TikitappDbContext db;
    private readonly NullLogger<ArtistsController> logger = new();
    private readonly Artist testArtist1 = new() { Id = TestGuid('1'), Name = "ARTIST 1" };
    private readonly Artist testArtist2 = new() { Id = TestGuid('2'), Name = "ARTIST 2" };

    public ArtistsControllerTests() {
        var dbName = Guid.NewGuid().ToString(); // use a unique database name each time
        var options = new DbContextOptionsBuilder<TikitappDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        db = new TikitappDbContext(options);
        db.Database.EnsureCreated();
        db.Artists.Add(testArtist1);
        db.Artists.Add(testArtist2);
        db.SaveChanges();
    }

    [Fact]
    public void Artists_Index_Returns_List_Of_Artists() {
        var controller = new ArtistsController(logger, db);
        var result = controller.Index() as ViewResult;
        result.ShouldNotBeNull();
        var model = result.Model as IList<Artist>;
        model.ShouldNotBeNull();
        model.Count.ShouldBe(2);
    }

    [Fact]
    public void Artists_View_Returns_Artist() {
        var controller = new ArtistsController(logger, db);
        var result = controller.View(testArtist1.Id) as ViewResult;
        result.ShouldNotBeNull();
        var model = result.Model as Artist;
        model.ShouldNotBeNull();
        model.Name.ShouldBe(testArtist1.Name);
    }
}

