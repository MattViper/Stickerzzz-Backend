using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class UserStickers
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public int StickerId { get; set; }
        public Sticker Sticker { get; set; }
    }
}
