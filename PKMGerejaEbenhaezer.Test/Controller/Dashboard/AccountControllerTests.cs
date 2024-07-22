using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using System.Linq.Expressions;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class AccountControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<AccountController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;
    private readonly Mock<IPasswordHasher<AppUser>> _passwordHasher;
    private readonly Mock<ISignInManager> _signManager;

    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<AccountController>>();
        _notificationService = new Mock<IToastrNotificationService>();
        _passwordHasher = new Mock<IPasswordHasher<AppUser>>();
        _signManager = new Mock<ISignInManager>();

        //SUT
        _accountController = new AccountController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object,
            _passwordHasher.Object,
            _signManager.Object);
    }

    [Fact]
    public async Task Index_Should_ReturnViewResult()
    {
        //Arrange
        var daftarUser = new List<AppUser>();
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Index();

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeOfType<List<AppUser>>();
    }

    [Fact]
    public void Tambah_Should_ReturnViewResult()
    {
        //Act
        var result = _accountController.Tambah();

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
            UserName = "Username", 
            Password = "password", 
            PasswordConfirmation = "password" 
        };
        _accountController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _accountController.Tambah(tambahVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnViewResultAndModelStateNotValid_WhenUserNameNotUnique()
    {
        //Arrange
        var tambahVM = new TambahVM
        {
            UserName = "Username",
            Password = "password",
            PasswordConfirmation = "password"
        };
        var appUser = new AppUser { UserName = tambahVM.UserName };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser> { appUser });

        //Act
        var result = await _accountController.Tambah(tambahVM);

        //Assert
        _accountController.ModelState.IsValid.Should().BeFalse();
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
            UserName = "Username",
            Password = "password",
            PasswordConfirmation = "password"
        };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser>());
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _accountController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        _accountController.ModelState.IsValid.Should().BeFalse();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<TambahVM>().Subject;
        model.Should().BeEquivalentTo(tambahVM);
    }

    [Fact]
    public async Task TambahPOST_Should_ReturnRedirectToActionIndexResultAndCallSaveChangesAsync_WhenSuccess()
    {
        //Arrange
        var actionName = nameof(AccountController.Index);
        var tambahVM = new TambahVM
        {
            UserName = "Username",
            Password = "password",
            PasswordConfirmation = "password"
        };
        var dbSet = new Mock<DbSet<AppUser>>();
        var appUser = new AppUser { UserName = "UsernameLain" };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser> { appUser }, dbSet);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        //Act
        var result = await _accountController.Tambah(tambahVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        dbSet.Verify(x => x.Add(It.Is<AppUser>(a => a.UserName == tambahVM.UserName)), Times.Once());
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task Edit_Should_ReturnNotFoundResult_WhenAppUserWithIdNotFound()
    {
        //Arrange
        var id = 1;
        var daftarUser = new List<AppUser> { new AppUser { Id = 2 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Edit(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnBadRequest_WhenAppUserRoleIsSuperAdmin()
    {
        //Arrange
        var id = 1;
        var daftarUser = new List<AppUser> { new AppUser { Id = id, Role = AppUserRoles.SuperAdmin } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Edit(id);

        //Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnBadRequest_WhenAppUserIdEqualToSignedInUserId()
    {
        //Arrange
        var id = 1;
        var user = new AppUser { Id = id };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser> { user });
        _signManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);

        //Act
        var result = await _accountController.Edit(id);

        //Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Edit_Should_ReturnViewResult_WhenSuccess()
    {
        //Arrange
        var id = 1;
        var user = new AppUser { Id = id, UserName = "Username" };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser> { user });
        _signManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(new AppUser { Id = 2 });

        //Act
        var result = await _accountController.Edit(id);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Id.Should().Be(id);
        model.UserName.Should().Be(user.UserName);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
    {
        //Arrange
        var editVM = new EditVM() 
        {
            Id = 1,
            UserName = "Username"
        };
        _accountController.ModelState.AddModelError(string.Empty, string.Empty);

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().BeEquivalentTo(editVM);
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndCallAddNotification_WhenAppUserNotFound()
    {
        //Arrange
        var actionName = nameof(AccountController.Index);
        var editVM = new EditVM()
        {
            Id = 1,
            UserName = "Username"
        };
        var daftarUser = new List<AppUser> { new AppUser { Id = 2 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
        _notificationService
            .Verify(
                x => x.AddNotification(It.Is<ToastrNotification>(x => x.Type == ToastrNotificationType.Error)),
                Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenUserNameNotUnique()
    {
        //Arrange
        var editVM = new EditVM()
        {
            Id = 1,
            UserName = "Username"
        };
        var daftarUser = new List<AppUser> 
        { 
            new AppUser { Id = 1 }, 
            new AppUser { Id = 2, UserName = editVM.UserName } 
        };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().BeEquivalentTo(editVM);
        _accountController.ModelState.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var editVM = new EditVM()
        {
            Id = 1,
            UserName = "Username"
        };
        var daftarUser = new List<AppUser> { new AppUser { Id = 1 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;
        model.Should().BeEquivalentTo(editVM);
        _accountController.ModelState.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task EditPOST_Should_HashPassword_WhenPasswordIsNotNull()
    {
        //Arrange
        var editVM = new EditVM()
        {
            Id = 1,
            UserName = "Username",
            Password = "PasswordBaru",
            PasswordConfirmation = "PasswordBaru",
        };
        var daftarUser = new List<AppUser>{ new AppUser { Id = 1 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        _passwordHasher.Verify(x => x.HashPassword(It.IsAny<AppUser>(), editVM.Password), Times.Once());
    }

    [Fact]
    public async Task EditPOST_Should_ReturnRedirectToActionIndexAndCallSaveChangesAsync_WhenSucces()
    {
        //Arrange
        var actionName = nameof(AccountController.Index);
        var editVM = new EditVM()
        {
            Id = 1,
            UserName = "Username"
        };
        var daftarUser = new List<AppUser> { new AppUser { Id = 1 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<int>());

        //Act
        var result = await _accountController.Edit(editVM);

        //Assert
        _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToActionResult.ActionName.Should().Be(actionName);
    }

    [Fact]
    public async Task Hapus_Should_ReturnNotFoundResult_WhenAppUserNotFound()
    {
        //Arrange
        var id = 1;
        var daftarUser = new List<AppUser> { new AppUser { Id = 2 } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Hapus(id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Hapus_Should_ReturnBadRequestResult_WhenAppUserRoleIsSuperAdmin()
    {
        //Arrange
        var id = 1;
        var appUser = new AppUser { Id = id , Role = AppUserRoles.SuperAdmin };
        var daftarUser = new List<AppUser> { appUser };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);

        //Act
        var result = await _accountController.Hapus(id);

        //Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Hapus_Should_ReturnBadRequest_WhenAppUserIdEqualSignedInAppUserId()
    {
        //Arrange
        var id = 1;
        var appUser = new AppUser { Id = id };
        var daftarUser = new List<AppUser> { appUser };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);
        _signManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(appUser);

        //Act
        var result = await _accountController.Hapus(id);

        //Assert
        _signManager.Verify(x => x.GetSignedInUser(), Times.Once());
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Hapus_Should_ReturnRedirectToActionIndexResultAndAddErrorNotification_WhenSaveChangesAsyncThrow()
    {
        //Arrange
        var actionName = nameof(AccountController.Index);
        var id = 1;
        var daftarUser = new List<AppUser> { new AppUser { Id = id } };
        _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarUser);
        _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _accountController.Hapus(id);

        //Assert
        var redirectToAction = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirectToAction.ActionName.Should().Be(actionName);
        _notificationService.Verify(
            x => x.AddNotification(It.Is<ToastrNotification>(t => t.Type == ToastrNotificationType.Error)),
            Times.Once());
    }
}
