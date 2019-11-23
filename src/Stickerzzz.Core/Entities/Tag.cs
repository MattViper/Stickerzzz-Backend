using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class Tag
    {
        public string TagId { get; set; }
        public List<TagStickers> PostTags { get; set; }
    }
}
