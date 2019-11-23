using FluentValidation;
using Stickerzzz.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Validators
{
    public class PostValidator : AbstractValidator<PostSpec>
    {
        public PostValidator()
        {
            RuleFor(post => post.Content).NotEmpty().WithMessage("Post content cannot be empty!");
            RuleFor(post => post.Stickers).NotNull().WithMessage("Post must have at least one sticker!");
        }
    }
}
