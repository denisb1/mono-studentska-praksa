using System;
using Day5.Models.Common;
using Newtonsoft.Json;

namespace Day5.Models
{
	public class Enrollment : IEnrollment
	{
		[JsonProperty("Id")]
		public Guid? Id { get; set; }
		public Guid? StudentId { get; set; }
		public Guid? CourseId { get; set; }

		public Enrollment(Guid? id, Guid? studentId, Guid? courseId)
		{
			Id = id;
			StudentId = studentId;
			CourseId = courseId;
		}
	}
}
