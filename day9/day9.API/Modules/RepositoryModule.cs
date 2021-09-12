using Autofac;
using day9.Repository;
using day9.Repository.Common;

namespace day9.API.Modules
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
