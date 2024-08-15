using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using NuGet.Versioning;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Controllers;

namespace PKMGerejaEbenhaezer.UnitTest.Controller
{
    public class FotoControllerTests
    {
        private readonly Mock<IAppDbContext> _appDbContext;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly Mock<ILogger<FotoController>> _logger;

        private readonly FotoController _fotoController;

        public FotoControllerTests()
        {
            //Depedencies
            _appDbContext = new Mock<IAppDbContext>();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _logger = new Mock<ILogger<FotoController>>();

            //SUT
            _fotoController = new FotoController(
                _appDbContext.Object,
                _webHostEnvironment.Object,
                _logger.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnNotFoundResult_WhenFotoWithIdNotFound()
        {
            //Arrange
            var id = 1;
            _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new List<Foto>());

            //Act
            var result = await _fotoController.Index(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Index_Should_ReturnNotFoundResult_WhenPathFotoNotExist()
        {
            //Arrange
            var id = 1;
            var path = "D:/sembarang/sembarangFoto.jpg";
            var foto = new Foto { Id = id, PathFoto = path };
            _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new List<Foto> { foto });

            //Act
            var result = await _fotoController.Index(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
