using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class PostEnvelope
    {

        public PostVM Post { get; set; }

        public PostEnvelope(PostVM post)
        {
            Post = post;
        }
    }

    public class PostVM
    {
        public int Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int Hearts { get; set; }
        //public List<Sticker> Stickers { get; set; }
        //public List<Comment> Comments { get; set; }
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; }

    }
}
