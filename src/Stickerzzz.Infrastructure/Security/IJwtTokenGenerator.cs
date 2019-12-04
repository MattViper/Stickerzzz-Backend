using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stickerzzz.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        Task<string> CreateToken(string username);
    }
}
