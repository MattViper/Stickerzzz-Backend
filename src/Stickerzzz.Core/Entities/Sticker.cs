using Stickerzzz.Core.SharedKernel;
using Stickerzzz.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Stickerzzz.Core.Entities
{
    public class Sticker : BaseEntity<int>
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Img { get; set; }

        [NotMapped]
        public List<string> TagList => (TagStickers.Select(i => i.TagId) ?? Enumerable.Empty<string>()).ToList();


        public ICollection<PostStickers> PostStickers { get; set; }
        public ICollection<UserStickers> UserStickers { get; set; }
        public ICollection<TagStickers> TagStickers { get; set; }
    }
}
