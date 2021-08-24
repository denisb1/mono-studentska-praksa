using Newtonsoft.Json;

namespace Day6.Models.REST
{
	public class TeacherRest
	{
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("Department")]
		public string Department { get; set; }
	}
}
