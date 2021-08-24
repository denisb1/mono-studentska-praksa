using System;

namespace Day3.Models
{
	public class Student
	{
		public Guid Id { get; }
		public string FName { get; }
		public string LName { get; }
		public string College { get; }
		public int Age { get; }
		public int CYear { get; }

		public Student(StudentDto studentDto)
		{
			Id = Guid.NewGuid();
			FName = studentDto.FName;
			LName = studentDto.LName;
			College = studentDto.College;
			Age = studentDto.Age;
			CYear = studentDto.Age;
		}

		public Student(Guid id, StudentDto studentDto)
		{
			Id = id;
			FName = studentDto.FName;
			LName = studentDto.LName;
			College = studentDto.College;
			Age = studentDto.Age;
			CYear = studentDto.Age;
		}
	}
}
