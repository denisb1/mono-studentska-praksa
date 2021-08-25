using System;
using System.Linq;
using System.Threading.Tasks;
using Day6.Common;
using Microsoft.AspNetCore.Mvc;
using Day6.Models.REST;
using Day6.Service;
using Microsoft.EntityFrameworkCore;

namespace Day6.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly IGenericService<CourseRest> _service;

		public CourseController(IGenericService<CourseRest> service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetCourses(string where, string order, int? page = 1, int? size = 10)
		{
			try
			{
				if (page == null && size == null) return BadRequest();

				var courses = await _service.GetAll(where, order);
				var pages = PaginatedList<CourseRest>
					.Create(courses.AsQueryable().AsNoTracking(), page.Value, size.Value);

				return Ok(pages);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetCourse(Guid id)
		{
			try
			{
				var course = await _service.GetById(id);
				return Ok(course);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddCourse([FromBody]CourseRest courseRest)
		{
			try
			{
				await _service.Insert(courseRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateCourse(Guid id, [FromBody]CourseRest courseRest)
		{
			try
			{
				await _service.Update(id, courseRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCourse([FromBody]CourseRest courseRest)
		{
			try
			{
				await _service.Delete(courseRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteCourse(Guid id)
		{
			try
			{
				await _service.Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
