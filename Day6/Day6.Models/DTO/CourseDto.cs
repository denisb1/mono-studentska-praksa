using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day6.Models.DTO
{
	public class CourseDto
	{
		public Guid Id { get; set; }
		public TeacherDto TeacherDto { get; set; }
		public IList<EnrollmentDto> EnrollmentDtos { get; set; }

		[Required]
		public string CourseName { get; set; }

		[Required]
		public double Ects { get; set; }

		[Required]
		public Guid TeacherId { get; set; }
	}
}
