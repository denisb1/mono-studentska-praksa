using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Day6.Common;
using Day6.DAL;
using Day6.Models.REST;
using Day6.Repository;
using Day6.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(o =>
			{
				o.AddPolicy("CorsPolicy", builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			});

			services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseNpgsql(DatabaseHelper.GetInstance().ConnectionString);
			});
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
			builder.RegisterType<RepositoryWork>().As<IRepositoryWork>();
			builder.RegisterType<StudentService>().As<IGenericService<StudentRest>>();
			builder.RegisterType<CourseService>().As<IGenericService<CourseRest>>();
			builder.RegisterType<TeacherService>().As<IGenericService<TeacherRest>>();
			builder.RegisterType<DatabaseContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
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
