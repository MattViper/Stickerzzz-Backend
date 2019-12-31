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
        public Guid CreatorId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int Hearts { get; set; }
        public List<StickerVM> Stickers { get; set; }
        public List<CommentVM> Comments { get; set; }
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; }

    }

    public class StickerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Img { get; set; }
        public List<string> TagList { get; set; }

    }

    public class CommentVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Hearts { get; set; }
        public int PostId { get; set; }
        public  Guid CreatorId { get; set; }

    }
}
