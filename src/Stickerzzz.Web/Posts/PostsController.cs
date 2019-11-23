using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stickerzzz.Web.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PostsEnvelope> Get([FromQuery] string creator, [FromQuery] string tag, [FromQuery] string favorited, int? limit, int? offset)
            => await _mediator.Send(new List.Query(creator, tag, favorited, limit, offset));

        [HttpGet("feed")]
        public async Task<PostsEnvelope> GetFeed([FromQuery] string creator, [FromQuery] string tag, [FromQuery] string favorited, int? limit, int? offset)
            => await _mediator.Send(new List.Query(creator, tag, favorited, limit, offset) { IsFeed = true });

        [HttpGet("{slug}")]
        public async Task<PostEnvelope> Get(string slug)
            => await _mediator.Send(new Details.Query(slug));

    }
}