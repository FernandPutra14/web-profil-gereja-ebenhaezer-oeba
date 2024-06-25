using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add configurations
builder.Services.Configure<PhotoFileSettingsOptions>(builder.Configuration
    .GetSection(PhotoFileSettingsOptions.PhotoFileSettings));
builder.Services.AddSingleton((sp) =>
{
    return sp.GetRequiredService<IOptions<PhotoFileSettingsOptions>>().Value;
});

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ISignInManager, SignInManager>();

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

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Dashboard",
    pattern: "Dashboard/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
