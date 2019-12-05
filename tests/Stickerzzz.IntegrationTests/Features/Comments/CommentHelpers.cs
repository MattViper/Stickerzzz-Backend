//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Stickerzzz.Core.Entities;
//using Stickerzzz.IntegrationTests.Features.User;
//using Stickerzzz.Web.Posts;
//using Microsoft.EntityFrameworkCore;
//using Xunit;
//namespace Stickerzzz.IntegrationTests.Features.Comments
//{
//    public static class CommentHelpers
//    {
//        /// <summary>
//        /// creates an post comment based on the given Create command. 
//        /// Creates a default user if parameter userName is empty.
//        /// </summary>
//        /// <param name="fixture"></param>
//        /// <param name="command"></param>
//        /// <param name="userName"></param>
//        /// <returns></returns>

//        public static async Task<Stickerzzz.Core.Entities.Comment> CreateComment(SliceFixture fixture, Create.Command command, string userName)
//        {
//            if (string.IsNullOrWhiteSpace(userName))
//            {
//                var user = await UserHelpers.CreateDefaultUser(fixture);
//                userName = user.Username;
//            }

//            var dbContext = fixture.GetDbContext();
//            var currentAccessor = new StubCurrentUserAccessor(userName);

//            var commentCreateHandler = new Create.Handler(dbContext, currentAccessor);
//            var created = await commentCreateHandler.Handle(command, new System.Threading.CancellationToken());
          
//            var dbPostWithComments = await fixture.ExecuteDbContextAsync(
//               db => db.Posts
//                   .Include(a => a.Comments).Include(a => a.)
//                   .Where(a => a.Slug == command.Slug)
//                   .SingleOrDefaultAsync()
//           );
//            var dbComment = dbPostWithComments.Comments
//               .Where(c => c.Id == dbPostWithComments.Id && c.Author == dbPostWithComments.Author)
//               .FirstOrDefault();



//            return dbComment;
//        }
//    }
//}
