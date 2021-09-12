using Newtonsoft.Json;

namespace day9.Model
{
	public class UpdateStudentRest
	{
#nullable enable
		[JsonProperty("FirstName")]
		public string? FirstName { get; set; }

		[JsonProperty("LastName")]
		public string? LastName { get; set; }

		[JsonProperty("Year")]
		public int? Year { get; set; }

		[JsonProperty("Age")]
		public int? Age { get; set; }
#nullable disable
	}
}
