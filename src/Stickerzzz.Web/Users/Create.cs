using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stickerzzz.Core.Users;
using Stickerzzz.Infrastructure.Data;
using Stickerzzz.Infrastructure.Errors;
using Stickerzzz.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Users
{
    public class Create
    {
        public class UserData
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserDataValidator : AbstractValidator<UserData>
        {
            public UserDataValidator()
            {
                RuleFor(x => x.UserName).NotNull().NotEmpty();
                RuleFor(x => x.Email).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<UserEnvelope>
        {
            public UserData User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull().SetValidator(new UserDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IMapper _mapper;

            public Handler(
                AppDbContext context,
                IPasswordHasher passwordHasher,
                IJwtTokenGenerator jwtTokenGenerator,
                IMapper mapper)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _jwtTokenGenerator = jwtTokenGenerator;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(i => i.UserName == message.User.UserName).AnyAsync(cancellationToken))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = Constants.IN_USE });
                }

                if (await _context.Users.Where(i => i.Email == message.User.Email).AnyAsync(cancellationToken))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = Constants.IN_USE });
                }

                var salt = Guid.NewGuid().ToByteArray();
                var appUser = new AppUser
                {
                    UserName = message.User.UserName,
                    Email = message.User.Email,
                    Hash = _passwordHasher.Hash(message.User.Password, salt),
                    Salt = salt
                };

                _context.Users.Add(appUser);
                await _context.SaveChangesAsync(cancellationToken);
                var user = _mapper.Map<AppUser, User>(appUser);
                user.Token = await _jwtTokenGenerator.CreateToken(appUser.UserName);  

                return new UserEnvelope(user);
            }
        }
    }
}
