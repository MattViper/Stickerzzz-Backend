using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.ApiModels
{
    public class PostSpec
    {
        public string Content { get; set; }
        public Guid CreatorId { get; set; }
        public List<StickerSpec> Stickers { get; set; }

    }
}
