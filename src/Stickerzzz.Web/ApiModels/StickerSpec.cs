using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.ApiModels
{
    public class StickerSpec
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Img { get; set; }
        public List<string> TagList { get; set; }

        public Guid CreatorId { get; set; }

    }
}