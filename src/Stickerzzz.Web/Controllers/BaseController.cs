using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stickerzzz.Core.Users;
using Stickerzzz.Web.Responses.Factories;
using Stickerzzz.Web.Services.ServiceResponses;

namespace Stickerzzz.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BaseController : ControllerBase
    {
        protected readonly ILoggerFactory loggerFactory = null;

        public BaseController(
            ILoggerFactory loggerFactory
        )

        {
            this.loggerFactory = loggerFactory;
        }

    }
}