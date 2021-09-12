using Autofac;
using day9.API.Configurations;
using day9.DAL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace day9.API.Modules
{
	public class DalModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c =>
			{
				var config = c.Resolve<IConfiguration>();

				var opt = new DbContextOptionsBuilder<TestingContext>();
				opt.UseNpgsql(config.GetConnectionString("postgreSQL"));

				return new TestingContext(opt.Options);
			}).AsSelf().InstancePerLifetimeScope();

			builder.Register(c =>
			{
				var config = c.Resolve<IConfiguration>();

				var opt = new DbContextOptionsBuilder<DatabaseContext>();
				opt.UseNpgsql(config.GetConnectionString("postgreSQL"));

				return new DatabaseContext(opt.Options);
			}).AsSelf().InstancePerLifetimeScope();
		}
	}
}
