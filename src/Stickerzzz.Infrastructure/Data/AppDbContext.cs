using Stickerzzz.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Stickerzzz.Core.SharedKernel;
using Ardalis.EFCore.Extensions;
using System.Reflection;
using JetBrains.Annotations;

namespace Stickerzzz.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}