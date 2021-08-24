using System;

namespace Day3.Models
{
	public class Course
	{
		public Guid Id { get; }
		public string Name { get; }

		public Course(CourseDto courseDto)
		{
			Id = Guid.NewGuid();
			Name = courseDto.Name;
		}

		public Course(Guid id, CourseDto courseDto)
		{
			Id = id;
			Name = courseDto.Name;
		}
	}
}
