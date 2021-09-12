using Newtonsoft.Json;

namespace day9.Model
{
	public class UpdateTeacherRest
	{
#nullable enable
		[JsonProperty("FirstName")]
		public string? FirstName { get; set; }

		[JsonProperty("LastName")]
		public string? LastName { get; set; }

		[JsonProperty("Department")]
		public string? Department { get; set; }
#nullable disable
	}
}
