using System.Collections.Generic;
using System.Linq;
using Day5.Models.Common;

namespace Day5.Models
{
	public class StudentREST : IStudent
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string College { get; set; }
		public int? CollegeYear { get; set; }
		public int? Age { get; set; }

		private StudentREST(IStudent student)
		{
			FirstName = student.FirstName;
			LastName = student.LastName;
			College = student.College;
			CollegeYear = student.CollegeYear;
			Age = student.Age;
		}

		public static StudentREST InitializeStudent(IStudent student)
		{
			return new StudentREST(student);
		}

		public static List<IStudent> InitializeStudents(IEnumerable<IStudent> students)
		{
			return students?.Select(student => new StudentREST(student)).Cast<IStudent>().ToList();
		}
	}
}
