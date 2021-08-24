using AutoMapper;
using Day6.DAL;
using Day6.Models.DTO;
using Day6.Models.REST;

namespace Day6.Repository
{
	public class MapperInitializer : Profile
	{
		public MapperInitializer()
		{
			CreateMap<CourseDb, CourseDto>().ReverseMap();
			CreateMap<CourseDb, CourseRest>().ReverseMap();
			CreateMap<EnrollmentDb, EnrollmentDto>().ReverseMap();
			CreateMap<StudentDb, StudentDto>().ReverseMap();
			CreateMap<StudentDb, StudentRest>().ReverseMap();
			CreateMap<TeacherDb, TeacherDto>().ReverseMap();
			CreateMap<TeacherDb, TeacherRest>().ReverseMap();
		}
	}
}
