using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using System.Linq.Expressions;

namespace PKMGerejaEbenhaezer.UnitTest.Controller.Dashboard;

public class AccountControllerTests
{
    private readonly Mock<IAppDbContext> _appDbContext;
    private readonly Mock<ILogger<AccountController>> _logger;
    private readonly Mock<IToastrNotificationService> _notificationService;
    private readonly Mock<IPasswordHasher<AppUser>> _passwordHasher;

    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        //Depedencies
        _appDbContext = new Mock<IAppDbContext>();
        _logger = new Mock<ILogger<AccountController>>();
        _notificationService = new Mock<IToastrNotificationService>();
        _passwordHasher = new Mock<IPasswordHasher<AppUser>>();

        //SUT
        _accountController = new AccountController(
            _appDbContext.Object,
            _logger.Object,
            _notificationService.Object,
            _passwordHasher.Object);
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
}
