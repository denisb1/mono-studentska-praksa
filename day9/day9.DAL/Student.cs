using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace day9.DAL
{
	public sealed class Student
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Year { get; set; }
		public int Age { get; set; }

		public IList<Enrollment> Enrollments { get; set; }
	}
}
