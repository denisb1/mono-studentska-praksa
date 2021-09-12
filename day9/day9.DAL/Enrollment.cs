using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace day9.DAL
{
	public sealed class Enrollment
	{
		public Guid Id { get; set; }

		[ForeignKey(nameof(Student))]
		public Guid StudentId { get; set; }
		public Student Student { get; set; }

		[ForeignKey(nameof(Course))]
		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}
