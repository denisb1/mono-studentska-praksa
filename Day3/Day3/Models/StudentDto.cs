namespace Day3.Models
{
	public class StudentDto
	{
		public string FName { get; }
		public string LName { get; }
		public string College { get; }
		public int Age { get; }
		public int CYear { get; }

		public StudentDto(string fName, string lName, string college, int age, int cYear)
		{
			FName = fName;
			LName = lName;
			College = college;
			Age = age;
			CYear = cYear;
		}
	}
}
