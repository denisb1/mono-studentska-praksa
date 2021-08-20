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
	[Route("api/course")]
	public class CourseController : ApiController
	{
		[HttpGet]
		[Route("api/course")]
		public async Task<HttpResponseMessage> Get()
		{
			List<Course> courses;
			HttpResponseMessage httpMessage;

			try
			{
				courses = await new CourseService().GetAll();
				httpMessage = Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception)
			{
				courses = null;
				httpMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			CourseREST.InitializeCourses(courses);
			return httpMessage;
		}

		[HttpGet]
		[Route("api/course/{id}")]
		public async Task<HttpResponseMessage> Get(Guid? id)
		{
			if (!id.HasValue) return Request.CreateResponse(HttpStatusCode.BadRequest);

			var course = await new CourseService().Get(id);
			CourseREST.InitializeCourse(course);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[Route("api/course")]
		public async Task<HttpResponseMessage> Post([FromBody]Course course)
		{
			if (course == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				await new CourseService().Add(course);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/course/{id}")]
		public async Task<HttpResponseMessage> Put([FromUri]Guid id, [FromBody]Course course)
		{
			if (course == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			await new CourseService().Update(id, course);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/course/{id}")]
		public async Task<HttpResponseMessage> Delete(Guid? id)
		{
			try
			{
				await new CourseService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
