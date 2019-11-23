using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.ApiModels
{
    public class PostDto
    {
        public int Id { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public ICollection<StickerDto> Stickers { get; set; }
        public string Content { get; set; }
        public int Hearts { get; set; }
        public ICollection<CommentDto> Comments { get; set; }

        public bool Favorited { get; set; }

        public int FavoritesCount { get; set; }
    }
}
