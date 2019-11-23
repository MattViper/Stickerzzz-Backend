using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Infrastructure.Data
{
    public interface ICurrentUserAccessor
    {
        string GetCurrentUsername();
    }
}
