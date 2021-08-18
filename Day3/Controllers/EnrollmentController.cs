using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day3.Models;
using Day3.Database;

namespace Day3.Controllers
{
	public class EnrollmentController : ApiController
	{
		[HttpGet]
		public List<List<string>> Get()
		{
			return EnrollmentDatabase.GetAll();
		}

		[HttpGet]
		public IEnumerable<string> Get(System.Guid id)
		{
			return EnrollmentDatabase.Get(id);
		}

		[HttpPost]
		public HttpResponseMessage Post(System.Guid studentId, System.Guid courseId)
		{
			EnrollmentDatabase.Add(new Enrollment(studentId, courseId));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		public HttpResponseMessage Put([FromUri]System.Guid id, [FromBody]Enrollment enrollment)
		{
			if (enrollment == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			EnrollmentDatabase.Update(id, enrollment);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		public HttpResponseMessage Delete(System.Guid id)
		{
			EnrollmentDatabase.Remove(id);
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
