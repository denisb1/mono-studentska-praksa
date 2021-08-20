using System.Collections.Generic;
using System.Linq;
using Day5.Models.Common;

namespace Day5.Models
{
	public class CourseREST : ICourse
	{
		public string CourseName { get; set; }
		public string TeacherFirstName { get; set; }
		public string TeacherLastName { get; set; }
		public double? Ects { get; set; }

		private CourseREST(ICourse course)
		{
			CourseName = course.CourseName;
			TeacherFirstName = course.TeacherFirstName;
			TeacherLastName = course.TeacherLastName;
			Ects = course.Ects;
		}

		public static CourseREST InitializeCourse(ICourse course)
		{
			return new CourseREST(course);
		}

		public static List<ICourse> InitializeCourses(IEnumerable<ICourse> courses)
		{
			return courses?.Select(course => new CourseREST(course)).Cast<ICourse>().ToList();
		}
	}
}
