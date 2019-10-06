using System.Collections.Generic;

namespace Stickerzzz.Core.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }

        
    }
}