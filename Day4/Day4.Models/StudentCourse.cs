namespace Day4.Models
{
	public class StudentCourse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
		public string CourseName { get; set; }
		public double? Ects { get; set; }

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
