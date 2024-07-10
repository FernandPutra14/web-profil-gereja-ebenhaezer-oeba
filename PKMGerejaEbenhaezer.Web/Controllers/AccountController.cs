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
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IToastrNotificationService _notificationService;

        public AccountController(ISignInManager signInManager,
            AppDbContext appDbContext, 
            ILogger<AccountController> logger, 
            IToastrNotificationService notificationService)
        {
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = logger;
            _notificationService = notificationService;
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
                ModelState.AddModelError(string.Empty, "Login Gagal!. User name atau password salah.");
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
            var result = await _signInManager.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [AllowAnonymous]
        public IActionResult AccessDenied(string? returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        //Edit Akun
        public IActionResult Edit()
        {
            var userName = User.Identity?.Name;

            if (userName is null) return RedirectToAction(nameof(Login));

            return View(new EditVM { UserName = userName });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid)
                return View(editVM);

            var userName = User.Identity?.Name;
            var user = await _appDbContext.AppUserTable
                .Where(p => p.UserName == userName).FirstOrDefaultAsync();

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Anda harus login terlebih dahulu untuk merubah password");
                return View(editVM);
            }

            var duplikasiNama = await _appDbContext.AppUserTable
                .AnyAsync(u => u.Id != user.Id && u.UserName == editVM.UserName);

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(editVM.UserName),
                    $"{editVM.UserName} sudah digunakan!. Gunakan nama lain.");
                return View(editVM);
            }

            var hasher = new PasswordHasher<AppUser>();

            if (editVM.Password is not null)
            {
                var verificationResult = hasher.VerifyHashedPassword(null,
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
                user.PasswordHash = hasher.HashPassword(null, editVM.Password);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal, terjadi error saat menyimpan ke database. Silahkan laporkan ke administrator");
                _logger.LogError(
                """
                    Edit Akun Gagal. 
                    User = {0}.
                    Exception : {1}
                """, userName, ex.ToString());
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
