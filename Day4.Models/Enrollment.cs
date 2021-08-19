using System;
using Newtonsoft.Json;

namespace Day4.Models
{
	public class Enrollment
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		[JsonProperty("StudentId")]
		public Guid? StudentId { get; set; }
		[JsonProperty("CourseId")]
		public Guid? CourseId { get; set; }

		public Enrollment(Guid? id, Guid? studentId, Guid? courseId)
		{
			Id = id;
			StudentId = studentId;
			CourseId = courseId;
		}
	}
}
