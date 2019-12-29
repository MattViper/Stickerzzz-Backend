using AutoMapper;
using Stickerzzz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class MappingPost : Profile
    {
        public MappingPost()
        {
            CreateMap<Post, PostVM>(MemberList.Destination);
        }
    }
}
