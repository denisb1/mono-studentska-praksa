using Newtonsoft.Json;

namespace Day5.Models.Common
{
	public interface ICourse
	{
		[JsonProperty("CourseName")]
		public string CourseName { get; set; }
		[JsonProperty("TeacherFirstName")]
		public string TeacherFirstName { get; set; }
		[JsonProperty("TeacherLastName")]
		public string TeacherLastName { get; set; }
		[JsonProperty("Ects")]
		public double? Ects { get; set; }
	}
}
