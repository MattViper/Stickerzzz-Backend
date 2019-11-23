using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class PostFavorites
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}
