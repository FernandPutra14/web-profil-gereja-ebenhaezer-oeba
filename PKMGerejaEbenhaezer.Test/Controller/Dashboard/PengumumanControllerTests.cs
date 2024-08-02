using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman;
using PKMGerejaEbenhaezer.Web.Services.PDF;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class PengumumanControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<PengumumanController>> _logger;
    private readonly Mock<IPDFUploadService> _pDFUploadService;
    private readonly Mock<IToastrNotificationService> _notificationService;

    private readonly PengumumanController _pengumumanController;

    public PengumumanControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<PengumumanController>>();
        _pDFUploadService = new Mock<IPDFUploadService>();
        _notificationService = new Mock<IToastrNotificationService>();

        //SUT
        _pengumumanController = new PengumumanController(
            _appDbContext.Object,
            _logger.Object,
            _pDFUploadService.Object,
            _notificationService.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarPengumuman = Array.Empty<Pengumuman>();

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        var result = await _pengumumanController.Index();

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<List<Pengumuman>>();
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _pengumumanController.Tambah();

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM();
        _pengumumanController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenFotoNotFound()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto + 1 } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        _pengumumanController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenHaveDocumentButPDFFormFileNull()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1, HaveDocument = true, PDFFormFile = null };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        _pengumumanController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenUploadPDFFailed()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1, HaveDocument = true, PDFFormFile = Mock.Of<IFormFile>() };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(Array.Empty<Pengumuman>());

        _pDFUploadService
            .Setup(x => x.UploadAsync<TambahVM>(tambahVM.PDFFormFile))
            .ReturnsAsync(Result.Failure<string>());

        _pengumumanController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        _pengumumanController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(Array.Empty<Pengumuman>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        _pengumumanController.ModelState.IsValid.Should().BeFalse();

        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<TambahVM>().And.BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallAdd()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };
        var dbSetMock = new Mock<DbSet<Pengumuman>>();

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(Array.Empty<Pengumuman>(), dbSetMock);

        //Act
        await _pengumumanController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(x => x.Add(It.IsAny<Pengumuman>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM { IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(Array.Empty<Pengumuman>());

        //Act
        await _pengumumanController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(PengumumanController.Index);
        var tambahVM = new TambahVM { IdFoto = 1 };
        var daftarFoto = new Foto[] { new() { Id = tambahVM.IdFoto } };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);
        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(Array.Empty<Pengumuman>());

        //Act
        var result = await _pengumumanController.Tambah(tambahVM);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFoundResult_WhenPengumumanNotFound()
    {
        //Arrange
        var id = 1;
        var daftarPengumuman = new Pengumuman[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        var result = await _pengumumanController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenPengumumanFound()
    {
        //Arrange
        var id = 1;
        var daftarPengumuman = new Pengumuman[] { new() { Id = id } };

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        var result = await _pengumumanController.Edit(id);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().Which.Id
              .Should().Be(id);
    }

    [Fact]
    public async Task Edit_Should_AddWarningNotification_WhenFotoIsNull()
    {
        //Arrange
        var id = 1;
        var daftarPengumuman = new Pengumuman[] { new() { Id = id, Foto = null } };

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        await _pengumumanController.Edit(id);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Warning)),
                    Times.Once());
    }

    [Fact]
    public async Task Edit_Should_AddWarningNotification_WhenHaveDocumentButFileNotExist()
    {
        //Arrange
        var id = 1;
        var daftarPengumuman = new Pengumuman[] 
        { 
            new() { Id = id, Foto = new Foto(), HaveDocument = true, PathPDF = "" } 
        };

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        await _pengumumanController.Edit(id);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Warning)),
                    Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM { Id = 1 };
        _pengumumanController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _pengumumanController.Edit(editVM);

        //Assert
        result.Should().BeOfType<ViewResult>().Which.Model
              .Should().NotBeNull().And.BeOfType<EditVM>().And.BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndAddErrorNotification_WhenPengumumanNotFound()
    {
        //Arrange
        var actionName = nameof(PendetaController.Index);
        var editVM = new EditVM { Id = 1 };
        var daftarPengumuman = new Pengumuman[] { new() { Id = editVM.Id + 1 } };

        _appDbContext.Setup(x => x.PengumumanTable).ReturnsDbSet(daftarPengumuman);

        //Act
        var result = await _pengumumanController.Edit(editVM);

        //Assert
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName
              .Should().NotBeNull().And.Be(actionName);
    }
}