using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.IntegrationTests.Features.User;
using Stickerzzz.Web.Posts;
using Xunit;
namespace Stickerzzz.IntegrationTests.Features.Post
{
    public class DeleteTest : SliceFixture
    {
        private readonly IMapper _mapper;
        public DeleteTest(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public async Task Expect_Delete_Post()
        {
            var createCmd = new Create.Command()
            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Content = "Content of the test post",
                }
            };
            var post = await PostHelpers.CreatePost(this, createCmd, _mapper);
            var slug = post.Slug;

            var deleteCmd = new Delete.Command(slug);

            var dbContext = GetDbContext();

            var postDeleteHandler = new Delete.Command.QueryHandler(dbContext);
            await postDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbPost = await ExecuteDbContextAsync(db => db.Posts.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());

            Assert.Null(dbPost);
        }

        [Fact]
        public async Task Expect_Delete_Post_With_Tags()
        {
            var createCmd = new Create.Command()

            {
                Post = new Create.PostData()
                {
                    Title = "Test post",
                    Content = "Content of the test post",

                }
            };
            var post = await PostHelpers.CreatePost(this, createCmd, _mapper);

            //var dbPostWithTags = await ExecuteDbContextAsync(

            //    db => db.Posts.Include(a => a.PostTags)

            //    .Where(d => d.Slug == post.Slug).SingleOrDefaultAsync()

            //);


            var deleteCmd = new Delete.Command(post.Slug);

            var dbContext = GetDbContext();

            var postDeleteHandler = new Delete.Command.QueryHandler(dbContext);
            await postDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbPost = await ExecuteDbContextAsync(db => db.Posts.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
            Assert.Null(dbPost);

        }
        //[Fact]

        //public async Task Expect_Delete_Post_With_Comments()

        //{
        //    var createPostCmd = new Create.Command()
        //    {
        //        Post = new Create.PostData()
        //        {
        //            Title = "Test post",
        //            Description = "Description of the test post",
        //            Body = "Body of the test post",
        //        }
        //    };


        //    var post = await PostHelpers.CreatePost(this, createPostCmd);
        //    var dbPost = await ExecuteDbContextAsync(
        //        db => db.Posts.Include(a => a.PostTags)
        //        .Where(d => d.Slug == Post.Slug).SingleOrDefaultAsync()
        //    );
        //    var Id = dbPost.Id;
        //    var slug = dbPost.Slug;

        //    // create post comment
        //    var createCommentCmd = new Stickerzzz.Core.Entities.Comment.Create.Command()
        //    {
        //        Comment = new Stickerzzz.Core.Entities.Comment.Create.CommentData()
        //        {
        //            Body = "post comment"
        //        },
        //        Slug = slug
        //    };
        //    var comment = await CommentHelpers.CreateComment(this, createCommentCmd, UserHelpers.DefaultUserName);



        //    // delete post with comment

        //    var deleteCmd = new Delete.Command(slug);

        //    var dbContext = GetDbContext();

        //    var postDeleteHandler = new Delete.QueryHandler(dbContext);
        //    await postDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

        //    var deleted = await ExecuteDbContextAsync(db => db.Post.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
        //    Assert.Null(deleted);

        //}
    }
}
