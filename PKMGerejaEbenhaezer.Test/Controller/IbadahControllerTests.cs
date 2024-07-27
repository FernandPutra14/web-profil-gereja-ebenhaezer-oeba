using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using PKMGerejaEbenhaezer.Web.Controllers;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Models.IbadahModels;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;
using System.Linq;

namespace PKMGerejaEbenhaezer.UnitTest.Controller
{
    public class IbadahControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;

        private readonly IbadahController _ibadahController;

        public static IEnumerable<object[]> BulanData { get => Enumerable.Range(1, 12).Select(x => new object[] { x }); }
        public static IEnumerable<object[]> TahunData
        {
            get => Enumerable.Range(0, 12).Select(x => new object[] { 2024 - x });
        }
        public static IEnumerable<object[]> SearchStringData
        {
            get => Enumerable.Range(0, 12).Select(x => new object[] { x.ToString() });
        }
        public static IEnumerable<object[]> PageIndexData
        {
            get => Enumerable.Range(1, 12).Select(x => new object[] { x });
        }
        public static IEnumerable<object[]> InvalidBulanData
        {
            get => Enumerable.Range(1, 6).Select(x => new object[] { 1 - x })
                .Concat(Enumerable.Range(1, 6).Select(x => new object[] { 12 + x }));
        }
        public static IEnumerable<object[]> InvalidTahunData
        {
            get => Enumerable.Range(1, 12).Select(x => new object[] { 1 - x });
        }
        public static IEnumerable<object[]> NegativePageIndexData
        {
            get => Enumerable.Range(1, 12)
                .Select(x => new object[] { 1 - x });
        }

        public IbadahControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();

            //SUT
            _ibadahController = new IbadahController(_appDbContext.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResult()
        {
            //Arrange
            var daftarIbadah = new List<Ibadah>();

            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>();
        }

        [Fact]
        public async Task Index_Should_ModelItemsNotContainIbadahWithPendetaOrKategoriNull()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            var daftarIbadahWithNull = GetDataIbadahWithPendetaAndKategoriNull();

            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah.Concat(daftarIbadahWithNull));

