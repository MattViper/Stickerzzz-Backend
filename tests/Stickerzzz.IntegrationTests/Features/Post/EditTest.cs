using AutoMapper;
using Stickerzzz.Web.Posts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Stickerzzz.IntegrationTests.Features.Post
{
    public class EditTest : SliceFixture
    {
        private readonly IMapper _mapper;
        public EditTest(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }
        [Fact]
        public async Task Expect_Edit_Post()
        {
            var createCommand = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Content = "Test Content"
                }
             };

            var createdPost = await PostHelpers.CreatePost(this, createCommand, _mapper);


            var command = new Edit.Command()
            {
                Post = new Edit.PostData()
                {
                    Title = "Updated " + createdPost.Title,
                    Content = "Updated " + createdPost.Content

                },
                Slug = createdPost.Slug
            };
            // remove the first tag and add a new tag

            //command.Post.TagList = new string[] { createdPost.TagList[1], "tag3" };

            var dbContext = GetDbContext();

            var postEditHandler = new Edit.Handler(dbContext, _mapper);
            var edited = await postEditHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.NotNull(edited);
            Assert.Equal(edited.Post.Title, command.Post.Title);
            //Assert.Equal(edited.Post.TagList.Count(), command.Post.TagList.Count());
            // use assert Contains because we do not know the order in which the tags are saved/retrieved
            //Assert.Contains(edited.Post.TagList[0], command.Post.TagList);
            //Assert.Contains(edited.Post.TagList[1], command.Post.TagList);
        }
    }
}
