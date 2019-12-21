using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
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

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
        [JsonIgnore]
        public ICollection<Sticker> Stickers { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        public ICollection<UserStickers> UserStickers { get; set; }
        [JsonIgnore]
        public ICollection<PostFavorites> PostFavorites { get; set; }
        [JsonIgnore]
        public ICollection<Friendship> FriendRequestsMade { get; set; }
        [JsonIgnore]
        public ICollection<Friendship> FriendRequestsAccepted { get; set; }
    }
}
