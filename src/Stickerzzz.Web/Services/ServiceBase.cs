using Microsoft.AspNetCore.Mvc;
using Stickerzzz.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Services
{
    public class ServiceBase
    {
        protected readonly AppDbContext context;
        public ServiceBase(AppDbContext context)
        {
            this.context = context;
        }

    }
}
