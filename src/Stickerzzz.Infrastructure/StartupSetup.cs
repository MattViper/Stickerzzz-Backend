//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//using Stickerzzz.Infrastructure.Data;
//using System;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;

//namespace Stickerzzz.Infrastructure
//{
//    public static class StartupSetup
//    {
//        static readonly string name = Dns.GetHostName(); // get container id
//        static readonly IPAddress ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
//        static readonly string db = "Stickerzzz";
//        public static void AddDbContext(this IServiceCollection services) =>
//            services.AddDbContextPool<AppDbContext>(
//                options => options.UseNpgsql("Host=host.docker.internal;Port=5432;Username=postgres;Password=NoFearNoMore12;Database=Stickerzzz;Command Timeout=0"));

//    }
//}
