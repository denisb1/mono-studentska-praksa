using Newtonsoft.Json;

namespace day9.Model
{
	public class CreateTeacherRest
	{
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }

		[JsonProperty("LastName")]
		public string LastName { get; set; }

		[JsonProperty("Department")]
		public string Department { get; set; }
	}
}
