using System;

namespace Stickerzzz.IntegrationTests.Features.Post
{
    public class EditTest : Slicefixture
    {
        [Fact]
        public async Task Expect_Edit_Post()
        {
            var createCommand = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Description = "Description of the test post",
                    Body = "Body of the test post",
                    TagList = new string[] { "tag1", "tag2" }
                }

            };
        }
    }
}
