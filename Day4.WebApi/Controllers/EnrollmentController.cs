using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day4.Models;
using Day4.Service;

namespace Day4.WebApi.Controllers
{
	[Route("api/enrollment")]
	public class EnrollmentController : ApiController
	{
		[HttpGet]
		[Route("api/enrollment")]
		public List<Enrollment> Get()
		{
			List<Enrollment> enrollments;
			try
			{
				enrollments = new EnrollmentService().GetAll();
			}
			catch (Exception)
			{
				enrollments = null;
			}
			return enrollments;
		}

		[HttpGet]
		[Route("api/enrollment/join")]
		public List<List<string>> GetAllJoined()
		{
			List<List<string>> enrollments;
			try
			{
				enrollments = new EnrollmentService().GetAllJoined();
			}
			catch (Exception)
			{
				enrollments = null;
			}
			return enrollments;
		}

		[HttpGet]
		[Route("api/enrollment/{id}")]
		public Enrollment Get(Guid? id)
		{
			return !id.HasValue ? null : new EnrollmentService().Get(id);
		}

		[HttpGet]
		[Route("api/enrollment/join/{id}")]
		public IEnumerable<string> GetJoined(Guid? id)
		{
			return !id.HasValue ? null : new EnrollmentService().GetJoined(id);
		}

		[HttpPost]
		[Route("api/enrollment")]
		public HttpResponseMessage Post([FromBody]Enrollment enrollment)
		{
			if (enrollment == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				new EnrollmentService().Add(enrollment);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/enrollment/{id}")]
		public HttpResponseMessage Put([FromUri]Guid id, [FromBody]Enrollment enrollment)
		{
			if (enrollment == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			new EnrollmentService().Update(id, enrollment);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/enrollment/{id}")]
		public HttpResponseMessage Delete(Guid? id)
		{
			try
			{
				new EnrollmentService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
