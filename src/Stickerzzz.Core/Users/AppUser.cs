using Microsoft.AspNetCore.Identity;
using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.Users
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Sticker> Stickers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserStickers> UserStickers { get; set; }
        public ICollection<PostFavorites> PostFavorites { get; set; }
        public ICollection<Friendship> FriendRequestsMade { get; set; }
        public ICollection<Friendship> FriendRequestsAccepted { get; set; }
    }
}
