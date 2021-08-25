using Newtonsoft.Json;

namespace Day6.Models.REST
{
	public class EnrollmentRest
	{
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("CourseName")]
		public string CourseName { get; set; }

		[JsonProperty("Ects")]
		public double Ects { get; set; }
	}
}
