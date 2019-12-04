using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Users
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Token { get; set; }
    }


    public class UserEnvelope
    {
        public UserEnvelope(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
