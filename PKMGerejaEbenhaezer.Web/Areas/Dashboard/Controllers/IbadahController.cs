using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class IbadahController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<IbadahController> _logger;

        public IbadahController(AppDbContext appDbContext, ILogger<IbadahController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
    }
}
