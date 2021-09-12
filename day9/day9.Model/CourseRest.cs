using System;
using Newtonsoft.Json;

namespace day9.Model
{
	public class CourseRest
	{
		[JsonProperty("Id")]
		public Guid Id { get; set; }

		[JsonProperty("CourseName")]
		public string CourseName { get; set; }

		[JsonProperty("Ects")]
		public decimal Ects { get; set; }

		[JsonProperty("TeacherId")]
		public Guid TeacherId { get; set; }
	}
}
