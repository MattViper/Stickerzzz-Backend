//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http.ModelBinding;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.Extensions.Logging;
//using Stickerzzz.Core.Users;
//using Stickerzzz.Web.ApiModels;
//using Stickerzzz.Web.Responses;
//using Stickerzzz.Web.Services;
//using Stickerzzz.Web.Services.Interfaces;

//namespace Stickerzzz.Web.Controllers
//{

//    public class PostsController : BaseController
//    {
//        private readonly ILogger<PostsController> _logger = null;
//        private readonly IPostsService _postsService;

//        public PostsController(
//            ILoggerFactory loggerFactory,
//            IPostsService postsService
//            )
//            : base(loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<PostsController>();
//            _postsService = postsService;
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetFeed([FromQuery] int? limit, [FromQuery] int? offset) => await _postsService.GetFeed(limit, offset);

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        public async Task<IActionResult> Create([FromBody] PostSpec postSpec) => await _postsService.Create(postSpec);


//        [HttpDelete]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<IActionResult> Delete([FromQuery] int id) => await _postsService.Delete(id);


//    }
//}