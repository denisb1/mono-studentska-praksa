using System;

namespace Day5.Models.Common
{
	public class EnrollmentDto
	{
		public Guid? StudentId { get; }
		public Guid? CourseId { get; }

		public EnrollmentDto(Guid? studentId, Guid? courseId)
		{
			StudentId = studentId;
			CourseId = courseId;
		}
	}
}
