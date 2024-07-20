using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Test
{
    public static class TestHelpers
    {
        public static Mock<AppDbContext> GetMockDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().Options;

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(Mock.Of<HttpContext>());

            return new Mock<AppDbContext>(dbOptions, mockHttpContextAccessor.Object);
        }
    }
}
