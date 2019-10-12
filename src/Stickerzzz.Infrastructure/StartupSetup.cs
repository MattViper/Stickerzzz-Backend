using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Stickerzzz.Infrastructure.Data;
using System;

namespace Stickerzzz.Infrastructure
{
	public static class StartupSetup
	{
        public static void AddDbContext(this IServiceCollection services) =>
            services.AddDbContextPool<AppDbContext>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql("Server=localhost;Database=db;User=root;Password=skrzypak3;", // replace with your Connection String
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql); // replace with your Server Version and Type
                    }));

    }
}
