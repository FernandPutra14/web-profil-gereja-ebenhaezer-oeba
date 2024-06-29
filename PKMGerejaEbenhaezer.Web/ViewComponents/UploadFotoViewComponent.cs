﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.ViewComponents
{
    public class UploadFotoViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public UploadFotoViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var daftarFoto = await _appDbContext.FotoTable.OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

            return View(daftarFoto);
        }
    }
}