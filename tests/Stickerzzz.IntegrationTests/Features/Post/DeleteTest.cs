using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.IntegrationTests.Features.User;
using Stickerzzz.Web.Posts;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace Stickerzzz.IntegrationTests.Features.Post
{
    public class DeleteTest : Slicefixture
    {
        [Fact]
        public async Task Expect_Delete_Post()
        {
            var createCmd = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Description = "Description of the test post",
                    Body = "Body of the test post",
                }
            };
            var post = await PostHelpers.CreatePost(this, createCmd);
            var slug = post.Slug;

            var deleteCmd = new Delete.Command(slug);

            var dbContext = GetDbContext();

            var postDeleteHandler = new Delete.QueryHandler(dbContext);
            await postDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbPost = await ExecuteDbContextAsync(db => db.Post.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());

            Assert.Null(dbPost);
        }

       
    }
}
