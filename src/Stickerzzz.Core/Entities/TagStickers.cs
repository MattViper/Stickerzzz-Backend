using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class TagStickers
    {

        public string TagId { get; set; }
        public Tag Tag { get; set; }

        public int StickerId { get; set; }
        public Sticker Sticker { get; set; }


    }
}
