using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah;
using System.Security.Cryptography;

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

        public async Task<IActionResult> Index()
        {
            var daftarIbadah = await _appDbContext.IbadahTable
                .Include(i => i.KategoriIbadah)
                .Include(i => i.Pendeta)
                .AsNoTracking().ToListAsync();

            return View(daftarIbadah);
        }

        //Tambah Ibadah
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            //Simpan ke database
            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == tambahVM.IdKategoriIbadah).FirstOrDefaultAsync();

            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == tambahVM.IdPendeta).FirstOrDefaultAsync();

            var ibadah = new Ibadah
            {
                Id = 0,
                Judul = tambahVM.Judul,
                Deskripsi = tambahVM.Deskripsi,
                NasPembimbing = tambahVM.NasPembimbing,
                TanggalIbadah = tambahVM.TanggalIbadah,
                Tempat = tambahVM.Tempat,
                KategoriIbadah = kategori,
                Pendeta = pendeta,
            };

            _appDbContext.IbadahTable.Add(ibadah);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal. Laporkan error ke administrator");
                _logger.LogError("Tambah. Simpan Gagal. Exception {0}", ex.ToString());
                return View(tambahVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Edit Ibadah
        public async Task<IActionResult> Edit(int id)
        {
            var ibadah = await _appDbContext.IbadahTable
                .Include(i => i.KategoriIbadah)
                .Include(i => i.Pendeta)
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            if (ibadah is null) return NotFound();

            return View(new EditVM
            {
                Id = ibadah.Id,
                Judul = ibadah.Judul,
                Deskripsi = ibadah.Judul,
                NasPembimbing = ibadah.NasPembimbing,
                TanggalIbadah = ibadah.TanggalIbadah,
                Tempat = ibadah.Tempat,
                IdKategoriIbadah = ibadah.KategoriIbadah?.Id,
                IdPendeta = ibadah.Pendeta?.Id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(editVM);

            var ibadah = await _appDbContext.IbadahTable
                .Where(i => i.Id == editVM.Id).FirstOrDefaultAsync();

            if(ibadah is null)
            {
                ModelState.AddModelError(string.Empty, "Ibadah yang ingin diubah tidak ditemukan");
                return View(editVM);
            }

            //Simpan ke database
            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == editVM.IdKategoriIbadah).FirstOrDefaultAsync();
            var pendeta = await _appDbContext.PendetaTable
                .Where(k => k.Id == editVM.IdPendeta).FirstOrDefaultAsync();

            ibadah.Judul = editVM.Judul;
            ibadah.Deskripsi = editVM.Deskripsi;
            ibadah.NasPembimbing = editVM.NasPembimbing;
            ibadah.TanggalIbadah = editVM.TanggalIbadah;
            ibadah.Tempat = editVM.Tempat;
            ibadah.KategoriIbadah = kategori;
            ibadah.Pendeta = pendeta;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal. Laporkan error ke administrator!");
                _logger.LogError("Edit. Simpan Gagal. Exception {0}", ex.ToString());
                return View(editVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Hapus Ibadah
        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            var ibadah = await _appDbContext.IbadahTable
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            if (ibadah is null) return NotFound();

            _appDbContext.IbadahTable.Remove(ibadah);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Hapus. Simpan Gagal. Exception {0}", ex.ToString());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
