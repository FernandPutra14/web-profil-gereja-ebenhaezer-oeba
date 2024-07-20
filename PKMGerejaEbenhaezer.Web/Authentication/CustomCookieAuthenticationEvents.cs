using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using System.Security.Claims;

namespace PKMGerejaEbenhaezer.Web.Authentication
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IAppDbContext _appDbContext;

        public CustomCookieAuthenticationEvents(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var lastChanged = userPrincipal?.Claims
                .Where(c => c.Type == CustomClaimTypes.LastChanged)
                .Select(c => c.Value).FirstOrDefault();

            var userName = userPrincipal?.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value).FirstOrDefault();

            if(string.IsNullOrEmpty(lastChanged) || string.IsNullOrEmpty(userName) 
                || !await ValidateLastChanged(userName, lastChanged))
            {
                context.RejectPrincipal();
            }

            await base.ValidatePrincipal(context);
        }

        private async Task<bool> ValidateLastChanged(string userName, string lastChanged)
        {
            var user = await _appDbContext.AppUserTable
                .Where(u => u.UserName == userName).FirstOrDefaultAsync();

            if (user is null) return false;

            if (!long.TryParse(lastChanged, out long lastChangedTicks)) return false;

            var lastChangedParsed = new DateTime(lastChangedTicks);

            if (lastChangedParsed != user.LastChanged) return false;

            return true;
        }
    }
}
