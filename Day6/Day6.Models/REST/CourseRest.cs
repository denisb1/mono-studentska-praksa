using Newtonsoft.Json;

namespace Day6.Models.REST
{
	public class CourseRest
	{
		[JsonProperty("CourseName")]
		public string CourseName { get; set; }

		[JsonProperty("Ects")]
		public double Ects { get; set; }
	}
}
