using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Controllers;
using PKMGerejaEbenhaezer.Web.Models;

namespace PKMGerejaEbenhaezer.UnitTest.Controller
{
    public class PengumumanControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;

        private readonly PengumumanController _pengumumanController;

        public static IEnumerable<object[]> BulanData { get => Enumerable.Range(1, 12).Select(x => new object[] { x }); }
        public static IEnumerable<object[]> TahunData { get => Enumerable.Range(0, 12).Select(x => new object[] { 2024 - x }); }
        public static IEnumerable<object[]> SearchStringData { get => Enumerable.Range(0, 12).Select(x => new object[] { x.ToString() }); }
        public static IEnumerable<object[]> PageIndexData { get => Enumerable.Range(1, 12).Select(x => new object[] { x }); }
        public static IEnumerable<object[]> InvalidBulanData 
        { 
            get => Enumerable.Range(1, 6).Select(x => new object[] { 1 - x })
                .Concat(Enumerable.Range(1, 6).Select(x => new object[] {12 + x})); 
        }
        public static IEnumerable<object[]> InvalidTahunData { get => Enumerable.Range(1, 12).Select(x => new object[] { 1 - x }); }
        public static IEnumerable<object[]> NegativePageIndexData { get => Enumerable.Range(1, 12).Select(x => new object[] { 1 - x }); }

        public PengumumanControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();

            //SUT
            _pengumumanController = new PengumumanController(_appDbContext.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResult()
        {
            //Arrange
            var daftarPengumuman = new List<Pengumuman>();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string?>());

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>();
        }

        [Fact]
        public async Task Index_Should_ModelItemsHaveCountEqualTo6OrLess()
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string?>());

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Items.Should().HaveCountLessThanOrEqualTo(6);
        }

        [Theory]
        [MemberData(nameof(BulanData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithMonthEqualToBulan(int bulan)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman(month: bulan);
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(bulan, It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string?>());

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Bulan.Should().Be(bulan);
            model.Items.Should().AllSatisfy(p => p.TanggalDiBuat.Month.Should().Be(bulan));
        }

        [Theory]
        [MemberData(nameof(TahunData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithYearEqualToTahun(int tahun)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman(year: tahun);
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(It.IsAny<int?>(), tahun, It.IsAny<int?>(), It.IsAny<string?>());

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Tahun.Should().Be(tahun);
            model.Items.Should().AllSatisfy(p => p.TanggalDiBuat.Year.Should().Be(tahun));
        }

        [Theory]
        [MemberData(nameof(SearchStringData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithJudulOrIsiContainSearchString(string searchString)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman(s: searchString);
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), searchString);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.SearchString.Should().Be(searchString);
            model.Items.Should()
                .OnlyContain(p => p.Judul.ToLower().Contains(searchString.ToLower()) || p.Isi.ToLower().Contains(searchString.ToLower()));
        }

        [Theory]
        [MemberData(nameof(PageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo(int pageIndex)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman(totalPages: pageIndex);
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController
                .Index(It.IsAny<int?>(), It.IsAny<int?>(), pageIndex, It.IsAny<string?>());

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Items.PageIndex.Should().Be(pageIndex);
        }

        [Theory]
        [MemberData(nameof(InvalidBulanData))]
        public async Task Index_Should_ModelBulanNull_WhenBulanOutOfRange(int bulan)
        {

            //Arrange
            var daftarPengumuman = GetDataPengumuman();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController.Index(bulan, null, null, null);

            //Assert 
            bulan.Should().NotBeInRange(1, 12);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Bulan.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(InvalidTahunData))]
        public async Task Index_Should_ModelTahunNull_WhenTahunNegativeOrZero(int tahun)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController.Index(null, tahun, null, null);

            //Assert 
            tahun.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Tahun.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(NegativePageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo1_WhenPageIndexNegativeOfZero(int pageIndex)
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController.Index(null, null, pageIndex, null);

            //Assert 
            pageIndex.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Items.PageIndex.Should().Be(1);
            model.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Index_Should_ModelPageIndexEqualToTotalPages_WhenPageIndexGreaterThanTotalPages()
        {
            //Arrange
            var daftarPengumuman = GetDataPengumuman();
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

            //Act
            var result = await _pengumumanController.Index(null, null, 100, null);

            //Assert 
            daftarPengumuman.Count.Should().BeGreaterThan(6);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Pengumuman>>().Subject;
            model.Items.PageIndex.Should().Be(model.Items.TotalPages);
            model.Items.Should().NotBeEmpty();
        }

        private List<Pengumuman> GetDataPengumuman()
        {
            return new List<Pengumuman>()
            {
                new Pengumuman
                {
                    Id = 1,
                    Judul = "Ibadah Natal Bersama",
                    Isi = "Kami mengundang seluruh jemaat untuk hadir dalam Ibadah Natal yang akan diadakan pada hari Minggu, 25 Desember 2024, pukul 10.00 WIB di Gereja. Mari kita rayakan kelahiran Yesus Kristus dengan sukacita dan kebersamaan. Setelah ibadah, akan diadakan acara ramah tamah di aula gereja.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 2,
                    Judul = "Rapat Anggota Jemaat Tahunan",
                    Isi = "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 6, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 3,
                    Judul = "Retret Pemuda Gereja",
                    Isi = "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 7, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 4,
                    Judul = "Penggalangan Dana untuk Renovasi Gereja",
                    Isi = "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 8, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 5,
                    Judul = "Pelayanan Sosial Natal",
                    Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 9, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 6,
                    Judul = "Pelayanan Sosial Natal",
                    Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 10, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                },
                new Pengumuman
                {
                    Id = 7,
                    Judul = "Pelayanan Sosial Natal",
                    Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 11, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    Foto = new Foto{ Id = 1 },
                    Pembuat = new AppUser {Id = 1}
                }
            };
        }

        private List<Pengumuman> GetDataPengumuman(int year = 2024, int month = 1, string s = "Tidak Kosong", int totalPages = 1)
        {
            return GetDataPengumuman()
                .Concat(Enumerable.Range(0, 6 * totalPages).Select(i => new Pengumuman
                {
                    Id = i,
                    Judul = $"Aasas{s}asaa",
                    Isi = $"aaasemwdcwvws{s}",
                    TanggalDiBuat = new DateTime(year, month, 12)
                })).ToList();
        }
    }
}
