using Autofac;
using AutoMapper;
using day9.API.Modules;
using day9.Model.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace day9.API
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
			services.AddCors(c => c.AddPolicy("AllowReact", b =>
				b.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "day9", Version = "v1" });
			});
			services.AddControllers().AddNewtonsoftJson(op =>
				op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.Register(c => Configuration).As<IConfiguration>().SingleInstance();

			builder.RegisterModule<DalModule>();
			builder.RegisterModule<RepositoryModule>();
			builder.RegisterModule<ServiceModule>();

			builder.Register(c => new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<MapperInitializer>();
				}
			)).As<AutoMapper.IConfigurationProvider>().SingleInstance();

			builder.Register(c =>
				new Mapper(c.Resolve<AutoMapper.IConfigurationProvider>())).As<IMapper>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "day9 v1"));
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowReact");
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
