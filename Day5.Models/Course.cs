using System;
using Day5.Models.Common;
using Newtonsoft.Json;

namespace Day5.Models
{
	public class Course : ICourse
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		public string CourseName { get; set; }
		public string TeacherFirstName { get; set; }
		public string TeacherLastName { get; set; }
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
