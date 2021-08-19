using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day4.Models;
using Day4.Service;

namespace Day4.WebApi.Controllers
{
	[Route("api/student")]
	public class StudentController : ApiController
	{
		[HttpGet]
		[Route("api/student")]
		public List<Student> Get()
		{
			List<Student> students;
			try
			{
				students = new StudentService().GetAll();
			}
			catch (Exception)
			{
				students = null;
			}
			return students;
		}

		[HttpGet]
		[Route("api/student/{id}")]
		public Student Get(Guid? id)
		{
			return !id.HasValue ? null : new StudentService().Get(id);
		}

		[HttpPost]
		[Route("api/student")]
		public HttpResponseMessage Post([FromBody]Student student)
		{
			if (student == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				new StudentService().Add(student);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/student/{id}")]
		public HttpResponseMessage Put([FromUri]Guid id, [FromBody]Student student)
		{
			if (student == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			new StudentService().Update(id, student);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/student/{id}")]
		public HttpResponseMessage Delete(Guid? id)
		{
			try
			{
				new StudentService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
