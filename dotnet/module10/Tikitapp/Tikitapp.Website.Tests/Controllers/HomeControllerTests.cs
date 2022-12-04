namespace Tikitapp.Website.Tests.Controllers;

public class HomeControllerTests {

	private readonly NullLogger<HomeController> logger = new();

	[Fact]
	public void Home_Index_Returns_ViewResult() {
		var c = new HomeController(logger);
		var result = c.Index();
		result.ShouldBeOfType<ViewResult>();
	}
}
