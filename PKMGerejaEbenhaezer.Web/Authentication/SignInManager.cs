
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using System.Security.Claims;

namespace PKMGerejaEbenhaezer.Web.Authentication
{
    public class SignInManager : ISignInManager
    {
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<SignInManager> _logger;

        public SignInManager(ILogger<SignInManager> logger, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<bool> SignInAsync(string userName, string password, bool rememberMe)
        {
            //Cek apakah akun ada
            var user = await _appDbContext.AppUserTable.Where(a => a.UserName == userName).FirstOrDefaultAsync();
            if(user == null) return false;

            //Bandingkan password dengan password di database
            var hasher = new PasswordHasher<AppUser>();
            var result = hasher.VerifyHashedPassword(null, user.Password, password);
            if(result == PasswordVerificationResult.Failed) return false;

            //Buat claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //Sign In
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
            };

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            _logger.LogInformation("{UserName} logged in at {Time} UTC", user.UserName, DateTime.UtcNow);

            return true;
        }

        public async Task<bool> SignOut()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }
    }
}
