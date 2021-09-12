using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace day9.Model
{
	public class TeacherRest
	{
		[JsonProperty("Id")]
		public Guid Id { get; set; }

		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("Department")]
		public string Department { get; set; }

		public IList<CourseRest> Courses { get; set; } = new List<CourseRest>();
	}
}
