using System;
using Newtonsoft.Json;

namespace Day5.Models.Common
{
	public interface IEnrollment
	{
		[JsonProperty("StudentId")]
		public Guid? StudentId { get; set; }
		[JsonProperty("CourseId")]
		public Guid? CourseId { get; set; }
	}
}
