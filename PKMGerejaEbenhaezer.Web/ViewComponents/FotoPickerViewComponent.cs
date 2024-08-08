using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Models;

namespace PKMGerejaEbenhaezer.Web.ViewComponents
{
    public class FotoPickerViewComponent : ViewComponent
    {
        private readonly IAppDbContext _appDbContext;

        public FotoPickerViewComponent(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string inputName, bool multi, int[]? values = null)
        {
            var daftarFoto = await _appDbContext.FotoTable.OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

            return View(new FotoPickerVM
            {
                DaftarFoto = daftarFoto,
                Multi = multi,
                InputName = inputName,
                DaftarIdFoto = values?.ToList()
            });
        }
    }
}
