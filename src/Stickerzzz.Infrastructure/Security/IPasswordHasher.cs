using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}
