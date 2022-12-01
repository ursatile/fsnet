namespace Tikitapp.Website.Tests.WebTests;

public class HomeTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> factory = new();
    [Fact]
    public async Task GET_Home_Index_Returns_Valid_Html() {
        var client = factory.CreateClient();
        var response = await client.GetAsync("/");
        response.IsSuccessStatusCode.ShouldBe(true);
        var body = await response.Content.ReadAsStringAsync();
        body.ShouldContain("Welcome");
    }
}
