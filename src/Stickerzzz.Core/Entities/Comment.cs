using Stickerzzz.Core.SharedKernel;
using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class Comment : BaseEntityWithUser<int, AppUser>
    {
        public string Content { get; set; }
        public int Hearts { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
