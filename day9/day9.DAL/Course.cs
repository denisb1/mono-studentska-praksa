using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace day9.DAL
{
	public sealed class Course
	{
		public Guid Id { get; set; }
		public string CourseName { get; set; }
		public decimal Ects { get; set; }

		[ForeignKey(nameof(Teacher))]
		public Guid TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public IList<Enrollment> Enrollments { get; set; }
	}
}
