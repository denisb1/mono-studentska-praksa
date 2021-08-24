using System;
using System.Collections.Generic;

namespace Day6.DAL
{
	public sealed class StudentDb
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Year { get; set; }
		public int Age { get; set; }

		public IList<EnrollmentDb> EnrollmentDbs { get; set; }
	}
}
