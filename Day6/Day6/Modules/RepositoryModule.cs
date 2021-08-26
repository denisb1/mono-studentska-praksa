using Autofac;
using Day6.Repository;

namespace Day6.Modules
{
	public class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<RepositoryWork>()
				.AsSelf()
				.As<IRepositoryWork>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
