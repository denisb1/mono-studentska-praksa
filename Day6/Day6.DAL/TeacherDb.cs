using System;
using System.Collections.Generic;

namespace Day6.DAL
{
	public sealed class TeacherDb
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Department { get; set; }

		public IList<CourseDb> CourseDbs { get; set; }
	}
}
