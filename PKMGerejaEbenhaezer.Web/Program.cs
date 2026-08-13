using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;
using PKMGerejaEbenhaezer.Web.Services.FileHelper;
using PKMGerejaEbenhaezer.Web.Services.ImageCompress;
using PKMGerejaEbenhaezer.Web.Services.PDF;
using PKMGerejaEbenhaezer.Web.Services.ScaniiApi;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

var builder = WebApplication.CreateBuilder(args);

// Add configurations
builder.Services.Configure<PhotoFileSettingsOptions>(builder.Configuration
    .GetSection(PhotoFileSettingsOptions.PhotoFileSettings));
builder.Services.AddScoped((sp) =>
{
    return sp.GetRequiredService<IOptionsSnapshot<PhotoFileSettingsOptions>>().Value;
});

builder.Services.Configure<PDFFileSettingsOptions>(builder.Configuration
    .GetSection(PDFFileSettingsOptions.PDFFileSettings));
builder.Services.AddScoped((sp) =>
{
    return sp.GetRequiredService<IOptionsSnapshot<PDFFileSettingsOptions>>().Value;
});

builder.Services.Configure<ImageCompressionOptions>(builder.Configuration
    .GetSection(ImageCompressionOptions.ImageCompression));
builder.Services.AddScoped(sp =>
{
    return sp.GetRequiredService<IOptionsSnapshot<ImageCompressionOptions>>().Value;
});

builder.Services.Configure<ScaniiApiSettingsOptions>(builder.Configuration
    .GetSection(ScaniiApiSettingsOptions.ScaniiApiSettings));
builder.Services.AddScoped(sp =>
{
    return sp.GetRequiredService<IOptionsSnapshot<ScaniiApiSettingsOptions>>().Value;
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
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });

builder.Services.AddSession();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISignInManager, SignInManager>();
builder.Services.AddScoped<IPDFUploadService, PDFUploadService>();
builder.Services.AddScoped<IToastrNotificationService, ToastrNotificationService>();
builder.Services.AddScoped<IImageCompressService, ImageCompressService>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
builder.Services.AddScoped<IFileHelperService, FileHelperService>();

builder.Services.AddHttpClient<IBeebeleApiService, BeebleApiService>(options =>
{
    options.BaseAddress = new Uri("https://beeble.vercel.app/api/v1/passage/");
});

builder.Services.AddHttpClient<IScaniiApiService, ScaniiApiService>(options =>
{
    options.BaseAddress = new Uri("https://api-ap2.scanii.com/v2.2/");

    var key = $"{builder.Configuration.GetValue<string>($"{ScaniiApiSettingsOptions.ScaniiApiSettings}:{nameof(ScaniiApiSettingsOptions.ApiKey)}")}:{builder.Configuration.GetValue<string>($"{ScaniiApiSettingsOptions.ScaniiApiSettings}:{nameof(ScaniiApiSettingsOptions.Secret)}")}";
    var keyBase64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(key));
    options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBase64);
});

builder.Services.AddMemoryCache();

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromMinutes(20)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCode{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Dashboard",
    pattern: "Dashboard/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

app.Run();