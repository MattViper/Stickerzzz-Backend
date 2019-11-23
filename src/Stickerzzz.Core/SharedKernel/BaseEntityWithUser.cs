using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stickerzzz.Core.SharedKernel
{
    public class BaseEntityWithUser<TKey, TUser> : BaseEntity<TKey>
    {
        public virtual TUser Creator { get; set; }
        public virtual Guid CreatorId { get; set; }
        public virtual TUser LastModifier { get; set; }
    }
}
