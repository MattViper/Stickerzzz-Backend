using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class Create
    {
        public class PostData
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public List<StickerData> StickersData { get; set; }
        }

        public class StickerData
        {
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string Img { get; set; }
            public List<string> TagList { get; set; }
        }

        public class PostDataValidator : AbstractValidator<PostData>
        {
            public PostDataValidator()
            {
                RuleFor(i => i.Title).NotNull().NotEmpty();
                RuleFor(i => i.Content).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<PostEnvelope>
        {
            public PostData Post { get; set; }
        }
        
        public class CommandValidator: AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(i => i.Post).NotNull().SetValidator(new PostDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, PostEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(AppDbContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<PostEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var creator = await _context.Users.FirstAsync(i => i.UserName == _currentUserAccessor.GetCurrentUsername(), cancellationToken);
                var tags = new List<Tag>();
                var stickers = new List<Sticker>();
                

                foreach (var sticker in message.Post.StickersData)
                {
                    var s = new Sticker()
                    {
                        Name = sticker.Name,
                        Longitude = sticker.Longitude,
                        Latitude = sticker.Latitude,
                        Img = sticker.Img
                    };
                    stickers.Add(s);

                    foreach (var tag in sticker.TagList ?? Enumerable.Empty<string>())
                    {
                        var t = await _context.Tags.FindAsync(tag);
                        if (t == null)
                        {
                            t = new Tag()
                            {
                                TagId = tag
                            };
                            await _context.Tags.AddAsync(t, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        tags.Add(t);
                    }
                    
                }

                var post = new Post()
                {
                    Creator = creator,
                    Content = message.Post.Content,
                    LastModificationTime = DateTime.UtcNow,     //later change models and add CreatedAt too
                    Title = message.Post.Title,
                    Slug = message.Post.Title.GenerateSlug(),
                    Stickers = stickers,
                    
                };
                await _context.Posts.AddAsync(post, cancellationToken);
                await _context.Stickers.AddRangeAsync(stickers);

                foreach (var sticker in post.Stickers)
                {
                    await _context.TagStickers.AddRangeAsync(tags.Select(x => new TagStickers()
                    {
                        Tag = x,
                        Sticker = sticker
                    }));

                    await _context.PostStickers.AddRangeAsync(new PostStickers()
                    {
                        Post = post,
                        Sticker = sticker
                    });
                }

                await _context.SaveChangesAsync();

                return new PostEnvelope(post);

            }
        }
    }
}
