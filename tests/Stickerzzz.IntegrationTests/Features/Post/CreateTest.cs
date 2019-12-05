using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.IntegrationTests.Features.User;
using Stickerzzz.Web.Posts;
using Xunit;
namespace Stickerzzz.IntegrationTests.Features.Post
{
	public class CreateTest : SliceFixture
	{
        //complete test with tags and stickers data
        [Fact]
        public async Task Expect_Create_Post()
        {
            var command = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Content = "Content of the test post",                  
                }

            };
            var post = await PostHelpers.CreatePost(this, command);

            Assert.NotNull(post);
            Assert.Equal(post.Title, command.Post.Title);
            Assert.Equal(post.Content, command.Post.Content);
            Assert.Equal(post.Stickers.Count(), command.Post.StickersData.Count());
        }
    }
}
