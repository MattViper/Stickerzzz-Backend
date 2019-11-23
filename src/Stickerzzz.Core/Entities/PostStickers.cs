using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class PostStickers
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int StickerId { get; set; }
        public Sticker Sticker { get; set; }
    }
}
