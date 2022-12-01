namespace Tikitapp.Website.Tests;

public static class TestData {
	private static Guid TestGuid(char c) => Guid.Parse(String.Empty.PadLeft(32, c));
	public static readonly Artist Artist1 = new() { Id = TestGuid('1'), Name = "ARTIST 1" };
	public static readonly Artist Artist2 = new() { Id = TestGuid('2'), Name = "ARTIST 2" };
}
