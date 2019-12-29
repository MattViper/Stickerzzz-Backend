using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.Infrastructure.Data;
using Stickerzzz.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class Details
    {
        public class Query: IRequest<PostEnvelope>
        {
            public string Slug { get; set; }

            public Query(string slug)
            {
                Slug = slug;
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(i => i.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, PostEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;


            public QueryHandler(AppDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<PostEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.GetAllData()
                    .FirstOrDefaultAsync(i => i.Slug == message.Slug, cancellationToken);

                if (post == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Post = Constants.NOT_FOUND });
                }

                var foundPost = _mapper.Map<Post, PostVM>(post);

                return new PostEnvelope(foundPost);
            }
        }
    }
}
