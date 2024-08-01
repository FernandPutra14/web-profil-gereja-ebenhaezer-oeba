using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pendeta;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class PendetaControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<PendetaController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;

    private readonly PendetaController _pendetaController;

    public PendetaControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<PendetaController>>();
        _notificationService = new Mock<IToastrNotificationService>();

        //SUT
        _pendetaController = new PendetaController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarPendeta = new List<Pendeta>();
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Index();

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<List<Pendeta>>();
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _pendetaController.Tambah();

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM();
        _pendetaController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _pendetaController.Tambah(tambahVM);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<TambahVM>();
    }
}
