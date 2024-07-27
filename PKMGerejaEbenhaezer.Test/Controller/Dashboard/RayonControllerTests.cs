using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class RayonControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<RayonController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;

    private readonly RayonController _rayonController;

    public RayonControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<RayonController>>();
        _notificationService = new Mock<IToastrNotificationService>();

        //SUT
        _rayonController = new RayonController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarRayon = new List<Rayon> { new() { Id = 1 } };
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(daftarRayon);

        //Act
        var result = await _rayonController.Index();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<List<Rayon>>().Subject;
        model.Should().BeEquivalentTo(daftarRayon);
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _rayonController.Tambah();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama" };
        _rayonController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _rayonController.Tambah(tambahVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().Subject.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenIdFotoNotFound()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };
        var foto = new Foto { Id = 2 };
        var daftarFoto = new Foto[] { foto };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(daftarFoto);

        //Act
        var result = await _rayonController.Tambah(tambahVM);

        //Assert
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().Subject.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaIsNotUnique()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Nama = tambahVM.Nama } });

        //Act
        var result = await _rayonController.Tambah(tambahVM);

        //Assert
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().Subject.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallAdd()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };
        var dbSetMock = new Mock<DbSet<Rayon>>();

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(Array.Empty<Rayon>(), dbSetMock);

        //Act
        await _rayonController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(x => x.Add(It.IsAny<Rayon>()));
    }

    [Fact]
    public async Task TambahPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(Array.Empty<Rayon>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //Act
        await _rayonController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(Array.Empty<Rayon>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _rayonController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>().Subject.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(RayonController.Index);
        var tambahVM = new TambahVM { Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = tambahVM.IdFoto } });
        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(Array.Empty<Rayon>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //Act
        var result = await _rayonController.Tambah(tambahVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFound_WhenRayonNotFound()
    {
        //Arrange
        var id = 1;

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = 2 } });

        //Act
        var result = await _rayonController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenRayonFound()
    {
        //Arrange
        var id = 1;
        var rayon = new Rayon { Id = id };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { rayon });

        //Act
        var result = await _rayonController.Edit(id);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Id.Should().Be(1);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM { Id = 1 };
        _rayonController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<EditVM>().Subject.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndAddErrorNotification_WhenRayonNotFound()
    {
        //Arrange
        var actionName = nameof(RayonController.Index);
        var editVM = new EditVM { Id = 1, IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = 2 } });
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = 2 } });

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        _notificationService
            .Verify(x => x.AddNotification(It.Is<ToastrNotification>(t => t.Type == ToastrNotificationType.Error)));

        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenIdFotoNotFound()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = editVM.Id } });
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = 2 } });

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<EditVM>().Subject.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNamaNotUnique()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable)
            .ReturnsDbSet(new Rayon[] { new() { Id = editVM.Id }, new() { Id = editVM.Id + 1, Nama = editVM.Nama } });

        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = editVM.IdFoto.Value } });

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<EditVM>().Subject.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = editVM.Id } });
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = editVM.IdFoto.Value } });

        //Act
        await _rayonController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var editVM = new EditVM { Id = 1, Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = editVM.Id } });
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = editVM.IdFoto.Value } });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        _rayonController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<EditVM>().Subject.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndex_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(RayonController.Index);
        var editVM = new EditVM { Id = 1, Nama = "Nama", IdFoto = 1 };

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = editVM.Id } });
        _appDbContext.Setup(x => x.FotoTable).ReturnsDbSet(new Foto[] { new() { Id = editVM.IdFoto.Value } });

        //Act
        var result = await _rayonController.Edit(editVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnNotFound_WhenRayonNotFound()
    {
        //Arrange
        var id = 1;

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = id + 1 } });

        //Act
        var result = await _rayonController.Hapus(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToActionResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(RayonController.Index);
        var id = 1;

        _appDbContext.Setup(x => x.RayonTable).ReturnsDbSet(new Rayon[] { new() { Id = id } });

        //Act
        var result = await _rayonController.Hapus(id);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }
}