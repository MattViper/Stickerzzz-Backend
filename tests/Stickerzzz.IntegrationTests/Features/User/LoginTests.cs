using Stickerzzz.Core.Users;
using Stickerzzz.Infrastructure.Security;
using Stickerzzz.Web.Users;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Stickerzzz.IntegrationTests.Features.User
{
    public class LoginTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Login()
        {
            var salt = Guid.NewGuid().ToByteArray();
            var person = new AppUser
            {
                UserName = "mv3",
                Email = "mv3@gmail.com",
                Hash = new PasswordHasher().Hash("password", salt),
                Salt = salt
            };
            await InsertAsync(person);

            var command = new Login.Command()
            {
                User = new Login.UserData()
                {
                    Email = "mv3@gmail.com",
                    Password = "password"
                }
            };

            var user = await SendAsync(command);

            Assert.NotNull(user?.User);
            Assert.Equal(user.User.Email, command.User.Email);
            Assert.Equal("mv3", user.User.Username);
            Assert.NotNull(user.User.Token);
        }
    }
}
