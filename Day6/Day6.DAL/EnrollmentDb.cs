using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day6.DAL
{
	public sealed class EnrollmentDb
	{
		public Guid Id { get; set; }

		[ForeignKey(nameof(StudentDb))]
		public Guid StudentId { get; set; }
		public StudentDb StudentDb { get; set; }

		[ForeignKey(nameof(CourseDb))]
		public Guid CourseId { get; set; }
		public CourseDb CourseDb { get; set; }
	}
}
