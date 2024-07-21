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
    public class WartaJemaatControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;

        private readonly WartaJemaatController _wartaController;

        public static IEnumerable<object[]> BulanData { get => Enumerable.Range(1, 12).Select(x => new object[] { x }); }
        public static IEnumerable<object[]> TahunData { get => Enumerable.Range(0, 12).Select(x => new object[] { 2024 - x }); }
        public static IEnumerable<object[]> PageIndexData { get => Enumerable.Range(1, 12).Select(x => new object[] { x }); }
        public static IEnumerable<object[]> InvalidBulanData
        {
            get => Enumerable.Range(1, 6).Select(x => new object[] { 1 - x })
                .Concat(Enumerable.Range(1, 6).Select(x => new object[] { 12 + x }));
        }
        public static IEnumerable<object[]> InvalidTahunData { get => Enumerable.Range(1, 12).Select(x => new object[] { 1 - x }); }
        public static IEnumerable<object[]> NegativePageIndexData
        {
            get => Enumerable.Range(1, 12)
                .Select(x => new object[] { 1 - x });
        }

        public WartaJemaatControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();

            //SUT
            _wartaController = new WartaJemaatController(_appDbContext.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResult()
        {
            //Arrange
            var daftarWartaJemaat = new List<WartaJemaat>();
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>();
        }

        [Fact]
        public async Task Index_Should_ModelItemsHaveCountEqualTo6OrLess()
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat();
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Items.Should().HaveCountLessThanOrEqualTo(6);
        }

        [Theory]
        [MemberData(nameof(BulanData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithMonthEqualToBulan(int bulan)
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat(month: bulan);
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(bulan);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Bulan.Should().Be(bulan);
            model.Items.Should().AllSatisfy(p => p.TanggalWarta.Month.Should().Be(bulan));
        }

        [Theory]
        [MemberData(nameof(TahunData))]
        public async Task Index_Should_ModelItemsContainsPengumumanWithYearEqualToTahun(int tahun)
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat(year: tahun);
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(tahun: tahun);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Tahun.Should().Be(tahun);
            model.Items.Should().AllSatisfy(p => p.TanggalWarta.Year.Should().Be(tahun));
        }

        [Theory]
        [MemberData(nameof(PageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo(int pageIndex)
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat(totalPages: pageIndex);
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(pageIndex:pageIndex);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Items.PageIndex.Should().Be(pageIndex);
        }

        [Theory]
        [MemberData(nameof(InvalidBulanData))]
        public async Task Index_Should_ModelBulanNull_WhenBulanOutOfRange(int bulan)
        {

            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat();
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(bulan:bulan);

            //Assert 
            bulan.Should().NotBeInRange(1, 12);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Bulan.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(InvalidTahunData))]
        public async Task Index_Should_ModelTahunNull_WhenTahunNegativeOrZero(int tahun)
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat();
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(tahun:tahun);

            //Assert 
            tahun.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Tahun.Should().BeNull();
            model.Items.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(NegativePageIndexData))]
        public async Task Index_Should_ModelPageIndexEqualTo1_WhenPageIndexNegativeOfZero(int pageIndex)
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat();
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(pageIndex:pageIndex);

            //Assert 
            pageIndex.Should().BeLessThanOrEqualTo(0);
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Items.PageIndex.Should().Be(1);
            model.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Index_Should_ModelPageIndexEqualToTotalPages_WhenPageIndexGreaterThanTotalPages()
        {
            //Arrange
            var daftarWartaJemaat = GetDataWartaJemaat(totalPages:2);
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWartaJemaat);

            //Act
            var result = await _wartaController.Index(pageIndex:100);

            //Assert 
            daftarWartaJemaat.Count.Should().BeGreaterThan(6);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM<WartaJemaat>>().Subject;
            model.Items.PageIndex.Should().Be(model.Items.TotalPages);
            model.Items.Should().NotBeEmpty();
        }

        private List<WartaJemaat> GetDataWartaJemaat()
        {
            return new List<WartaJemaat>()
            {
                new WartaJemaat
                {
                    Id = 1,
                    TanggalWarta = new DateOnly(2024, 7, 7),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 7, 0, 0, 0, DateTimeKind.Unspecified),
                    Pembuat = new AppUser {Id = 1},
                },
                new WartaJemaat
                {
                    Id = 2,
                    TanggalWarta = new DateOnly(2024, 7, 21),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 14, 0, 0, 0, DateTimeKind.Unspecified),
                    Pembuat = new AppUser {Id = 1},
                },
                new WartaJemaat
                {
                    Id = 3,
                    TanggalWarta = new DateOnly(2024, 7, 28),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 21, 0, 0, 0, DateTimeKind.Unspecified),
                    Pembuat = new AppUser {Id = 1},
                }
            };
        }

        private List<WartaJemaat> GetDataWartaJemaat(int year = 2024, int month = 1, int totalPages = 1)
        {
            return GetDataWartaJemaat()
                .Concat(Enumerable.Range(0, 6 * totalPages).Select(i => new WartaJemaat
                {
                    Id = i,
                    TanggalWarta = new DateOnly(year, month, 12),
                    TanggalDiBuat = new DateTime(year, month, 12)
                })).ToList();
        }
    }
}
