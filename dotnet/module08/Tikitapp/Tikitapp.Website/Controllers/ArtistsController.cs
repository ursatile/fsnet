using Microsoft.AspNetCore.Mvc;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Controllers;

public class ArtistsController : Controller {
	private readonly ILogger<ArtistsController> logger;
	private readonly TikitappDbContext db;

	public ArtistsController(ILogger<ArtistsController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	public IActionResult Index() {
		var artists = db.Artists.ToList();
		return View(artists);
	}

	public IActionResult Shows(string id) {
		var artist = db.Artists.FirstOrDefault(a => a.Slug == id);
		if (artist == default) return NotFound();
		return View(artist);
	}
}
