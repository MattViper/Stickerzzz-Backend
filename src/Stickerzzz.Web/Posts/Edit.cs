using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Entities;
using Stickerzzz.Infrastructure.Data;
using Stickerzzz.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Posts
{
    public class Edit
    {
        public class PostData
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string[] TagList { get; set; }
        }

        public class Command : IRequest<PostEnvelope>
        {
            public PostData Post { get; set; }
            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(i => i.Post).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, PostEnvelope>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<PostEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var post = await _context.Posts
                    .Include(i => i.PostStickers)
                        .ThenInclude(i => i.Sticker)
                            .ThenInclude(i => i.TagList)
                    .Where(x => x.Slug == message.Slug)
                    .FirstOrDefaultAsync(cancellationToken);

                if (post == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { Post = Constants.NOT_FOUND });
                }
                post.Content = message.Post.Content ?? post.Content;
                post.Title = message.Post.Title ?? post.Title;
                post.Slug = message.Post.Title.GenerateSlug();

                var postTagList = (message.Post.TagList ?? Enumerable.Empty<string>());

                var stickerTagsToCreate = GetStickerTagsToCreate(post.Stickers, postTagList);
                var stickerTagstoDelete = GetStickerTagsToDelete(post.Stickers, postTagList);

                if (_context.ChangeTracker.Entries().First(x => x.Entity == post).State == EntityState.Modified
                    || stickerTagsToCreate.Any() || stickerTagstoDelete.Any())
                {
                    post.LastModificationTime = DateTime.UtcNow;
                }

                await _context.TagStickers.AddRangeAsync(stickerTagsToCreate, cancellationToken);
                _context.TagStickers.RemoveRange(stickerTagstoDelete);

                await _context.SaveChangesAsync(cancellationToken);

                return new PostEnvelope(await _context.Posts.GetAllData()
                    .Where(i => i.Slug == post.Slug)
                    .FirstOrDefaultAsync(cancellationToken));

                          
            }

            private async Task<List<Tag>> GetTagsToCreate(IEnumerable<string> stickerTagList)
            {
                var tagsToCreate = new List<Tag>();

                foreach(var tag in stickerTagList)
                {
                    var t = await _context.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag()
                        {
                            TagId = tag
                        };
                        tagsToCreate.Add(t);
                    }
                }

                return tagsToCreate;
            }

            static List<TagStickers> GetStickerTagsToCreate(ICollection<Sticker> stickers, IEnumerable<string> stickerTagList)
            {
                var stickerTagsToCreate = new List<TagStickers>();
                foreach (var tag in stickerTagList)
                {
                    foreach (var sticker in stickers)
                    {
                        var ts = sticker.TagStickers.FirstOrDefault(t => t.TagId == tag);
                        if (ts == null)
                        {
                            ts = new TagStickers()
                            {
                                Sticker = sticker,
                                StickerId = sticker.Id,
                                Tag = new Tag() { TagId = tag },
                                TagId = tag
                            };
                            stickerTagsToCreate.Add(ts);
                        }
                    }


                }
                return stickerTagsToCreate;
            }

            static List<TagStickers> GetStickerTagsToDelete(ICollection<Sticker> stickers, IEnumerable<string> stickerTagList)
            {
                var stickerTagsToDelete = new List<TagStickers>();
                foreach (var sticker in stickers)
                {
                    foreach (var tag in sticker.TagStickers)
                    {
                        var ts = stickerTagList.FirstOrDefault(t => t == tag.TagId);
                        if (ts == null)
                        {
                            stickerTagsToDelete.Add(tag);
                        }
                    }
                }

                return stickerTagsToDelete;
            }
        }
    }
}
