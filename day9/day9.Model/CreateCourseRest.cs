using System;
using Newtonsoft.Json;

namespace day9.Model
{
	public class CreateCourseRest
	{
		[JsonProperty("CourseName")]
		public string CourseName { get; set; }

		[JsonProperty("Ects")]
		public decimal Ects { get; set; }

		[JsonProperty("TeacherId")]
		public Guid TeacherId { get; set; }
	}
}
