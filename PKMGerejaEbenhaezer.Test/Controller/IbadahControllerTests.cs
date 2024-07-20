using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using PKMGerejaEbenhaezer.Web.Controllers;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;

namespace PKMGerejaEbenhaezer.Test.Controller
{
    public class IbadahControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;
        private readonly Mock<IBeebeleApiService> _beebeleApiService;

        private readonly IbadahController _ibadahController;

        public IbadahControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();
            _beebeleApiService = new Mock<IBeebeleApiService>();

            //SUT
            _ibadahController = new IbadahController(_appDbContext.Object, _beebeleApiService.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResult()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();

            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(It.IsAny<int?>(), It.IsAny<int?>(), null);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>();
        }

        [Fact]
        public async Task Index_Should_ModelItemsNotContainIbadahWithPendetaOrKategoriNull()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            var daftarIbadahWithNull = GetDataIbadahWithPendetaOrKategoriNull();

            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah.Concat(daftarIbadahWithNull));

            //Act
            var result = await _ibadahController.Index(null, null, null);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().AllSatisfy(i => i.KategoriIbadah.Should().NotBeNull());
            model.Items.Should().AllSatisfy(i => i.Pendeta.Should().NotBeNull());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public async Task Index_Should_ModelOnlyContainsIbadahWithMonthEqualToInputBulan_WhenBulanNotNull(
            int bulan)
        {
            //Arrange
            var daftarIbadah = GetDataIbadahWithMonth(bulan);

            _appDbContext.Setup(a => a.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(bulan, null, null);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().AllSatisfy(i => i.TanggalIbadah.Month.Should().Be(bulan));
        }

        [Theory]
        [InlineData(13)]
        [InlineData(-13)]
        [InlineData(1000000)]
        [InlineData(-1000000)]
        [InlineData(0)]
        public async Task Index_Should_ModelBulanIsNull_WhenBulanOutOfRange(int bulan)
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();

            _appDbContext.Setup(a => a.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(bulan, null, null);

            //Assert
            bulan.Should().NotBeInRange(1, 12);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Bulan.Should().BeNull();
        }

        [Theory]
        [InlineData(2024)]
        [InlineData(2023)]
        [InlineData(2022)]
        [InlineData(2021)]
        [InlineData(2020)]
        [InlineData(2019)]
        [InlineData(2018)]
        [InlineData(2017)]
        [InlineData(2016)]
        [InlineData(2015)]
        [InlineData(2014)]
        [InlineData(2013)]
        public async Task Index_Should_ModelOnlyContainsIbadahWithYearEqualToInputTahun_WhenTahunIsNotNull(
            int tahun)
        {
            //Arrange
            var daftarIbadah = GetDataIbadahWithYear(tahun);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(null, tahun, null);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().AllSatisfy(i => i.TanggalIbadah.Year.Should().Be(tahun));
        }

        [Theory]
        [InlineData("Aadsad")]
        [InlineData("Judul")]
        [InlineData("String")]
        public async Task Index_Should_ModelOnlyContainsIbadahWithJudulContainInputString_WhenTahunIsNotNull(
            string s)
        {
            //Arrange
            var daftarIbadah = GetDataIbadahContainString(s);
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(null, null, s);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().AllSatisfy(i => i.Judul.ToLower().Should().Contain(s.ToLower()));
        }

        [Fact]
        public async Task Index_Should_ModelItemsHaveCountEqualTo6OrLess()
        {
            //Arrange
            var daftarIbadah = GetDataIbadah();
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

            //Act
            var result = await _ibadahController.Index(null, null, null);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<Ibadah>>().Subject;
            model.Items.Should().HaveCountLessThanOrEqualTo(6);
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

        private List<Ibadah> GetDataIbadahWithMonth(int month)
        {
            return GetDataIbadah()
                .Concat(Enumerable.Range(0, 6).Select(i => new Ibadah
                {
                    Id = i,
                    TanggalIbadah = new DateTime(2024, month, 1),
                    KategoriIbadah = new KategoriIbadah(),
                    Pendeta = new Pendeta()
                })).ToList();
        }

        private List<Ibadah> GetDataIbadahWithYear(int year)
        {
            return GetDataIbadah()
                .Concat(Enumerable.Range(0, 6).Select(i => new Ibadah
                {
                    Id = i,
                    TanggalIbadah = new DateTime(year, 1, 1),
                    KategoriIbadah = new KategoriIbadah(),
                    Pendeta = new Pendeta()
                })).ToList();
        }

        private List<Ibadah> GetDataIbadahContainString(string s)
        {
            return GetDataIbadah()
                .Concat(Enumerable.Range(0, 6).Select(i => new Ibadah
                {
                    Id = i,
                    Judul = s,
                    Deskripsi = s,
                    KategoriIbadah = new KategoriIbadah(),
                    Pendeta = new Pendeta()
                })).ToList();
        }

        private List<Ibadah> GetDataIbadahWithPendetaOrKategoriNull()
        {
            return new List<Ibadah>
            {
                new Ibadah
                {
                    Id = 4,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(1, 1, 1, 1, 1, 1),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = null,
                    Pendeta = null
                },
                new Ibadah
                {
                    Id = 5,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(1, 1, 1, 1, 1, 1),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = null,
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 6,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(1, 1, 1, 1, 1, 1),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = null
                }
            };
        }
    }
}
