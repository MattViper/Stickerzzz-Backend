using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<UserEnvelope> Create([FromBody] Create.Command command) => await _mediator.Send(command);

        [HttpPost("login")]
        public async Task<UserEnvelope> Login([FromBody] Login.Command command) => await _mediator.Send(command);
    }
}
