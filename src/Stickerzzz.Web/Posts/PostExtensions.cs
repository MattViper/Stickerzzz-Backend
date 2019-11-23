using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public static class PostExtensions
    {
        public static IQueryable<Post> GetAllData(this DbSet<Post> posts)
        {
            return posts
                .Where(i => !i.IsDeleted)
                .Include(i => i.PostStickers)
                    .ThenInclude(ps => ps.Sticker)
                .Include(i => i.PostFavorites)
                    .ThenInclude(pf => pf.User)
                .Include(i => i.Creator)
                .AsNoTracking();
        }
    }
}
