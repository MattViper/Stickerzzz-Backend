using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Infrastructure.Data;
using Stickerzzz.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Slug { get; set; }
            public Command(string slug)
            {
                Slug = slug;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Slug).NotNull().NotEmpty();
                }
            }

            public class QueryHandler : IRequestHandler<Command>
            {
                private readonly AppDbContext _context;

                public QueryHandler(AppDbContext context)
                {
                    _context = context;
                }

                public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
                {
                    var post = await _context.Posts
                        .FirstOrDefaultAsync(i => i.Slug == message.Slug, cancellationToken);

                    if (post == null)
                    {
                        throw new RestException(System.Net.HttpStatusCode.NotFound, new { Post = Constants.NOT_FOUND });
                    }

                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
            }
        }
    }
}
