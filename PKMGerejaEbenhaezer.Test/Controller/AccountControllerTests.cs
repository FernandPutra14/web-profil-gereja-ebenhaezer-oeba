using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Controllers;
using PKMGerejaEbenhaezer.Web.Models.Account;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.Test.Controller
{
    public class AccountControllerTests
    {
        private readonly Mock<ISignInManager> _signInManager;
        private readonly Mock<IAppDbContext> _appDbContext;
        private readonly Mock<ILogger<AccountController>> _logger;
        private readonly Mock<IToastrNotificationService> _toastrNotificationService;
        private readonly Mock<IPasswordHasher<AppUser>> _passwordHasher;

        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _signInManager = new Mock<ISignInManager>();
            _appDbContext = new Mock<IAppDbContext>();
            _logger = new Mock<ILogger<AccountController>>();
            _toastrNotificationService = new Mock<IToastrNotificationService>();
            _passwordHasher = new Mock<IPasswordHasher<AppUser>>();

            _accountController = new AccountController(
                _signInManager.Object,
                _appDbContext.Object,
                _logger.Object,
                _toastrNotificationService.Object,
                _passwordHasher.Object);
        }

        [Fact]
        public void Login_Should_ReturnViewResult()
        {
            //Act
            var result =  _accountController.Login("");

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.Model.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<LoginVM>();
        }

        [Fact]
        public async Task LoginPOST_Should_ReturnViewResult_WhenModelStateNotValid()
        {
            //Arrange
            var loginVM = new LoginVM();
            _accountController.ModelState.AddModelError(string.Empty, string.Empty);

            //Act
            var result = await _accountController.Login(loginVM, "");

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<LoginVM>();
        }

        [Fact]
        public async Task LoginPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSignInFailed()
        {
            //Arrange
            var loginVM = new LoginVM();

            _signInManager.Setup(s => s.SignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(false);

            //Act
            var result = await _accountController.Login(loginVM, "");

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<LoginVM>();

            _accountController.ModelState.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task LoginPOST_Should_ReturnRedirectResult_WhenSignInSuccess()
        {
            //Arrange
            var loginVM = new LoginVM();
            string returnUrl = "/Dashboard";

            _signInManager.Setup(s => s.SignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(true);

            //Act
            var result = await _accountController.Login(loginVM, returnUrl);

            //Assert
            result.Should().NotBeNull();
            var redirectResult = result.Should().BeOfType<RedirectResult>().Subject;

            redirectResult.Should().NotBeNull();
            redirectResult.Url.Should().Be(returnUrl);
        }

        [Fact]
        public async Task Logout_Should_ReturnRedirectToActionIndexResult()
        {
            //Arrange
            var actionName = "Index";
            var controllerName = "Home";

            //Act
            var result = await _accountController.Logout();

            //Assert
            result.Should().NotBeNull();
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;

            redirectToActionResult.ActionName.Should().Be(actionName);
            redirectToActionResult.ControllerName.Should().Be(controllerName);
        }

        [Fact]
        public async Task Edit_Should_ReturnViewResult_WhenGetUserSignedInUserReturnNotNull()
        {
            //Arrange
            var user = new AppUser() { UserName = "Name" };
            _signInManager.Setup(s => s.GetSignedInUser()).ReturnsAsync(user);

            //Act
            var result =  await _accountController.Edit();

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            var model = viewResult.Model.Should().BeOfType<EditVM>().Subject;

            model.UserName.Should().Be(user.UserName);
        }

        [Fact]
        public async Task Edit_Should_ReturnRedirectToActionLoginResultWhenGetSignedInUserReturnNull()
        {
            //Arrange
            var actionName = "Login";
            AppUser? user = null;
            _signInManager.Setup(s => s.GetSignedInUser()).ReturnsAsync(user);

            //Act
            var result = await _accountController.Edit();

            //Assert
            result.Should().NotBeNull();
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;

            redirectToActionResult.ActionName.Should().Be(actionName);
        }

        [Fact]
        public async Task EditPOST_Should_ReturnViewResult_WhenModelStateNotValid()
        {
            //Arrange
            var editVM = new EditVM();
            _accountController.ModelState.AddModelError(string.Empty, string.Empty);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Model.Should().BeOfType<EditVM>();
        }

        [Fact]
        public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenGetSignedUserReturnNull()
        {
            //Arrange
            var editVM = new EditVM();
            AppUser? user = null;

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            result.Should().NotBeNull();
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Model.Should().BeOfType<EditVM>();
            _accountController.ModelState.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNewUserNameNotUnique()
        {
            //Arrange
            var duplicateUserName = "Name";
            var editVM = new EditVM { UserName = duplicateUserName};
            var user = new AppUser { Id = 1 };
            var daftarAppUser = new AppUser[] { new AppUser { Id = 2, UserName = duplicateUserName } };

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);
            _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarAppUser);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<EditVM>();
            _accountController.ModelState.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenNewPasswordEqualToOldPassword()
        {
            //Arrange
            var password = "Password";
            var editVM = new EditVM() { Password = password };
            var user = new AppUser();

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);
            _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser>());
            _passwordHasher.Setup(x => x.VerifyHashedPassword(null, user.PasswordHash, password))
                .Returns(PasswordVerificationResult.Success);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<EditVM>();
            _accountController.ModelState.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenSaveChangesAsyncThrow()
        {
            //Arrange
            var editVM = new EditVM { UserName = "NewUserName", Password = "NewPassword" };
            var user = new AppUser();

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);
            _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser>() { user });
            _passwordHasher.Setup(x => x.VerifyHashedPassword(null, user.PasswordHash, editVM.Password))
                .Returns(PasswordVerificationResult.Failed);
            _appDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(It.IsAny<Exception>());

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            _accountController.ModelState.IsValid.Should().BeFalse();
            _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task EditPOST_Should_ReturnRedirectToActionLoginResult_WhenSuccess()
        {
            //Arrange
            var actionName = "Login";
            var editVM = new EditVM { UserName = "NewUserName", Password = "NewPassword" };
            var user = new AppUser();

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);
            _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(new List<AppUser>() { user });
            _passwordHasher.Setup(x => x.VerifyHashedPassword(null, user.PasswordHash, editVM.Password))
                .Returns(PasswordVerificationResult.Failed);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectToActionResult.ActionName.Should().Be(actionName);
            _accountController.ModelState.IsValid.Should().BeTrue();
            _appDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
