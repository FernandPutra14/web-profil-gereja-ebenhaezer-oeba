using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.WartaJemaat;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class WartaJemaatControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<WartaJemaatController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;

    private readonly WartaJemaatController _wartaJemaatController;

    public WartaJemaatControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<WartaJemaatController>>();
        _notificationService = new Mock<IToastrNotificationService>();

        //SUT
        _wartaJemaatController = new WartaJemaatController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarWarta = Array.Empty<WartaJemaat>();

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Index();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull().And.BeOfType<List<WartaJemaat>>();
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _wartaJemaatController.Tambah();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull().And.BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM();
        _wartaJemaatController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenTanggalWartaNotUnique()
    {
        //Arrange
        var tambahVM = new TambahVM { TanggalWarta = new DateOnly(1, 1, 1)};
        var daftarWarta = new WartaJemaat[] { new() { TanggalWarta = tambahVM.TanggalWarta } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        _wartaJemaatController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallAdd()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };
        var dbSetMock = new Mock<DbSet<WartaJemaat>>();

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(Array.Empty<WartaJemaat>(), dbSetMock);

        //Act
        await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(x => x.Add(It.IsAny<WartaJemaat>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(Array.Empty<WartaJemaat>());

        //Act
        await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(Array.Empty<WartaJemaat>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        _wartaJemaatController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(WartaJemaatController.Index);
        var tambahVM = new TambahVM
        {
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(Array.Empty<WartaJemaat>());

        //Act
        var result = await _wartaJemaatController.Tambah(tambahVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFound_WhenWartaNotFound()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenWartaFound()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id, DocumentLink = new Uri("http://google.drive.com") } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Edit(id);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().Which.Id
              .Should().Be(id);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM();
        _wartaJemaatController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _wartaJemaatController.Edit(editVM);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndAddErrorNotification_WhenWartaNotFound()
    {
        //Arrange
        var actionName = nameof(WartaJemaatController.Index);
        var editVM = new EditVM { Id = 1 };
        var daftarWarta = new WartaJemaat[] { new() { Id = editVM.Id + 1 } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Edit(editVM);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(t => t.Type == ToastrNotificationType.Error)),
                    Times.Once());

        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenTanggalWartaNotUnique()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, TanggalWarta = new DateOnly(1, 1, 1)};
        var daftarWarta = new WartaJemaat[] 
        { 
            new() { Id = editVM.Id }, 
            new() { Id = editVM.Id + 1, TanggalWarta = editVM.TanggalWarta } 
        };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Edit(editVM);

        //Assert
        _wartaJemaatController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var editVM = new EditVM 
        { 
            Id = 1, 
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };
        var daftarWarta = new WartaJemaat[] { new() { Id = editVM.Id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        await _wartaJemaatController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var editVM = new EditVM
        {
            Id = 1,
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };
        var daftarWarta = new WartaJemaat[] { new() { Id = editVM.Id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _wartaJemaatController.Edit(editVM);

        //Assert
        _wartaJemaatController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(WartaJemaatController.Index);
        var editVM = new EditVM
        {
            Id = 1,
            TanggalWarta = new DateOnly(1, 1, 1),
            DocumentLink = "https://google.drive.com"
        };
        var daftarWarta = new WartaJemaat[] { new() { Id = editVM.Id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Edit(editVM);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnNotFound_WhenWartaNotFound()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Hapus(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Hapus_Should_CallRemove()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id } };
        var dbSetMock = new Mock<DbSet<WartaJemaat>>();

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta, dbSetMock);

        //Act
        await _wartaJemaatController.Hapus(id);

        //Assert
        dbSetMock.Verify(x => x.Remove(It.Is<WartaJemaat>(x => x.Id == id)), Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_CallSaveChangesAsync()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        await _wartaJemaatController.Hapus(id);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_AddErrorNotification_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        await _wartaJemaatController.Hapus(id);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(t => t.Type == ToastrNotificationType.Error)),
                    Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToActionIndex_WhenWartaFound()
    {
        //Arrange
        var actionName = nameof(WartaJemaatController.Index);
        var id = 1;
        var daftarWarta = new WartaJemaat[] { new() { Id = id } };

        _appDbContext.Setup(x => x.WartaJemaatTable).ReturnsDbSet(daftarWarta);

        //Act
        var result = await _wartaJemaatController.Hapus(id);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }
}