using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class IbadahControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<IbadahController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;
    private readonly Mock<IBeebeleApiService> _beebeleApiService;

    private readonly IbadahController _ibadahController;

    public IbadahControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<IbadahController>>();
        _notificationService = new Mock<IToastrNotificationService>();
        _beebeleApiService = new Mock<IBeebeleApiService>();

        //SUT
        _ibadahController = new IbadahController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object,
            _beebeleApiService.Object);
    }

    [Fact]
    public async Task Index_Should_Return_ViewResult()
    {
        //Arrange
        var daftarIbadah = GetDataIbadah();
        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

        //Act
        var result = await _ibadahController.Index();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<List<Ibadah>>().Subject;

        model.Should().Contain(daftarIbadah);
    }

    [Fact]
    public async Task Index_Should_AddWarningNotification_WhenKategoriOrPendetaIsNull()
    {
        //Arrange
        var daftarIbadah = GetDataIbadah().Concat(GetDataIbadahWithPendetaAndKategoriNull());
        var numberOfIbadahWithKategoriNull = daftarIbadah.Count(i => i.KategoriIbadah == null);
        var numberOfIbadahWithPendetaNull = daftarIbadah.Count(i => i.Pendeta == null);
        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

        //Act
        await _ibadahController.Index();

        //Assert
        _notificationService
            .Verify(
                x => x.AddNotification(It.Is<ToastrNotification>(t => t.Type == ToastrNotificationType.Warning)),
                Times.Exactly(numberOfIbadahWithPendetaNull + numberOfIbadahWithKategoriNull));
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _ibadahController.Tambah();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<TambahVM>();
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };
        _ibadahController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _ibadahController.Tambah(tambahVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNasPembimbingNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };
        _beebeleApiService.Setup(x => x.IsValid(tambahVM.NasPembimbing)).ReturnsAsync(false);

        //Act
        var result = await _ibadahController.Tambah(tambahVM);

        //Assert
        _ibadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenRenunganNotValid()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };

        var sequence = new MockSequence();
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.Renungan)).ReturnsAsync(false);

        //Act
        var result = await _ibadahController.Tambah(tambahVM);

        //Assert
        _beebeleApiService.VerifyAll();
        _ibadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };

        var sequence = new MockSequence();
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.Renungan)).ReturnsAsync(true);

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(Array.Empty<Ibadah>());
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(Array.Empty<KategoriIbadah>());
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(Array.Empty<Pendeta>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _ibadahController.Tambah(tambahVM);

        //Assert
        _beebeleApiService.VerifyAll();
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        _ibadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_CallIbadahTableAddAndSaveChangesAsync()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };
        var kategoriIbadah = new KategoriIbadah { Id = tambahVM.IdKategoriIbadah };
        var pendeta = new Pendeta { Id = tambahVM.IdPendeta };

        var dbSetMock = new Mock<DbSet<Ibadah>>();

        var sequence = new MockSequence();
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.Renungan)).ReturnsAsync(true);

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(Array.Empty<Ibadah>(), dbSetMock);
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] { kategoriIbadah });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new Pendeta[] { pendeta });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<int>());

        //Act
        await _ibadahController.Tambah(tambahVM);

        //Assert
        dbSetMock.Verify(
            x => x.Add(It.Is<Ibadah>(x => x.Judul == tambahVM.Judul &&
                                          x.Deskripsi == tambahVM.Deskripsi &&
                                          x.TanggalIbadah == tambahVM.TanggalIbadah &&
                                          x.Tempat == tambahVM.Tempat &&
                                          x.NasPembimbing == tambahVM.NasPembimbing &&
                                          x.Renungan == tambahVM.Renungan &&
                                          x.Pendeta == pendeta &&
                                          x.KategoriIbadah == kategoriIbadah)), 
            Times.Once());
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionIndexResult_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(IbadahController.Index);
        var tambahVM = new TambahVM
        {
            Judul = "Judul",
            Deskripsi = "Deskripsi",
            TanggalIbadah = new DateTime(),
            Tempat = "Tempat",
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdPendeta = 1,
            IdKategoriIbadah = 1
        };
        var kategoriIbadah = new KategoriIbadah { Id = tambahVM.IdKategoriIbadah };
        var pendeta = new Pendeta { Id = tambahVM.IdPendeta };

        var dbSetMock = new Mock<DbSet<Ibadah>>();

        var sequence = new MockSequence();
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(sequence).Setup(x => x.IsValid(tambahVM.Renungan)).ReturnsAsync(true);

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(Array.Empty<Ibadah>(), dbSetMock);
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new KategoriIbadah[] { kategoriIbadah });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new Pendeta[] { pendeta });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<int>());

        //Act
        var result = await _ibadahController.Tambah(tambahVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFoundResult_WhenIbadahNotFound()
    {
        //Arrange
        var id = 1;
        var daftarIbadah = new List<Ibadah> { new Ibadah { Id = 2 } };
        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

        //Act
        var result = await _ibadahController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenIbadahFound()
    {
        //Arrange
        var id = 1;
        var daftarIbadah = new List<Ibadah> { new Ibadah { Id = id } };
        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(daftarIbadah);

        //Act
        var result = await _ibadahController.Edit(id);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Id.Should().Be(id);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        _ibadahController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _ibadahController.Edit(It.IsAny<EditVM>());

        //Assert
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNasPembimbingNotValid()
    {
        //Arrange
        var editVM = new EditVM { NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()) };
        _beebeleApiService.Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(false);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _beebeleApiService.Verify(x => x.IsValid(editVM.NasPembimbing), Times.Once());
        _ibadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenRenunganNotValid()
    {
        //Arrange
        var editVM = new EditVM 
        { 
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>())
        };

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(false);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _beebeleApiService.VerifyAll();
        _ibadahController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToIndexResultAndAddErrorNotification_WhenIbadahNotFound()
    {
        //Arrange
        var actionName = nameof(IbadahController.Index);

        var editVM = new EditVM
        {
            Id = 1,
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>())
        };

        var ibadah = new Ibadah { Id = 2 };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _beebeleApiService.VerifyAll();
        _appDbContext.VerifyGet(x => x.IbadahTable, Times.AtLeastOnce());
        _notificationService
            .Verify(
            x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Error)),
            Times.Once());

        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenKategoriNotFound()
    {
        //Arrange
        var editVM = new EditVM
        {
            Id = 1,
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdKategoriIbadah = 1,
        };

        var ibadah = new Ibadah { Id = editVM.Id };
        var kategori = new KategoriIbadah { Id = 2 };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new List<KategoriIbadah> { kategori });

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _appDbContext.VerifyGet(x => x.KategoriIbadahTable, Times.Once());
        _ibadahController.ModelState.IsValid.Should().BeFalse();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenPendetaNotFound()
    {
        //Arrange
        var editVM = new EditVM
        {
            Id = 1,
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdKategoriIbadah = 1,
            IdPendeta = 1
        };

        var ibadah = new Ibadah { Id = editVM.Id };
        var kategori = new KategoriIbadah { Id = editVM.IdKategoriIbadah.Value };
        var pendeta = new Pendeta { Id = 2 };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new List<KategoriIbadah> { kategori });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new List<Pendeta> { pendeta });

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _appDbContext.VerifyGet(x => x.PendetaTable, Times.Once());
        _ibadahController.ModelState.IsValid.Should().BeFalse();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_CallSaveChangesAsync()
    {
        //Arrange
        var editVM = new EditVM
        {
            Id = 1,
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdKategoriIbadah = 1,
            IdPendeta = 1
        };

        var ibadah = new Ibadah { Id = editVM.Id };
        var kategori = new KategoriIbadah { Id = editVM.IdKategoriIbadah.Value };
        var pendeta = new Pendeta { Id = editVM.IdPendeta.Value };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new List<KategoriIbadah> { kategori });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new List<Pendeta> { pendeta });

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        await _ibadahController.Edit(editVM);

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
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdKategoriIbadah = 1,
            IdPendeta = 1
        };

        var ibadah = new Ibadah { Id = editVM.Id };
        var kategori = new KategoriIbadah { Id = editVM.IdKategoriIbadah.Value };
        var pendeta = new Pendeta { Id = editVM.IdPendeta.Value };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new List<KategoriIbadah> { kategori });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new List<Pendeta> { pendeta });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        _ibadahController.ModelState.IsValid.Should().BeFalse();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().Be(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndex_WhenEditSuccess()
    {
        //Arrange
        var actionName = nameof(IbadahController.Index);
        var editVM = new EditVM
        {
            Id = 1,
            NasPembimbing = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            Renungan = new AyatAlkitab(Kitab.Kejadian, 1, Array.Empty<int>()),
            IdKategoriIbadah = 1,
            IdPendeta = 1
        };

        var ibadah = new Ibadah { Id = editVM.Id };
        var kategori = new KategoriIbadah { Id = editVM.IdKategoriIbadah.Value };
        var pendeta = new Pendeta { Id = editVM.IdPendeta.Value };

        _appDbContext.Setup(x => x.IbadahTable).ReturnsDbSet(new List<Ibadah> { ibadah });
        _appDbContext.Setup(x => x.KategoriIbadahTable).ReturnsDbSet(new List<KategoriIbadah> { kategori });
        _appDbContext.Setup(x => x.PendetaTable).ReturnsDbSet(new List<Pendeta> { pendeta });
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockSequence = new MockSequence();
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.NasPembimbing)).ReturnsAsync(true);
        _beebeleApiService.InSequence(mockSequence)
            .Setup(x => x.IsValid(editVM.Renungan!)).ReturnsAsync(true);

        //Act
        var result = await _ibadahController.Edit(editVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    private List<Ibadah> GetDataIbadah()
    {
        return new List<Ibadah>
            {
                new Ibadah
                {
                    Id = 1,
                    Judul = "Kebaktian Pagi Pertama",
                    Deskripsi = "Kebaktian hari minggu pagi pertama",
                    NasPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 15 }),
                    Renungan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 15 }),
                    TanggalIbadah = new DateTime(2024,  12, 23, 6, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 1},
                    Pendeta = new Pendeta { Id = 1 },
                },
                new Ibadah
                {
                    Id = 2,
                    Judul = "Kebaktian Pagi Kedua",
                    Deskripsi = "Kebaktian hari minggu pagi kedua",
                    NasPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 15 }),
                    Renungan = new AyatAlkitab(Kitab.Markus, 3, 4, 15 ),
                    TanggalIbadah = new DateTime(2023, 11, 23, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 1},
                    Pendeta = new Pendeta { Id = 3 },
                },
                new Ibadah
                {
                    Id = 3,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2022, 10, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 4,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2021, 9, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 5,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2020, 8, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 6,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2019, 7, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 7,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2018, 6, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 8,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2017, 5, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 9,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2016, 4, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 10,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2015, 3, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 11,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2014, 2, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                },
                new Ibadah
                {
                    Id = 12,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = new AyatAlkitab(Kitab.Matius, 3, new int[] { 16 }),
                    TanggalIbadah = new DateTime(2013, 1, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadah = new KategoriIbadah { Id = 2},
                    Pendeta = new Pendeta { Id = 2 }
                }
            };
    }

    private List<Ibadah> GetDataIbadahWithPendetaAndKategoriNull()
    {
        return GetDataIbadah()
            .Select(i =>
            {
                i.KategoriIbadah = null;
                i.Pendeta = null;
                return i;
            }).ToList();
    }
}
