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
	public class CreateTest : Slicefixture
	{
        [Fact]
        public async Task Expect_Create_Post()
        {
            var command = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Description = "Description of the test post",
                    Body = "Body of the test post",
                    TagList = new string[] { "tag1", "tag2" }
                }

            };
            var post = await PostHelpers.CreatePost(this, command);

            Assert.NotNull(post);
            Assert.Equal(post.Title, command.post.Title);
            Assert.Equal(post.TagList.Count(), command.post.TagList.Count());
        }
    }
}
