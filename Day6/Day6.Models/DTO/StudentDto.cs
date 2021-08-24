using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day6.Models.DTO
{
	public class StudentDto
	{
		public Guid Id { get; set; }

		public IList<EnrollmentDto> EnrollmentDtos { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public int Year { get; set; }

		[Required]
		public int Age { get; set; }
	}
}
