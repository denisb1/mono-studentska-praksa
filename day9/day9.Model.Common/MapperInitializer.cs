using AutoMapper;
using day9.DAL;

namespace day9.Model.Common
{
	public class MapperInitializer : Profile
	{
		public MapperInitializer()
		{
			CreateMap<Course, CourseRest>().ReverseMap();
			CreateMap<CreateCourseRest, Course>().ReverseMap();
			CreateMap<UpdateCourseRest, Course>().ReverseMap();

			CreateMap<Student, StudentRest>().ReverseMap();
			CreateMap<CreateStudentRest, Student>().ReverseMap();
			CreateMap<UpdateStudentRest, Student>().ReverseMap();

			CreateMap<Teacher, TeacherRest>().ReverseMap();
			CreateMap<CreateTeacherRest, Teacher>().ReverseMap();
			CreateMap<UpdateTeacherRest, Teacher>().ReverseMap();

			CreateMap<StudentRest, Enrollment>()
				.ForMember(e => e.Student, opt =>
					opt.Ignore());
		}
	}
}
