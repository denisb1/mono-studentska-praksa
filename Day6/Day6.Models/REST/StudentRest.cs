using Newtonsoft.Json;

namespace Day6.Models.REST
{
	public class StudentRest
	{
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("Year")]
		public int Year { get; set; }

		[JsonProperty("Age")]
		public int Age { get; set; }
	}
}
