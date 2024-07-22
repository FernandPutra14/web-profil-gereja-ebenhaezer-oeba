using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Home;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard
{
    public class HomeControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;

        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();

            //SUT
            _homeController = new HomeController(_appDbContext.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResult()
        {
            //Arrange
            _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new List<Rayon>());

            //Act
            var result = await _homeController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM>();
        }

        [Fact]
        public async Task Index_Should_ModelHaveTotalJemaatByAge()
        {
            //Arrange
            var daftarRayon = GetDataRayon();
            _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(daftarRayon);

            //Act
            var result = await _homeController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<IndexVM>().Subject;
            model.TotalAnak.Should().Be(daftarRayon.Sum(r => r.JumlahAnak));
            model.TotalRemaja.Should().Be(daftarRayon.Sum(r => r.JumlahRemaja));
            model.TotalPemuda.Should().Be(daftarRayon.Sum(r => r.JumlahPemuda));
            model.TotalDewasa.Should().Be(daftarRayon.Sum(r => r.JumlahDewasa));
            model.TotalLansia.Should().Be(daftarRayon.Sum(r => r.JumlahLansia));
        }

        private List<Rayon> GetDataRayon()
        {
            return new List<Rayon>
            {
                new Rayon
                {
                    Id = 1,
                    Nama = "Rayon I",
                    FotoKetua = new Foto(),
                    KetuaRayon = "Ketua Rayon I",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new Rayon
                {
                    Id = 2,
                    Nama = "Rayon II",
                    FotoKetua = new Foto(),
                    KetuaRayon = "Ketua Rayon II",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new Rayon
                {
                    Id = 3,
                    Nama = "Rayon III",
                    FotoKetua = new Foto(),
                    KetuaRayon = "Ketua Rayon III",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                }
            };
        }
    }
}
