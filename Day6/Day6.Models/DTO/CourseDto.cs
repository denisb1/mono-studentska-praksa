using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day6.Models.DTO
{
	public class CourseDto
	{
		public Guid Id { get; set; }
		public virtual TeacherDto TeacherDto { get; set; }
		public virtual IList<StudentDto> StudentDtos { get; set; }

		[Required]
		public string CourseName { get; set; }

		[Required]
		public double Ects { get; set; }

		[Required]
		public Guid TeacherId { get; set; }
	}
}
