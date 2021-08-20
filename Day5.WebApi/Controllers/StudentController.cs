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
	[Route("api/student")]
	public class StudentController : ApiController
	{
		[HttpGet]
		[Route("api/student")]
		public async Task<HttpResponseMessage> Get()
		{
			List<Student> students;
			HttpResponseMessage httpMessage;

			try
			{
				students = await new StudentService().GetAll();
				httpMessage = Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception)
			{
				students = null;
				httpMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			StudentREST.InitializeStudents(students);
			return httpMessage;
		}

		[HttpGet]
		[Route("api/student/{id}")]
		public async Task<HttpResponseMessage> Get(Guid? id)
		{
			if (!id.HasValue) return Request.CreateResponse(HttpStatusCode.BadRequest);

			var student = await new StudentService().Get(id);
			StudentREST.InitializeStudent(student);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[Route("api/student")]
		public async Task<HttpResponseMessage> Post([FromBody]Student student)
		{
			if (student == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				await new StudentService().Add(student);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/student/{id}")]
		public async Task<HttpResponseMessage> Put([FromUri]Guid id, [FromBody]Student student)
		{
			if (student == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			await new StudentService().Update(id, student);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/student/{id}")]
		public async Task<HttpResponseMessage> Delete(Guid? id)
		{
			try
			{
				await new StudentService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
