using System;
using System.Collections.Generic;
using day9.DAL;
using Newtonsoft.Json;

namespace day9.Model
{
	public class StudentRest
	{
		[JsonProperty("Id")]
		public Guid Id { get; set; }

		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("Year")]
		public int Year { get; set; }

		[JsonProperty("Age")]
		public int Age { get; set; }

		public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
	}
}
