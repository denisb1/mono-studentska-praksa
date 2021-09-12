using System;
using System.Collections.Generic;

namespace day9.DAL
{
	public sealed class Teacher
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Department { get; set; }

		public IList<Course> Courses { get; set; }
	}
}
