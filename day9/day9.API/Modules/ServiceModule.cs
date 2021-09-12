using Autofac;
using day9.Model;
using day9.Service;
using day9.Service.Common;

namespace day9.API.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<StudentService>()
				.AsSelf()
				.As<IGenericService<StudentRest, CreateStudentRest, UpdateStudentRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
			builder.RegisterType<CourseService>()
				.AsSelf()
				.As<IGenericService<CourseRest, CreateCourseRest, UpdateCourseRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
			builder.RegisterType<TeacherService>()
				.AsSelf()
				.As<IGenericService<TeacherRest, CreateTeacherRest, UpdateTeacherRest>>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
