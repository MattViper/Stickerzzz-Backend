using Stickerzzz.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Stickerzzz.Core.SharedKernel;
using Ardalis.EFCore.Extensions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stickerzzz.Core.Users;
using Microsoft.AspNetCore.Identity;
using System;
using Stickerzzz.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace Stickerzzz.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        private IDbContextTransaction _currentTransaction;

        public DbSet<Sticker> Stickers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostStickers> PostStickers { get; set; }
        public DbSet<UserStickers> UserStickers { get; set; }
        public DbSet<PostFavorites> PostFavorites { get; set;}
        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<TagStickers> TagStickers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<PostStickers>()
                .HasKey(x => new { x.PostId, x.StickerId });

            modelBuilder.Entity<PostStickers>()
                .HasOne(p => p.Post)
                .WithMany(s => s.PostStickers)
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostStickers>()
                .HasOne(s => s.Sticker)
                .WithMany(p => p.PostStickers)
                .HasForeignKey(s => s.StickerId);

            modelBuilder.Entity<PostFavorites>()
                .HasKey(x => new { x.PostId, x.UserId });

            modelBuilder.Entity<PostFavorites>()
                .HasOne(p => p.Post)
                .WithMany(f => f.PostFavorites)
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostFavorites>()
                .HasOne(f => f.User)
                .WithMany(p => p.PostFavorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<UserStickers>()
                .HasKey(x => new { x.UserId, x.StickerId });

            modelBuilder.Entity<UserStickers>()
                .HasOne(u => u.User)
                .WithMany(s => s.UserStickers)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserStickers>()
                .HasOne(s => s.Sticker)
                .WithMany(u => u.UserStickers)
                .HasForeignKey(s => s.StickerId);

            modelBuilder.Entity<TagStickers>().HasKey(i => new { i.TagId, i.StickerId });

            modelBuilder.Entity<TagStickers>()
                .HasOne(t => t.Tag)
                .WithMany(ts => ts.PostTags)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<TagStickers>()
                .HasOne(s => s.Sticker)
                .WithMany(ts => ts.TagStickers)
                .HasForeignKey(s => s.StickerId);


            modelBuilder.Entity<Friendship>().HasKey(i => new { i.ApplicationUserId, i.FriendId });

            modelBuilder.Entity<Friendship>().HasOne(e => e.ActionUser).WithOne().HasForeignKey<Friendship>(e => e.ActionUserId);


            modelBuilder.Entity<Friendship>()
               .HasOne(x => x.ApplicationUser)
               .WithMany(y => y.FriendRequestsMade)
               .HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(x => x.Friend)
                .WithMany(y => y.FriendRequestsAccepted)
                .HasForeignKey(x => x.FriendId);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();



        }

        #region Transactions

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = Database.BeginTransaction();

        }

        public void CommitTransaction()
        {
            try
            {
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion
    }
}