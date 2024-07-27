using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Controllers;
using PKMGerejaEbenhaezer.Web.Models.Home;

namespace PKMGerejaEbenhaezer.UnitTest.Controller
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _logger;
        private readonly Mock<IAppDbContext> _appDbContext;

        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            //Depedencies
            _logger = new Mock<ILogger<HomeController>>();
            _appDbContext = new Mock<IAppDbContext>();

            //SUT
            _homeController = new HomeController(_logger.Object, _appDbContext.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewResultWithIndexVMAsModel()
        {
            //Arrange
            _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new List<Pendeta>());
            _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new List<Rayon>());
            _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(new List<Pengumuman>());
            _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah>());
            _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(new List<WartaJemaat>());

            //Act
            var result = await _homeController.Index();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<IndexVM>();
        }

        [Fact]
        public async Task KoordinatorRayon_Should_ReturnViewResult()
        {
            //Arrange
            _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new List<Rayon>());

            //Act
            var result = await _homeController.KoordinatorRayon();

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<List<Rayon>>();
        }
    }
}
