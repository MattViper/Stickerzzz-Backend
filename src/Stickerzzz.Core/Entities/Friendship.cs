using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Entities
{
    public class Friendship
    {
        public Guid ApplicationUserId { get; set; }
        public AppUser ApplicationUser { get; set; }

        public Guid FriendId { get; set; }
        public AppUser Friend { get; set; }

        public StatusCode Status { get; set; }

        public Guid ActionUserId { get; set; }
        public AppUser ActionUser { get; set; }

        public DateTime Timestamp { get; set; }
    }

    public enum StatusCode
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2,
        Blocked = 3
    }
}
