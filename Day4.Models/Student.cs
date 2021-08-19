using System;
using Day4.Models.Common;
using Newtonsoft.Json;

namespace Day4.Models
{
	public class Student
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		[JsonProperty("FirstName")]
		public string FirstName { get; set; }
		[JsonProperty("LastName")]
		public string LastName { get; set; }
		[JsonProperty("College")]
		public string College { get; set; }
		[JsonProperty("CollegeYear")]
		public int? CollegeYear { get; set; }
		[JsonProperty("Age")]
		public int? Age { get; set; }

		public Student(Guid? id, Name studentName, string college, int? year, int? age)
		{
			Id = id;
			FirstName = studentName.First;
			LastName = studentName.Last;
			College = college;
			CollegeYear = year;
			Age = age;
		}
	}
}
