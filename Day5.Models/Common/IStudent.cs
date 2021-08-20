using Newtonsoft.Json;

namespace Day5.Models.Common
{
	public interface IStudent
	{
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }
		[JsonProperty("LastName")]
		public string LastName { get; set; }
		[JsonProperty("College")]
		public string College { get; set; }
		[JsonProperty("CollegeYear")]
		public int? CollegeYear { get; set; }
		[JsonProperty("Age")]
		public int? Age { get; set; }
	}
}
