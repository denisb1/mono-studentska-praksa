using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day4.Models;
using Day4.Service;

namespace Day4.WebApi.Controllers
{
	[Route("api/course")]
	public class CourseController : ApiController
	{
		[HttpGet]
		[Route("api/course")]
		public List<Course> Get()
		{
			List<Course> courses;
			try
			{
				courses = new CourseService().GetAll();
			}
			catch (Exception)
			{
				courses = null;
			}
			return courses;
		}

		[HttpGet]
		[Route("api/course/{id}")]
		public Course Get(Guid? id)
		{
			return !id.HasValue ? null : new CourseService().Get(id);
		}

		[HttpPost]
		[Route("api/course")]
		public HttpResponseMessage Post([FromBody]Course course)
		{
			if (course == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

			try
			{
				new CourseService().Add(course);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPut]
		[Route("api/course/{id}")]
		public HttpResponseMessage Put([FromUri]Guid id, [FromBody]Course course)
		{
			if (course == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
			new CourseService().Update(id, course);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpDelete]
		[Route("api/course/{id}")]
		public HttpResponseMessage Delete(Guid? id)
		{
			try
			{
				new CourseService().Delete(id);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
