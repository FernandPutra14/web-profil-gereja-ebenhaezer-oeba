using FluentAssertions;
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
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.KategoriIbadah;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class KategoriIbadahControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<KategoriIbadahController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;

    private readonly KategoriIbadahController _kategoriIbadahController;

    public KategoriIbadahControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<KategoriIbadahController>>();
        _notificationService = new Mock<IToastrNotificationService>();

        //SUT
        _kategoriIbadahController = new KategoriIbadahController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarKategori = new List<KategoriIbadah>();
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Index();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<List<KategoriIbadah>>();
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _kategoriIbadahController.Tambah();

        //Assert
        var viewResult = result.Should().NotBeNull().And.BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull().And.BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM();
        _kategoriIbadahController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().And.Be(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaNotUnique()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };
        var daftarKategori = new KategoriIbadah[] { new() { Nama = tambahVM.Nama.ToLower() } };
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        _appDbContext.VerifyGet(x => x.KategoriIbadahTable, Times.Once());
        _kategoriIbadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().And.Be(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallAdd()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };
        var dbSetMock = new Mock<DbSet<KategoriIbadah>>();

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>(), dbSetMock);

        //Act
        await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(x => x.Add(It.IsAny<KategoriIbadah>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>());

        //Act
        await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        _kategoriIbadahController.ModelState.IsValid.Should().BeFalse();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().And.Be(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToIndexResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(KategoriIbadahController.Index);
        var tambahVM = new TambahVM { Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>());

        //Act
        var result = await _kategoriIbadahController.Tambah(tambahVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFoundResult_WhenKategoriIbadahNotFound()
    {
        //Arrange
        var id = 1;

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>());

        //Act
        var result = await _kategoriIbadahController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenKategoriIbadahFound()
    {
        //Arrange
        var id = 1;

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] { new() { Id = id } });

        //Act
        var result = await _kategoriIbadahController.Edit(id);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().NotBeNull().And.BeOfType<EditVM>().Subject;
        model.Id.Should().Be(id);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM();
        _kategoriIbadahController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _kategoriIbadahController.Edit(editVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().NotBeNull().And.BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaNotUnique()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama" };
        var daftarKategori = new KategoriIbadah[] { new() { Id = editVM.Id + 1, Nama = editVM.Nama.ToLower() } };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Edit(editVM);

        //Assert
        _appDbContext.VerifyGet(x => x.KategoriIbadahTable, Times.Once());

        _kategoriIbadahController.ModelState.IsValid.Should().BeFalse();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().NotBeNull().And.BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToIndexAndAddErrorNotification_WhenKategoriNotFound()
    {
        //Arrange
        var actionName = nameof(KategoriIbadahController.Index);
        var editVM = new EditVM { Id = 1, Nama = "Nama" };
        var daftarKategori = new KategoriIbadah[] { new() { Id = editVM.Id + 1 } };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Edit(editVM);

        //Assert
        _appDbContext.VerifyGet(x => x.KategoriIbadahTable, Times.Exactly(2));
        _notificationService
            .Verify(
                x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Error)),
                Times.Once());

        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task EditPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] {new(){ Id = editVM.Id}});

        //Act
        await _kategoriIbadahController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] { new() { Id = editVM.Id } });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _kategoriIbadahController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().NotBeNull().And.BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToIndexResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(KategoriIbadahController.Index);
        var editVM = new EditVM { Id = 1, Nama = "Nama" };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] { new() { Id = editVM.Id } });

        //Act
        var result = await _kategoriIbadahController.Edit(editVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().NotBeNull().And.Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnNotFoundResult_WhenKategoriNotFound()
    {
        //Arrange
        var id = 1;
        var daftarKategori = new KategoriIbadah[] { new() { Id = id + 1 } };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Hapus(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Hapus_Should_CallSaveChangesAsync()
    {
        //Arrange
        var id = 1;
        var daftarKategori = new KategoriIbadah[] { new() { Id = id } };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        await _kategoriIbadahController.Hapus(id);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToIndexResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(KategoriIbadahController.Index);
        var id = 1;
        var daftarKategori = new KategoriIbadah[] { new() { Id = id } };

        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(daftarKategori);

        //Act
        var result = await _kategoriIbadahController.Hapus(id);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().NotBeNull().And.Be(actionName);
    }
}