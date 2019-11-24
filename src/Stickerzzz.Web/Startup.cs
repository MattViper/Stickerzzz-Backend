using Stickerzzz.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using AutoMapper;
using FluentValidation.WebApi;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Stickerzzz.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Stickerzzz.Web
{
	public class Startup
	{
		public Startup(IConfiguration config) => this.Configuration = config;

		public IConfiguration Configuration { get; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext();
			//var mappingConfig = new MapperConfiguration(mc =>
			//{
			//    mc.AddProfile(new MappingProfile());
			//});

			//IMapper mapper = mappingConfig.CreateMapper();
			//services.AddSingleton(mapper);
			services.AddCors();
			services.AddControllersWithViews().AddNewtonsoftJson().AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
            });
			services.AddRazorPages();

			//ModelState Validation Middleware

			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stickerzzz API", Version = "v1" }));

			//services.AddScoped<IPostsService, PostsService>();
			services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DBContextTransactionPipelineBehavior<,>));
			services.AddJwt();
			return ContainerSetup.InitializeWeb(Assembly.GetExecutingAssembly(), services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{

			loggerFactory.AddSerilogLogging();

			if (env.EnvironmentName == "Development")
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseCors(builder =>
				builder
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod());

			app.UseRouting();
			app.UseAuthorization();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stickerzzz API V1");
				c.RoutePrefix = string.Empty;
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapRazorPages();
			});
		}
	}
}
