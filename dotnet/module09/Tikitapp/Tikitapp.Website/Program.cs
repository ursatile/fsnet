using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

var builder = WebApplication.CreateBuilder(args);

// Add this line BEFORE AddControllersWithViews() to use lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();
var sqlConnectionString = builder.Configuration.GetConnectionString("Tikitapp");
builder.Services.AddDbContext<TikitappDbContext>(
	options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();