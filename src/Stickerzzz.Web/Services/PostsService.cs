//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Stickerzzz.Core.Entities;
//using Stickerzzz.Infrastructure.Data;
//using Stickerzzz.Web.ApiModels;
//using Stickerzzz.Web.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;
//using System.Web.Http.ModelBinding;

//namespace Stickerzzz.Web.Services
//{
//    public class PostsService : ServiceBase, IPostsService
//    {
//        private readonly IMapper _mapper;
//        private readonly ICurrentUserAccessor _currentUserAccessor;

//        public PostsService(AppDbContext context, IMapper mapper, ICurrentUserAccessor currentUserAccessor) : base(context)
//        {
//            _mapper = mapper;
//            _currentUserAccessor = currentUserAccessor;
//        }

//        public async Task<ServiceResponse<bool>> Create(PostSpec spec)
//        {

//            //var author = await context.Users.FirstAsync(x => x.UserName == _currentUserAccessor.GetCurrentUsername());
//            var tags = new List<Tag>();

//            foreach(var Sticker in spec.Stickers)
//            {
//                foreach(var tag in Sticker.TagList ?? Enumerable.Empty<string>())
//                {
//                    var t = await context.Tags.FindAsync(tag);
//                    if (t == null)
//                    {
//                        t = new Tag()
//                        {
//                            TagId = tag
//                        };
//                        await context.Tags.AddAsync(t);
//                        await context.SaveChangesAsync();
//                    }
//                    tags.Add(t);
//                }
//            }

//            var createdPost = _mapper.Map<Post>(spec);
//            //createdPost.Creator = author;
//            createdPost.CreatorId = spec.CreatorId;

//            var addedStickers = _mapper.Map<List<Sticker>>(spec.Stickers);

//            await context.Posts.AddAsync(createdPost);
//            await context.Stickers.AddRangeAsync(addedStickers);

//            foreach(var sticker in createdPost.Stickers) 
//            {
//                await context.TagStickers.AddRangeAsync(tags.Select(x => new TagStickers()
//                {
//                    Tag = x,
//                    Sticker = sticker
//                }));

//                await context.PostStickers.AddRangeAsync(new PostStickers()
//                {
//                    Post = createdPost,
//                    Sticker = sticker
//                });
//            }

//            await context.SaveChangesAsync();

//            return ServiceResponse<bool>.Ok(true, "Post created successfully");


//        }

//        public async Task<ServiceResponse<bool>> Delete(int id)
//        {
//            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

//            if (post == null)
//            {
//                return ServiceResponse<bool>.Error("Post not found");
//            }

//            context.Posts.Remove(post);
//            await context.SaveChangesAsync();
//            return ServiceResponse<bool>.Ok(true, "Post has been deleted");
//        }

//        public async Task<ServiceResponse<PostDto>> Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<ServiceResponse<IEnumerable<PostDto>>> GetFeed(int? limit,  int? offset)
//        {
//            var query = context.Posts
//                .Where(i => !i.IsDeleted)
//                .Include(i => i.PostStickers)
//                    .ThenInclude(j => j.Sticker)
//                        //.ThenInclude(k => k.TagList)
//                .Include(i => i.PostFavorites)
//                    .ThenInclude(j => j.User)
//                .Include(i => i.Creator);


//            var posts = await query
//                .Skip(offset ?? 0)
//                .Take(limit ?? 20)
//                .Select(row => _mapper.Map<PostDto>(row))
//                .ToListAsync();

//            return ServiceResponse<IEnumerable<PostDto>>.Ok(posts);
//        }


//        public async Task<ServiceResponse<bool>> Update(int id, PostSpec spec)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
