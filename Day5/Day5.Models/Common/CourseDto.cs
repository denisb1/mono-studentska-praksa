namespace Day5.Models.Common
{
	public class CourseDto
	{
		public string CourseName { get; }
		public string TeacherFirstName { get; }
		public string TeacherLastName { get; }
		public double? Ects { get; }

		public CourseDto(string courseName, string teacherFirstName, string teacherLastName, double? ects)
		{
			CourseName = courseName;
			TeacherFirstName = teacherFirstName;
			TeacherLastName = teacherLastName;
			Ects = ects;
		}
	}
}
