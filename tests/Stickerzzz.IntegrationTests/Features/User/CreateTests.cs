using Microsoft.EntityFrameworkCore;
using Stickerzzz.Infrastructure.Security;
using Stickerzzz.Web.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stickerzzz.IntegrationTests.Features.User
{
    public class CreateTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Create_User()
        {
            var command = new Web.Users.Create.Command()
            {
                User = new Create.UserData()
                {
                    Email = "mv2@gmail.com",
                    Password = "password",
                    UserName = "mv2"
                }
            };

            await SendAsync(command);

            var created = await ExecuteDbContextAsync(db => db.Users.Where(d => d.Email == command.User.Email).SingleOrDefaultAsync());

            Assert.NotNull(created);
            Assert.Equal(created.Hash, new PasswordHasher().Hash("password", created.Salt));
        }
    }
}
