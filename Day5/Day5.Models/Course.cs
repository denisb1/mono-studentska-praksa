using System;
using Day5.Models.Common;

namespace Day5.Models
{
	public class Course
	{
		public Guid? Id { get; set; }
		public string CourseName { get; set; }
		public string TeacherFirstName { get; set; }
		public string TeacherLastName { get; set; }
		public double? Ects { get; set; }

		public Course(Guid? id, CourseDto courseDto)
		{
			Id = id;
			CourseName = courseDto.CourseName;
			TeacherFirstName = courseDto.TeacherFirstName;
			TeacherLastName = courseDto.TeacherLastName;
			Ects = courseDto.Ects;
		}
	}
}
