using System;
using System.Collections.Generic;
using System.Linq;
using Day5.Models.Common;

namespace Day5.Models
{
	public class EnrollmentREST : IEnrollment
	{
		public Guid? StudentId { get; set; }
		public Guid? CourseId { get; set; }

		private EnrollmentREST(IEnrollment enrollment)
		{
			StudentId = enrollment.StudentId;
			CourseId = enrollment.CourseId;
		}

		public static EnrollmentREST InitializeEnrollment(IEnrollment enrollment)
		{
			return new EnrollmentREST(enrollment);
		}

		public static List<IEnrollment> InitializeEnrollments(IEnumerable<IEnrollment> enrollments)
		{
			return enrollments?.Select(enrollment => new EnrollmentREST(enrollment)).Cast<IEnrollment>().ToList();
		}
	}
}
