using Newtonsoft.Json;
using Stickerzzz.Core.SharedKernel;
using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class Post : BaseEntityWithUser<int, AppUser>
    {
        public ICollection<Sticker> Stickers { get; set; }
        public string Content { get; set; }
        public int Hearts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostStickers> PostStickers { get; set; }

        [JsonIgnore]
        public ICollection<PostFavorites> PostFavorites { get; set; }

        [NotMapped]
        public bool Favorited => PostFavorites?.Any() ?? false;

        [NotMapped]
        public int FavoritesCount => PostFavorites?.Count ?? 0;
    }
}
