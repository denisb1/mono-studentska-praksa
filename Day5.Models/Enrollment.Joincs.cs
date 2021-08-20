using System;
using Day5.Models.Common;
using Newtonsoft.Json;

namespace Day5.Models
{
	public class EnrollmentJoin : IEnrollment, IStudent, ICourse
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		public Guid? StudentId { get; set; }
		public Guid? CourseId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
		public int? Age { get; set; }
		public string CourseName { get; set; }
		public string TeacherFirstName { get; set; }
		public string TeacherLastName { get; set; }
		public double? Ects { get; set; }

		public EnrollmentJoin(Enrollment enrollment, Student student, Course course)
		{
			Id = enrollment.Id;
			StudentId = enrollment.StudentId;
			CourseId = enrollment.CourseId;
			FirstName = student.FirstName;
			LastName = student.LastName;
			College = student.College;
			CollegeYear = student.CollegeYear;
			Age = student.Age;
			CourseName = course.CourseName;
			TeacherFirstName = course.TeacherFirstName;
			TeacherLastName = course.TeacherLastName;
			Ects = course.Ects;
		}
	}
}
