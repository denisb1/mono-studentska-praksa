using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Day6.Common;
using Day6.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Day6
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; private set; }
		public ILifetimeScope AutofacContainer { get; private set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddControllersAsServices();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Day6", Version = "v1" });
			});

			services.AddControllers().AddNewtonsoftJson(op =>
				op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

			services.AddOptions();
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new DalModule());
			builder.RegisterModule(new RepositoryModule());
			builder.RegisterModule(new ServiceModule());

			builder.RegisterAutoMapper(typeof(MapperInitializer).Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Day6 v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
