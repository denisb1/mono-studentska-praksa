using System;
using System.ComponentModel.DataAnnotations;

namespace Day6.Models.DTO
{
	public class EnrollmentDto
	{
		public Guid Id { get; set; }
		public virtual StudentDto StudentDto { get; set; }
		public virtual CourseDto CourseDto { get; set; }

		[Required]
		public Guid StudentId { get; set; }

		[Required]
		public Guid CourseId { get; set; }
	}
}
