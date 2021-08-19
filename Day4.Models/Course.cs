using System;
using Day4.Models.Common;
using Newtonsoft.Json;

namespace Day4.Models
{
	public class Course
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }

		[JsonProperty("CourseName")]
		public string CourseName { get; set; }
		[JsonProperty("TeacherFirstName")]
		public string TeacherFirstName { get; set; }
		[JsonProperty("TeacherLastName")]
		public string TeacherLastName { get; set; }
		[JsonProperty("Ects")]
		public double? Ects { get; set; }

		public Course(Guid? id, string courseName, Name teacherName, double? ects)
		{
			Id = id;
			CourseName = courseName;
			TeacherFirstName = teacherName.First;
			TeacherLastName = teacherName.Last;
			Ects = ects;
		}
	}
}
