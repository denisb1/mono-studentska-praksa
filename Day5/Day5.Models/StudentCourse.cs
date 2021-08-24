namespace Day5.Models
{
	public class StudentCourse
	{
		public string FirstName { get; }
		public string LastName { get; }
		public string College { get; }
		public int? CollegeYear { get; }
		public string CourseName { get; }
		public double? Ects { get; }

		public StudentCourse(string firstName, string lastName, string college, int? collegeYear, string courseName, double? ects)
		{
			FirstName = firstName;
			LastName = lastName;
			College = college;
			CollegeYear = collegeYear;
			CourseName = courseName;
			Ects = ects;
		}
	}
}