            //Act
            var result = await _ibadahController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().AllSatisfy(i => i.KategoriIbadah.Should().NotBeNull());
            model.Items.Should().AllSatisfy(i => i.Pendeta.Should().NotBeNull());
        }

        [Fact]
        public async Task Index_Should_ModelItemsHaveCountEqualTo6OrLess()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().HaveCountLessThanOrEqualTo(6);
        }

        [Theory]
        [MemberData(nameof(BulanData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithMonthEqualToBulan(int bulan)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah(month: bulan);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(bulan: bulan);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Bulan.Should().Be(bulan);
            model.Items.Should().AllSatisfy(i => i.TanggalIbadah.Month.Should().Be(bulan));
        }

        [Theory]
        [MemberData(nameof(TahunData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithYearEqualToTahun(int tahun)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah(year: tahun);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(tahun: tahun);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Tahun.Should().Be(tahun);
            model.Items.Should().AllSatisfy(i => i.TanggalIbadah.Year.Should().Be(tahun));
        }

        [Theory]
        [MemberData(nameof(SearchStringData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithJudulOrIsiContainSearchString(
            string searchString)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah(s: searchString);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(searchString: searchString);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.SearchString.Should().Be(searchString);
            model.Items.Should()
                .OnlyContain(i => i.Judul.ToLower().Contains(searchString.ToLower())
                                  || i.Deskripsi.ToLower().Contains(searchString.ToLower()));
        }

        [Theory]
        [MemberData(nameof(PageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo(int pageIndex)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah(totalPages: pageIndex);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(pageIndex: pageIndex);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.PageIndex.Should().Be(pageIndex);
        }

        [Theory]
        [MemberData(nameof(InvalidBulanData))]
        public async Task Index_Should_ModelBulanNull_WhenBulanOutOfRange(int bulan)
        {
            //Arrange
            var _daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(_daftarIbadah);

            //Act
            var result = await _ibadahController.Index(bulan: bulan);

            //Assert 
            bulan.Should().NotBeInRange(1, 12);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Bulan.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(InvalidTahunData))]
        public async Task Index_Should_ModelTahunNull_WhenTahunNegativeOrZero(int tahun)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(tahun: tahun);

            //Assert 
            tahun.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Tahun.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(NegativePageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo1_WhenPageIndexNegativeOfZero(int pageIndex)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(pageIndex: pageIndex);

            //Assert 
            pageIndex.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.PageIndex.Should().Be(1);
            model.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Index_Should_ModelPageIndexEqualToTotalPages_WhenPageIndexGreaterThanTotalPages()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(pageIndex: 100);

            //Assert 
            daftarIbadah.Count.Should().BeGreaterThan(6);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.PageIndex.Should().Be(model.Items.TotalPages);
            model.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Detail_Should_ReturnNotFoundResult_WhenIbadahWithIdNotFound()
        {
            //Arrange
            var id = 1;
            var daftarIbadah = new List<Ibadah> { new Ibadah { Id = 2 } };
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Detail(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Detail_Should_ReturnNotFoundResult_WhenKategoriOrPendetaIsNull()
        {
            //Arrange
            var id = 1;
            var daftarIbadah = new List<Ibadah> { new Ibadah { Id = id, Pendeta = null, KategoriIbadah = null } };
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Detail(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Detail_Should_ReturnViewResult_WhenSuccess()
        {
            //Arrange
            var id = 1;
            var ibadah = new Ibadah
            {
                Id = id,
                Pendeta = new Pendeta(),
                KategoriIbadah = new KategoriIbadah()
            };
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });

            //Act
            var result = await _ibadahController.Detail(id);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<Ibadah>().Subject;
            model.Should().BeEquivalentTo(ibadah);
        }

        private List<Ibadah> GetDataIbadah()
        {
            return new List<Ibadah>
            {
                new Ibadah
                {
                    Id = 1,
                    Judul = "Kebaktian Pagi Pertama",
                    Deskripsi = "Kebaktian hari minggu pagi pertama",
                    NasPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 15 }),
                    Renungan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 15 }),
                    TanggalIbadah = new DateTime(2024,  12, 23, 6, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 1},
                    Pendeta = new Pendeta { Id = 1 },
                },
                new Ibadah
                {
                    Id = 2,
                    Judul = "Kebaktian Pagi Kedua",
                    Deskripsi = "Kebaktian hari minggu pagi kedua",
                    NasPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 15 }),
                    Renungan = new AyatAlkitab(Kitab.Markus, 3, 4, 15 ),
                    TanggalIbadah = new DateTime(2023, 11, 23, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 1},
                    Pendeta = new Pendeta { Id = 3 },
                },
                new Ibadah
                {
                    Id = 3,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2022, 10, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 4,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2021, 9, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 5,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2020, 8, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 6,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2019, 7, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 7,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2018, 6, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 8,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2017, 5, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 9,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2016, 4, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 10,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2015, 3, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 11,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2014, 2, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 12,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2013, 1, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                }
            };
        }

        private List<Ibadah> GetDataIbadah(
            int year = 2024, int month = 1, string s = "Tidak Kosong", int totalPages = 1)
        {
            return GetDataIbadah()
                .Concat(Enumerable.Range(0, 6 * totalPages).Select(i => new Ibadah
                {
                    Id = i,
                    Judul = $"Aasas{s}asaa",
                    Deskripsi = $"aaasemwdcwvws{s}",
                    TanggalIbadah = new DateTime(year, month, 12),
                    Pendeta = new Pendeta(),
                    KategoriIbadah = new KategoriIbadah(),
                })).ToList();
        }

        private List<Ibadah> GetDataIbadahWithPendetaAndKategoriNull()
        {
            return GetDataIbadah()
                .Select(i =>
                {
                    i.KategoriIbadah = null;
                    i.Pendeta = null;
                    return i;
                }).ToList();
        }
    }
}
