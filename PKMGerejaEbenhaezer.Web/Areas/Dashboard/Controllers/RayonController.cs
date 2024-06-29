using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class RayonController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<RayonController> _logger;

        public RayonController(
            AppDbContext appDbContext,
            ILogger<RayonController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarRayon = await _appDbContext.RayonTable
                .Include(r => r.FotoKetua)
                .AsNoTracking().ToListAsync();

            return View(daftarRayon);
        }

        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            var foto = await _appDbContext.FotoTable.Where(f => f.Id == tambahVM.IdFoto)
                .FirstOrDefaultAsync();

            if(foto == null)
            {
                ModelState.AddModelError(string.Empty, "Foto tidak ada");
                return View(tambahVM);
            }

            //Simpan data rayon
            var newRayon = new Rayon
            {
                Id = 0,
                Nama = tambahVM.Nama,
                KetuaRayon = tambahVM.KetuaRayon,
                FotoKetua = foto,
                JumlahLakiLaki = tambahVM.JumlahLakiLaki,
                JumlahPerempuan = tambahVM.JumlahPerempuan,
                JumlahAnak = tambahVM.JumlahAnak,
                JumlahRemaja = tambahVM.JumlahRemaja,
                JumlahPemuda = tambahVM.JumlahPemuda,
                JumlahDewasa = tambahVM.JumlahDewasa,
                JumlahLansia = tambahVM.JumlahLansia,
            };

            var changeTracker = _appDbContext.RayonTable.Add(newRayon);

            try
            {
                var result = await _appDbContext.SaveChangesAsync();

                if (result <= 0) throw new Exception("Jumlah entitas yang disimpan 0");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Data gagal disimpan!");
                _logger.LogError("Tambah Rayon Gagal! Error : {0}", ex.Message);
                return View(tambahVM);
            }

            _logger.LogInformation("Tambah Rayon Sukses! Id : {0}", changeTracker.Entity.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rayon = await _appDbContext.RayonTable.Where(r => r.Id == id)
                .Include(r => r.FotoKetua)
                .AsNoTracking().FirstOrDefaultAsync();

            if (rayon == null) return NotFound();

            return View(new EditVM
            {
                Id = id,
                Nama = rayon.Nama,
                IdFoto = rayon.FotoKetua?.Id,
                KetuaRayon = rayon.KetuaRayon,
                JumlahLakiLaki = rayon.JumlahLakiLaki,
                JumlahPerempuan = rayon.JumlahPerempuan,
                JumlahAnak = rayon.JumlahAnak,
                JumlahRemaja = rayon.JumlahRemaja,
                JumlahPemuda = rayon.JumlahPemuda,
                JumlahDewasa = rayon.JumlahDewasa,
                JumlahLansia = rayon.JumlahLansia,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(editVM);

            var rayon = await _appDbContext.RayonTable.Where(r => r.Id == editVM.Id).FirstOrDefaultAsync();
            if (rayon == null)
            {
                ModelState.AddModelError(string.Empty, "Data yang akan disimpan tidak ada di database!");
                _logger.LogError("Edit Rayon Gagal! Data dengan Id {0} tidak ada!", editVM.Id);
                return View(editVM);
            }

            if(editVM.IdFoto is not null)
            {
                var foto = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if(foto is null)
                {
                    ModelState.AddModelError(string.Empty, "Foto tidak ada");
                    return View(editVM);
                }
            }

            //Simpan perubahan
            rayon.Nama = editVM.Nama;
            rayon.KetuaRayon = editVM.KetuaRayon;
            rayon.JumlahLakiLaki = editVM.JumlahLakiLaki;
            rayon.JumlahPerempuan = editVM.JumlahPerempuan;
            rayon.JumlahAnak = editVM.JumlahAnak;
            rayon.JumlahRemaja = editVM.JumlahRemaja;
            rayon.JumlahPemuda = editVM.JumlahPemuda;
            rayon.JumlahDewasa = editVM.JumlahDewasa;
            rayon.JumlahLansia = editVM.JumlahLansia;

            if (editVM.IdFoto is not null) 
            {
                rayon.FotoKetua = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .FirstOrDefaultAsync();
            }

            try
            {
                var result = await _appDbContext.SaveChangesAsync();

                if (result <= 0) throw new Exception("Tidak ada data yang disimpan");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Proses simpan gagal!");
                _logger.LogError("Edit Rayon Gagal! Error : {0}", ex.Message);
                return View(editVM);
            }

            _logger.LogInformation("Edit Rayon Sukses : Id {0}", editVM.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            var rayon = await _appDbContext.RayonTable.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (rayon == null) return NotFound(id);

            _appDbContext.RayonTable.Remove(rayon);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Rayon Gagal Id {0} : {1}", id, ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
