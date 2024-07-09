using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.KategoriIbadah;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Dashboard")]
    public class KategoriIbadahController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<IbadahController> _logger;
        private readonly IToastrNotificationService _notificationService;

        public KategoriIbadahController(AppDbContext appDbContext,
            ILogger<IbadahController> logger,
            IToastrNotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _notificationService = notificationService;
        }


        //Daftar Kategori
        public async Task<IActionResult> Index()
        {
            var daftarKategori = await _appDbContext.KategoriIbadahTable
                .AsNoTracking().ToListAsync();

            return View(daftarKategori);
        }

        //Tambah Kategori
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            var duplikasiNama = await _appDbContext.KategoriIbadahTable
                .AnyAsync(k => k.Nama.ToLower().Trim() == tambahVM.Nama.ToLower().Trim());

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(TambahVM.Nama), 
                    $"{tambahVM.Nama} sudah digunakan!. Gunakan nama lain");
                return View(tambahVM);
            }

            //Simpan ke database
            var kategoriIbadah = new KategoriIbadah
            {
                Id = 0,
                Nama = tambahVM.Nama,
                Durasi = tambahVM.Durasi,
            };

            _appDbContext.KategoriIbadahTable.Add(kategoriIbadah);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error menyimpan data. Segera laporkan ke administrator!");
                _logger.LogError("Tambah Kategori Ibadah. Exception : {0}", ex.ToString());
                return View(tambahVM);
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Kategori ibadah baru berhasil ditambah"
            });
            return RedirectToAction(nameof(Index));
        }

        //Edit Kategori
        public async Task<IActionResult> Edit(int id)
        {
            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == id).FirstOrDefaultAsync();

            if (kategori is null) return NotFound();

            return View(new EditVM 
            { 
                Id = id, 
                Nama = kategori.Nama,
                TotalJam = (int)kategori.Durasi.TotalHours,
                TotalMenit = (int)kategori.Durasi.TotalMinutes % 60,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(editVM);

            var duplikasiNama = await _appDbContext.KategoriIbadahTable
                .AnyAsync(k => k.Id != editVM.Id && k.Nama.ToLower().Trim() == editVM.Nama.ToLower().Trim());
            
            if(duplikasiNama)
            {
                ModelState.AddModelError(nameof(EditVM.Nama), $"{editVM.Nama} sudah digunakan untuk kategori lainnya");
                return View(editVM);
            }

            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == editVM.Id).FirstOrDefaultAsync();

            if(kategori is null)
            {
                //Kasih Notifikasi
                return RedirectToAction(nameof(Index));
            }

            //Simpan ke database
            kategori.Nama = editVM.Nama;
            kategori.Durasi = editVM.Durasi;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error menyimpan data. Silahkan laporkan ke administrator");
                _logger.LogError("Edit Kategori. Simpan Gagal. Exception : {0}", ex.ToString());
                return View(editVM);
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Kategori ibadah berhasil diubah"
            });
            return RedirectToAction(nameof(Index));
        }

        //Hapus Kategori
        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
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
            catch (Exception ex)
            {
                _logger.LogError("Hapus Kategori. Simpan Gagal. Exception : {0}", ex.ToString());
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Error,
                    Title = "Hapus Kategori Ibadah Gagal",
                    Message = "Error terjadi saat mencoba menghapus data dari database. Silahkan laporkan ke administrator"
                });
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Kategori Ibadah sukses dihapus"
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
