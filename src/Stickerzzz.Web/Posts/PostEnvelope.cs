using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class PostEnvelope
    {
        public Post Post { get; set; }

        public PostEnvelope(Post post)
        {
            Post = post;
        }
    }
}
