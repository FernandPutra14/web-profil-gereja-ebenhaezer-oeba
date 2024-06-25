using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class RayonController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PhotoFileSettingsOptions _photoFileSettings;
        private readonly ILogger<RayonController> _logger;

        public RayonController(
            IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext,
            PhotoFileSettingsOptions photoFileSettings,
            ILogger<RayonController> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _photoFileSettings = photoFileSettings;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarRayon = await _appDbContext.RayonTable.AsNoTracking().ToListAsync();

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

            //Simpan foto
            var fileFormContent = await FileHelpers.ProcessFormFile<TambahVM>(
                tambahVM.FotoKetua,
                ModelState,
                _photoFileSettings.PermittedFileExtensions,
                _photoFileSettings.SizeLimit);

            if (!ModelState.IsValid) return View(tambahVM);

            //Simpan data rayon
            var newRayon = new Rayon
            {
                Id = 0,
                Nama = tambahVM.Nama,
                KetuaRayon = tambahVM.KetuaRayon,
                FotoKetua = fileFormContent,
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

                if (result <= 0) throw new Exception("Simpan pengumuman gagal! Jumlah entitas yang disimpan 0");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Data gagal disimpan!");
                _logger.LogError("Tambah Rayon Gagal : {0}", ex.Message);
                return View(tambahVM);
            }

            _logger.LogInformation("Tambah Rayon Sukses : Id {0}", changeTracker.Entity.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rayon = await _appDbContext.RayonTable.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync();

            if (rayon == null) return NotFound();

            return View(new EditVM
            {
                Id = id,
                Nama = rayon.Nama,
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

            //Simpan foto baru kalau ada
            if (editVM.FotoKetua != null)
            {
                var fileFormContent = await FileHelpers.ProcessFormFile<EditVM>(
                editVM.FotoKetua,
                ModelState,
                _photoFileSettings.PermittedFileExtensions,
                _photoFileSettings.SizeLimit);

                if (!ModelState.IsValid) return View(editVM);

                rayon.FotoKetua = fileFormContent;
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

            try
            {
                var result = await _appDbContext.SaveChangesAsync();

                if (result <= 0) throw new Exception("Tidak ada data yang disimpan");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Proses simpan gagal!");
                _logger.LogError("Edit Rayon Gagal Id {0} : {1}", editVM.Id, ex.Message);
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
