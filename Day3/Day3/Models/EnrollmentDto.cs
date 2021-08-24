using System;

namespace Day3.Models
{
	public class EnrollmentDto
	{
		public Guid StudentId { get; }
		public Guid CourseId { get; }

		public EnrollmentDto(Guid studentId, Guid courseId)
		{
			StudentId = studentId;
			CourseId = courseId;
		}

	}
}
