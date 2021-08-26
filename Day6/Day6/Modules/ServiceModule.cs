using Autofac;
using Day6.Models.REST;
using Day6.Service;

namespace Day6.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<StudentService>()
				.AsSelf()
				.As<IGenericService<StudentRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
			builder.RegisterType<CourseService>()
				.AsSelf()
				.As<IGenericService<CourseRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
			builder.RegisterType<TeacherService>()
				.AsSelf()
				.As<IGenericService<TeacherRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
