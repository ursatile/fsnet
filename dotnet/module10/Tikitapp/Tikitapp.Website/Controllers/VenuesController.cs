using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Controllers;

public class VenuesController : Controller {
	private readonly ILogger<VenuesController> logger;
	private readonly TikitappDbContext db;

	public VenuesController(ILogger<VenuesController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	public IActionResult Index() {
		var venues = db.Venues.ToList();
		return View(venues);
	}

	public IActionResult Shows(string id) {
		var venue = db.Venues
			.Include(v => v.Shows).ThenInclude(s => s.Artist)
			.Include(v => v.Shows).ThenInclude(s => s.TicketTypes)
			.FirstOrDefault(a => a.Slug == id);
		if (venue == default) return NotFound();
		return View(venue);
	}
}
