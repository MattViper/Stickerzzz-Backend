using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class PostsEnvelope
    {
        public List<Post> Posts { get; set; }
        public int PostsCount { get; set; }
    }
}
