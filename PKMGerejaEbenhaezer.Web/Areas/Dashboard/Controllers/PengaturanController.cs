using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengaturan;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using System.Drawing;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;

[Authorize(Roles = AppUserRoles.SuperAdmin)]
[Area("Dashboard")]
public class PengaturanController : Controller
{
    private readonly IOptionsMonitor<PhotoFileSettingsOptions> _photoFileSettingsOptions;
    private readonly IOptionsMonitor<ImageCompressionOptions> _imageCompressionOptions;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IToastrNotificationService _notificationService;
    private readonly ILogger<PengaturanController> _logger;

    public PengaturanController(
        IOptionsMonitor<PhotoFileSettingsOptions> photoFileSettings,
        IOptionsMonitor<ImageCompressionOptions> imageCompressionOptions,
        IWebHostEnvironment webHostEnvironment,
        IToastrNotificationService notificationService,
        ILogger<PengaturanController> logger)
    {
        _photoFileSettingsOptions = photoFileSettings;
        _imageCompressionOptions = imageCompressionOptions;
        _webHostEnvironment = webHostEnvironment;
        _notificationService = notificationService;
        _logger = logger;
    }

    public IActionResult Foto()
    {
        var photoFileSettings = _photoFileSettingsOptions.CurrentValue;
        var imageCompressionSettings = _imageCompressionOptions.CurrentValue;

        return View(new FotoVM
        {
            MinSizeLimit = photoFileSettings.MinSizeLimit / 1024,
            MaxSizeLimit = photoFileSettings.MaxSizeLimit / 1024,
            CompressionQuality = imageCompressionSettings.CompressionQuality,
            SmallWidth = imageCompressionSettings.Small.Width,
            SmallHeight = imageCompressionSettings.Small.Height,
            MediumWidth = imageCompressionSettings.Medium.Width,
            MediumHeight = imageCompressionSettings.Medium.Height,
            LargeWidth = imageCompressionSettings.Large.Width,
            LargeHeight = imageCompressionSettings.Large.Height,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Foto(FotoVM fotoVM)
    {
        if (!ModelState.IsValid) return View(fotoVM);

        var photoFileSettings = _photoFileSettingsOptions.CurrentValue;
        var imageCompressionOptions = _imageCompressionOptions.CurrentValue;

        photoFileSettings.MinSizeLimit = fotoVM.MinSizeLimit * 1024;
        photoFileSettings.MaxSizeLimit = fotoVM.MaxSizeLimit * 1024;

        imageCompressionOptions.CompressionQuality = fotoVM.CompressionQuality;
        imageCompressionOptions.Small = new Size(fotoVM.SmallWidth, fotoVM.SmallHeight);
        imageCompressionOptions.Medium = new Size(fotoVM.MediumWidth, fotoVM.MediumHeight);
        imageCompressionOptions.Large = new Size(fotoVM.LargeWidth, fotoVM.LargeHeight);

        var customSettings = new Dictionary<string, object>
        {
            { PhotoFileSettingsOptions.PhotoFileSettings, photoFileSettings },
            { ImageCompressionOptions.ImageCompression, imageCompressionOptions },
        };

        var customSettingsJson = JsonConvert.SerializeObject(customSettings, Formatting.Indented);
        var filePath = $"{_webHostEnvironment.ContentRootPath}/{CustomConfigurationProviders.CustomJson}";

        try
        {
            using TextWriter writer = new StreamWriter(filePath, append: false);
            await writer.WriteAsync(customSettingsJson);

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pengaturan Foto Sukses Diubah"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error when try to save configuration to Path : {@pathFile}. " +
                "Message : {@message}. TimeStamp : {@timeStamp}",
                filePath,
                ex.Message,
                DateTime.Now);

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Error,
                Title = "Gagal Menyimpan Pengaturan!",
                Message = "Hubungi administrator untuk melaporkan error"
            });
        }

        return RedirectToAction(nameof(Foto));
    }
}
