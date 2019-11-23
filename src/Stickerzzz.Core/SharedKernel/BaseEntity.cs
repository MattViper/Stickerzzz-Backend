using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stickerzzz.Core.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        [Key]
        [JsonIgnore]
        public TId Id { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual Guid? DeleterId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }


    }
}