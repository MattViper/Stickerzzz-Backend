using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.ApiModels
{
    public class CommentDto
    {
        public int Id { get; set; }
        public virtual Guid CreatorId { get; set; }

        public string Content { get; set; }
        public int Hearts { get; set; }
    }
}
