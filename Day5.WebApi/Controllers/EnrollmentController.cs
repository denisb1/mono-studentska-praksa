using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Day5.Models;
using Day5.Service;

namespace Day5.WebApi.Controllers
{
	[Route("api/enrollment")]
	public class EnrollmentController : ApiController
	{
		[HttpGet]
		[Route("api/enrollment")]
		public async Task<HttpResponseMessage> Get()
		{
			List<Enrollment> enrollments;
			HttpResponseMessage httpMessage;

			try
			{
				enrollments = await new EnrollmentService().GetAll();
				httpMessage = Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception)
			{
				enrollments = null;
				httpMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			EnrollmentREST.InitializeEnrollments(enrollments);
			return httpMessage;
		}

		[HttpGet]
		[Route("api/enrollment/{id}")]
		public async Task<HttpResponseMessage> Get(Guid? id)
		{
			if (!id.HasValue) return Request.CreateResponse(HttpStatusCode.BadRequest);

			var enrollment =  await new EnrollmentService().Get(id);
			EnrollmentREST.InitializeEnrollment(enrollment);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpGet]
		[Route("api/enrollment/join")]
		public async Task<List<EnrollmentJoin>> GetAllJoined()
		{
			List<EnrollmentJoin> enrollments;
			//try
			//{
				enrollments = await new EnrollmentService().GetAllJoined();
				//return Request.CreateResponse(HttpStatusCode.OK);
			//}
			//catch (Exception)
			//{
			//	enrollments = null;
				//return Request.CreateResponse(HttpStatusCode.BadRequest);
			//}
			return enrollments;
		}

		[HttpGet]
		[Route("api/enrollment/join/{id}")]
		public async Task<EnrollmentJoin> GetJoined(Guid? id)
		{
			if (!id.HasValue) return null;// Request.CreateResponse(HttpStatusCode.BadRequest);
			var enrollment = await new EnrollmentService().GetJoined(id);
			//return Request.CreateResponse(HttpStatusCode.OK);
			return enrollment;
		}

		[HttpPost]
		[Route("api/enrollment")]
		public async Task<HttpResponseMessage> Post([FromBody]Enrollment enrollment)
		{
			if (enrollment == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				await new EnrollmentService().Add(enrollment);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/enrollment/{id}")]
		public async Task<HttpResponseMessage> Put([FromUri]Guid id, [FromBody]Enrollment enrollment)
		{
			if (enrollment == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			await new EnrollmentService().Update(id, enrollment);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/enrollment/{id}")]
		public async Task<HttpResponseMessage> Delete(Guid? id)
		{
			try
			{
				await new EnrollmentService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
