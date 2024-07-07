using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah;

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


        //Daftar Kategori
        public async Task<IActionResult> Kategori()
        {
            var daftarKategori = await _appDbContext.KategoriIbadahTable
                .AsNoTracking().ToListAsync();

            return View(daftarKategori);
        }

        //Tambah Kategori
        public IActionResult TambahKategori()
        {
            return View(new TambahKategoriVM());
        }

        [HttpPost]
        public async Task<IActionResult> TambahKategori(TambahKategoriVM tambahKategoriVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahKategoriVM);

            var duplikasiNama =  await _appDbContext.KategoriIbadahTable
                .AnyAsync(k => k.Nama.ToLower().Trim() == tambahKategoriVM.Nama.ToLower().Trim());

            if (duplikasiNama) 
            {
                ModelState.AddModelError(nameof(TambahKategoriVM.Nama), $"{tambahKategoriVM.Nama} sudah digunakan!. Gunakan nama lain");
                return View(tambahKategoriVM);
            }

            //Simpan ke database
            var kategoriIbadah = new KategoriIbadah
            {
                Id = 0,
                Nama = tambahKategoriVM.Nama,
                Durasi = tambahKategoriVM.Durasi,
            };

            _appDbContext.KategoriIbadahTable.Add(kategoriIbadah);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error menyimpan data. Segera laporkan ke administrator!");
                _logger.LogError("Tambah Kategori Ibadah. Exception : {0}", ex.ToString());
                return View(tambahKategoriVM);
            }

            return RedirectToAction(nameof(Kategori));
        }

        //Hapus Kategori
        [HttpPost]
        public async Task<IActionResult> HapusKategori(int id)
        {
            //Validasi
            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == id).FirstOrDefaultAsync();

            if (kategori is null) return NotFound();

            _appDbContext.KategoriIbadahTable.Remove(kategori);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Hapus Kategori. Simpan Gagal. Exception : {0}", ex.ToString());
            }

            return RedirectToAction(nameof(Kategori));
        }
    }
}
