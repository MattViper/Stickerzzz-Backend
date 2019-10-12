using Stickerzzz.Core.SharedKernel;
using System.Collections.Generic;

namespace Stickerzzz.Core.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(T id) where T : BaseEntity<T>;
        List<T> List<T>() where T : BaseEntity<T>;
        T Add<T>(T entity) where T : BaseEntity<T>;
        void Update<T>(T entity) where T : BaseEntity<T>;
        void Delete<T>(T entity) where T : BaseEntity<T>;
    }
}
