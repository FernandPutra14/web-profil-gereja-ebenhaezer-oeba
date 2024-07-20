using FluentAssertions;
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
        private readonly Mock<AppDbContext> _appDbContext;
        private readonly Mock<ILogger<AccountController>> _logger;
        private readonly Mock<IToastrNotificationService> _toastrNotificationService;

        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _signInManager = new Mock<ISignInManager>();
            _appDbContext = TestHelpers.GetMockDbContext();
            _logger = new Mock<ILogger<AccountController>>();
            _toastrNotificationService = new Mock<IToastrNotificationService>();

            _accountController = new AccountController(
                _signInManager.Object,
                _appDbContext.Object,
                _logger.Object,
                _toastrNotificationService.Object);
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
        public async Task EditPOST_Should_ReturnViewResultAndModelStateNotValid_WhenUserNameNotUnique()
        {
            //Arrange
            var duplicateUserName = "Name";
            var editVM = new EditVM { UserName = duplicateUserName};
            var user = new AppUser { UserName = duplicateUserName };
            var daftarAppUser = new AppUser[] { new AppUser { UserName = duplicateUserName } };

            _signInManager.Setup(x => x.GetSignedInUser()).ReturnsAsync(user);
            _appDbContext.Setup(x => x.AppUserTable).ReturnsDbSet(daftarAppUser);

            //Act
            var result = await _accountController.Edit(editVM);

            //Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<EditVM>();
        }
    }
}
