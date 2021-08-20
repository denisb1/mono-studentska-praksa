using System;
using Day5.Models.Common;
using Newtonsoft.Json;

namespace Day5.Models
{
	public class Student : IStudent
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
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
