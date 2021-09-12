using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using day9.API.Pagination;
using day9.Model;
using day9.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day9.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly IGenericService<CourseRest, CreateCourseRest, UpdateCourseRest> _service;

		public CourseController(IGenericService<CourseRest, CreateCourseRest, UpdateCourseRest> service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetCourses(
			string where,
			string order,
			int? page = 1,
			int? size = 10)
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
		public async Task<IActionResult> AddCourse([FromBody]CreateCourseRest createCourseRest)
		{
			try
			{
				await _service.Insert(createCourseRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateCourse(
			Guid id,
			[FromBody]UpdateCourseRest updateCourseRest)
		{
			try
			{
				await _service.Update(id, updateCourseRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
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
