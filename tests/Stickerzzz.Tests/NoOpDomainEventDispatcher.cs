using Stickerzzz.Core.Interfaces;
using Stickerzzz.Core.SharedKernel;

namespace Stickerzzz.UnitTests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispatch(BaseDomainEvent domainEvent) { }
    }
}
