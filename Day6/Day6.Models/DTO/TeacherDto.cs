using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day6.Models.DTO
{
	public class TeacherDto
	{
		public Guid Id { get; set; }
		public IList<CourseDto> CourseDtos { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Department { get; set; }
	}
}
