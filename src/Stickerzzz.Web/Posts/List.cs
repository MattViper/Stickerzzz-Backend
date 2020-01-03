using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class List
    {
        public class Query : IRequest<PostsEnvelope>
        {
            public string Creator { get; }
            public string Tag { get; }
            public string FavoritedUsername { get; }
            public int? Limit { get; }
            public int? Offset { get; }
            public bool IsFeed { get; set; }

            public Query(string creator, string tag, string favorited, int? limit, int? offset)
            {
                Creator = creator;
                Tag = tag;
                FavoritedUsername = favorited;
                Limit = limit;
                Offset = offset;
            }

            public class QueryHandler : IRequestHandler<Query, PostsEnvelope>
            {
                private readonly AppDbContext _context;
                private readonly ICurrentUserAccessor _currentUserAccessor;
                private readonly IMapper _mapper;
                public QueryHandler(AppDbContext context, ICurrentUserAccessor currentUserAccessor, IMapper mapper)
                {
                    _context = context;
                    _currentUserAccessor = currentUserAccessor;
                    _mapper = mapper;
                }

                public async Task<PostsEnvelope> Handle(Query message, CancellationToken cancellationToken)
                {
                    IQueryable<Post> queryable = _context.Posts.GetAllData();

                    if (message.IsFeed && _currentUserAccessor.GetCurrentUsername() != null)
                    {
                        var currentUser = await _context.Users.Include(i => i.FriendRequestsAccepted).FirstOrDefaultAsync(i => i.UserName == _currentUserAccessor.GetCurrentUsername(), cancellationToken);
                        queryable = queryable.Where(i => currentUser.FriendRequestsAccepted.Select(f => f.FriendId).Contains(i.Creator.Id));
                    }

                    if (!string.IsNullOrWhiteSpace(message.Tag))
                    {
                        var tag = await _context.TagStickers.FirstOrDefaultAsync(x => x.TagId == message.Tag, cancellationToken);
                        if (tag != null)
                        {
                            queryable = queryable.Where(x => x.PostStickers.Any(ps => ps.Sticker.TagList.Contains(tag.TagId)));  //might be wrong
                        }
                        else
                        {
                            return new PostsEnvelope();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(message.Creator))
                    {
                        var creator = await _context.Users.FirstOrDefaultAsync(x => x.UserName == message.Creator, cancellationToken);
                        if (creator != null)
                        {
                            queryable = queryable.Where(x => x.Creator == creator);
                        }
                        else
                        {
                            return new PostsEnvelope();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(message.FavoritedUsername))
                    {
                        var creator = await _context.Users.FirstOrDefaultAsync(x => x.UserName == message.FavoritedUsername, cancellationToken);
                        if (creator != null)
                        {
                            queryable = queryable.Where(i => i.PostFavorites.Any(pf => pf.UserId == creator.Id));
                        }
                        else
                        {
                            return new PostsEnvelope();
                        }
                    }

                    var posts = await queryable
                        .OrderByDescending(i => i.LastModificationTime)
                        .Skip(message.Offset ?? 0)
                        .Take(message.Limit ?? 20)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    var postsVM = _mapper.Map<List<Post>, List<PostVM>>(posts);
                    return new PostsEnvelope()
                    {
                        Posts = postsVM,
                        PostsCount = postsVM.Count

                    };

                }
            }

            
        }
    }
}
