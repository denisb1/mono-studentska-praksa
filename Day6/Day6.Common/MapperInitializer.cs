using AutoMapper;
using Day6.DAL;
using Day6.Models.DTO;
using Day6.Models.REST;

namespace Day6.Common
{
	public class MapperInitializer : Profile
	{
		public MapperInitializer()
		{
			CreateMap<CourseDb, CourseDto>().ReverseMap();
			CreateMap<CourseDto, CourseRest>().ReverseMap();
			CreateMap<CourseDb, CourseRest>().ReverseMap();

			CreateMap<StudentDb, StudentDto>().ReverseMap();
			CreateMap<StudentDto, StudentRest>().ReverseMap();
			CreateMap<StudentDb, StudentRest>().ReverseMap();

			CreateMap<TeacherDb, TeacherDto>().ReverseMap();
			CreateMap<TeacherDto, TeacherRest>().ReverseMap();
			CreateMap<TeacherDb, TeacherRest>().ReverseMap();
		}
	}
}
