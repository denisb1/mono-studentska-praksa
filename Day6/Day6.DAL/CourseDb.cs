using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day6.DAL
{
	public sealed class CourseDb
	{
		public Guid Id { get; set; }
		public string CourseName { get; set; }
		public double Ects { get; set; }

		[ForeignKey(nameof(TeacherDb))]
		public Guid TeacherId { get; set; }
		public TeacherDb TeacherDb { get; set; }

		public IList<EnrollmentDb> EnrollmentDbs { get; set; }
	}
}
