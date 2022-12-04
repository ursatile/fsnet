using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.Controllers;

public class ShowsController : Controller {
	private readonly ILogger<ShowsController> logger;
	private readonly TikitappDbContext db;

	public ShowsController(ILogger<ShowsController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	public IActionResult Tickets(Guid id) {
		var show = db.Shows
			.Include(show => show.TicketTypes)
			.Include(show => show.Artist)
			.Include(show => show.Venue)
			.FirstOrDefault(show => show.Id == id);
		logger.LogDebug("Looking for show with ID", id);
		if (show == default) return NotFound();
		return View(show);
	}
}
