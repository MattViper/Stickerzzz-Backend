using Stickerzzz.Web.Users;
using System.Threading.Tasks;

namespace Stickerzzz.IntegrationTests.Features.User
{
    public static class UserHelpers
    {
        public static readonly string DefaultUserName = "mattviper";



        /// <summary>

        /// creates a default user to be used in different tests

        /// </summary>

        /// <param name="fixture"></param>

        /// <returns></returns>

        public static async Task<Web.Users.User> CreateDefaultUser(SliceFixture fixture)

        {

            var command = new Create.Command()

            {

                User = new Create.UserData()

                {

                    Email = "email",

                    Password = "password",

                    UserName = DefaultUserName

                }

            };



            var commandResult = await fixture.SendAsync(command);

            return commandResult.User;

        }
    }
}
