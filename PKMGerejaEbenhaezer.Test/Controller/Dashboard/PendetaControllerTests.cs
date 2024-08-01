using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
              .Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaNotUnique()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };
        var daftarPendeta = new Pendeta[] { new() { Nama = tambahVM.Nama } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Tambah(tambahVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenFotoNotFound()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto + 1 } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>());
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pendetaController.Tambah(tambahVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallAdd()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };
        var dbSetMock = new Mock<DbSet<Pendeta>>();

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>(), dbSetMock);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });

        //Act
        await _pendetaController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(x => x.Add(It.IsAny<Pendeta>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>());
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });

        //Act
        await _pendetaController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>());
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _pendetaController.Tambah(tambahVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>());
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });

        //Act
        var result = await _pendetaController.Tambah(tambahVM);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_NotFound_WhenPendetaNotFound()
    {
        //Arrange
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ViewResult_WhenPendetaFound()
    {
        //Arrange
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Edit(id);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
            .Should().BeOfType<EditVM>().Which.Id
            .Should().Be(id);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM();
        _pendetaController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaNotUnique()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama" };
        var daftarPendeta = new Pendeta[] { new() { Id = editVM.Id + 1, Nama = editVM.Nama } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenFotoNotFound()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, IdFoto = 1 };
        var daftarPendeta = new Pendeta[] { new() { Id = editVM.Id } };
        var daftarFoto = new Foto[] { new() { Id = editVM.IdFoto.Value + 1 } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndAddErrorNotification_WhenPendetaNotFound()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var editVM = new EditVM { Id = 1, Nama = "Nama", IdFoto = 1 };
        var daftarPendeta = Array.Empty<Pendeta>();
        var daftarFoto = new Foto[] { new() { Id = editVM.IdFoto.Value } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Error)),
                    Times.Once());

        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task EditPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, IdFoto = 1 };
        var daftarPendeta = new Pendeta[] { new() { Id = editVM.Id } };
        var daftarFoto = new Foto[] { new() { Id = editVM.IdFoto.Value } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        await _pendetaController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, IdFoto = 1 };
        var daftarPendeta = new Pendeta[] { new() { Id = editVM.Id } };
        var daftarFoto = new Foto[] { new() { Id = editVM.IdFoto.Value } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        _pendetaController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var editVM = new EditVM { Id = 1, IdFoto = 1 };
        var daftarPendeta = new Pendeta[] { new() { Id = editVM.Id } };
        var daftarFoto = new Foto[] { new() { Id = editVM.IdFoto.Value } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pendetaController.Edit(editVM);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnNotFound_WhenPendetaNotFound()
    {
        //Arrange
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Hapus(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Hapus_Should_CallRemove()
    {
        //Arrange
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id } };
        var dbSetMock = new Mock<DbSet<Pendeta>>();

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta, dbSetMock);

        //Act
        await _pendetaController.Hapus(id);

        //Assert
        dbSetMock.Verify(x => x.Remove(It.Is<Pendeta>(x => x.Id == id)), Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_CallSaveChangesAsync()
    {
        //Arrange
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        await _pendetaController.Hapus(id);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToActionIndexAndAddErrorNotification_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _pendetaController.Hapus(id);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Error)),
                    Times.Once());

        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToActionIndexAndAddSuccessNotification_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var id = 1;
        var daftarPendeta = new Pendeta[] { new() { Id = id } };

        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(daftarPendeta);

        //Act
        var result = await _pendetaController.Hapus(id);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Success)),
                    Times.Once());

        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }
}