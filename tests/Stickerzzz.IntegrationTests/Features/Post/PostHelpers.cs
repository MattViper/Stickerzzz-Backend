using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.IntegrationTests.Features.User;
using Stickerzzz.Web.Posts;
using Moq;

namespace Stickerzzz.IntegrationTests.Features.Post
{
    public static class PostHelpers
    {
        // <summary>
        // creates an post based on the given Create command.It also creates a default user
        // </summary>
        // <param name = "fixture" ></ param >
        // < param name="command"></param>
        // <returns></returns>
        private static readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
        public static async Task<Stickerzzz.Core.Entities.Post> CreatePost(SliceFixture fixture, Create.Command command, Mock<IMapper> mapperMock)
        {
            //first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);
            
            var dbContext = fixture.GetDbContext();
            var currentAccessor = new StubCurrentUserAccessor(user.Username);
            var postCreateHandler = new Create.Handler(dbContext, currentAccessor, mapperMock.Object);
            var created = await postCreateHandler.Handle(command, new System.Threading.CancellationToken());

            var dbPost = await fixture.ExecuteDbContextAsync(db => db.Posts.Where(a => a.Id == created.Post.Id)
                .SingleOrDefaultAsync());

            return dbPost;
        }
    }
}
