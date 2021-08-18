namespace Day3.Models
{
	public class Enrollment
	{
		public System.Guid StudentID { get; }
		public System.Guid CourseID { get; }

		public Enrollment(System.Guid studentId, System.Guid courseId)
		{
			StudentID = studentId;
			CourseID = courseId;
		}
	}
}
