namespace Day3.Models
{
	public class Student
	{
		public string FName { get; }
		public string LName { get; }
		public string College { get; }
		public int Age { get; }
		public int CYear { get; }

		public Student(string fname, string lname, string college, int age, int year)
		{
			FName = fname;
			LName = lname;
			College = college;
			Age = age;
			CYear = year;
		}
	}
}
