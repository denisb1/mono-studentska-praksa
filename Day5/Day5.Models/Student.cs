using System;
using Day5.Models.Common;

namespace Day5.Models
{
	public class Student
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
		public int? Age { get; set; }

		public Student(Guid? id, StudentDto studentDto)
		{
			Id = id;
			FirstName = studentDto.FirstName;
			LastName = studentDto.LastName;
			College = studentDto.College;
			CollegeYear = studentDto.CollegeYear;
			Age = studentDto.Age;
		}
	}
}
