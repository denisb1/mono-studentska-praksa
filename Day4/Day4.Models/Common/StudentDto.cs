namespace Day4.Models
{
	public class StudentDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
		public int? Age { get; set; }

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
