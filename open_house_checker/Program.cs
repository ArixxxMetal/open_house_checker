using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using open_house_checker.Models.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TEST_PROJECTSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OpenHouseDB"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(option =>
{
    option.LoginPath = "/Login/Index";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    option.AccessDeniedPath = "/Login/Index";
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CheckIn}/{action=Index}/{id?}");

app.Run();
