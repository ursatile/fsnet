using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tikitapp.Website.Data;
using Tikitapp.Website.Models;

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
}
