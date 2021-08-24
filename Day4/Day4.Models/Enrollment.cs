using System;
using Day4.Models.Common;

namespace Day4.Models
{
	public class Enrollment
	{
		public Guid? Id { get; set; }
		public Guid? StudentId { get; set; }
		public Guid? CourseId { get; set; }

		public Enrollment(Guid? id, EnrollmentDto enrollmentDto)
		{
			Id = id;
			StudentId = enrollmentDto.StudentId;
			CourseId = enrollmentDto.CourseId;
		}
	}
}
