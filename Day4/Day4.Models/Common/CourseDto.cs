using System.Text.Json.Serialization;

namespace Day4.Models.Common
{
	public class CourseDto
	{
		[JsonPropertyName("CourseName")]
		public string CourseName { get; set; }
		[JsonPropertyName("TeacherFirstName")]
		public string TeacherFirstName { get; set; }
		[JsonPropertyName("TeacherLastName")]
		public string TeacherLastName { get; set; }
		[JsonPropertyName("Ects")]
		public double? Ects { get; set; }

		public CourseDto(string courseName, string teacherFirstName, string teacherLastName, double? ects)
		{
			CourseName = courseName;
			TeacherFirstName = teacherFirstName;
			TeacherLastName = teacherLastName;
			Ects = ects;
		}
	}
}
