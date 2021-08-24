using System;

namespace Day4.Models.Common
{
	public class EnrollmentDto
	{
		public Guid? StudentId { get; set; }
		public Guid? CourseId { get; set; }

		public EnrollmentDto(Guid? studentId, Guid? courseId)
		{
			StudentId = studentId;
			CourseId = courseId;
		}
	}
}
