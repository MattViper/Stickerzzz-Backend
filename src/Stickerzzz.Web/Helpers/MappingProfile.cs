using AutoMapper;
using AutoMapper.Configuration;
using Stickerzzz.Core.Entities;
using Stickerzzz.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Helpers
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
        }
    }
}
