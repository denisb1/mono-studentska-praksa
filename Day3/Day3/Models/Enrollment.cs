using System;

namespace Day3.Models
{
	public class Enrollment
	{
		public Guid Id { get; }
		public Guid StudentId { get; }
		public Guid CourseId { get; }

		public Enrollment(EnrollmentDto enrollmentDto)
		{
			Id = Guid.NewGuid();
			StudentId = enrollmentDto.StudentId;
			CourseId = enrollmentDto.CourseId;
		}

		public Enrollment(Guid id, EnrollmentDto enrollmentDto)
		{
			Id = id;
			StudentId = enrollmentDto.StudentId;
			CourseId = enrollmentDto.CourseId;
		}
	}
}
