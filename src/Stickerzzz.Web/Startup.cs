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
using Stickerzzz.Web.Helpers;
using Stickerzzz.Web.Services.Interfaces;
using Stickerzzz.Web.Services;
using FluentValidation.WebApi;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

			services.AddControllersWithViews().AddNewtonsoftJson().AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
            });
			services.AddRazorPages();

			//ApiBehaviorOptions ModelState Middleware

			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stickerzzz API", Version = "v1" }));

            services.AddScoped<IPostsService, PostsService>();

			return ContainerSetup.InitializeWeb(Assembly.GetExecutingAssembly(), services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.EnvironmentName == "Development")
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseRouting();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stickerzzz API V1"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapRazorPages();
			});
		}
	}
}
