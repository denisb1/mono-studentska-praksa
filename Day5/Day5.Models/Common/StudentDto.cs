namespace Day5.Models.Common
{
	public class StudentDto
	{
		public string FirstName { get; }
		public string LastName { get; }
		public string College { get; }
		public int? CollegeYear { get; }
		public int? Age { get; }

		public StudentDto(string firstName, string lastName, string college, int? collegeYear, int? age)
		{
			FirstName = firstName;
			LastName = lastName;
			College = college;
			CollegeYear = collegeYear;
			Age = age;
		}
	}
}
