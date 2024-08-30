using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Models.Account;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using System.Drawing;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISignInManager _signInManager;
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IToastrNotificationService _notificationService;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(
            ISignInManager signInManager,
            IAppDbContext appDbContext,
            ILogger<AccountController> logger,
            IToastrNotificationService notificationService,
            IPasswordHasher<AppUser> passwordHasher)
        {
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = logger;
            _notificationService = notificationService;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home", new { Area = "Dashboard" });
            ViewData["returnUrl"] = returnUrl;

            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home", new { Area = "Dashboard" });
            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid == false) return View(loginVM);

            var result = await _signInManager.SignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe);

            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Login Gagal! Username atau password salah.");
                return View(loginVM);
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Info,
                Title = $"Selamat datang kembali {loginVM.UserName}"
            });
            return Redirect(returnUrl!);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any)]
        public IActionResult AccessDenied(string? returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        //Edit Akun
        public async Task<IActionResult> Edit()
        {
            var user = await _signInManager.GetSignedInUser();

            if (user is null) return RedirectToAction(nameof(Login));

            return View(new EditVM { UserName = user.UserName });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid)
                return View(editVM);
            
            var user = await _signInManager.GetSignedInUser();

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Anda harus login terlebih dahulu untuk mengubah password");
                return View(editVM);
            }

            var duplikasiNama = await _appDbContext.AppUserTable
                .AnyAsync(u => u.Id != user.Id && u.UserName == editVM.UserName);

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(editVM.UserName),
                    $"{editVM.UserName} sudah digunakan!. Gunakan username lainnya.");
                return View(editVM);
            }


            if (editVM.Password is not null)
            {
                var verificationResult = _passwordHasher.VerifyHashedPassword(null,
                    user.PasswordHash, editVM.Password);

                if (verificationResult == PasswordVerificationResult.Success ||
                    verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    ModelState.AddModelError(nameof(editVM.Password), "Password baru sama dengan password lama");
                    return View(editVM);
                }
            }

            //Simpan ke database
            user.UserName = editVM.UserName;

            if (editVM.Password is not null)
                user.PasswordHash = _passwordHasher.HashPassword(null, editVM.Password);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal, terjadi error saat menyimpan ke database. Silahkan laporkan ke administrator");
                _logger.LogError(ex, "Edit Akun Gagal. User = {@userName}. Message = {@message}", user.UserName, ex.Message);
                return View(editVM);
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Akun Sukses Diubah",
                Message = "Silahkan login dengan Username dan Password baru anda"
            });
            return RedirectToAction(nameof(Login));
        }
    }
}
