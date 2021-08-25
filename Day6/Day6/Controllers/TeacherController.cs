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
	public class TeacherController : ControllerBase
	{
		private readonly IGenericService<TeacherRest> _service;

		public TeacherController(IGenericService<TeacherRest> service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetTeachers(string where, string order, int? page = 1, int? size = 10)
		{
			try
			{
				if (page == null && size == null) return BadRequest();

				var teachers = await _service.GetAll(where, order);
				var pages = PaginatedList<TeacherRest>
					.Create(teachers.AsQueryable().AsNoTracking(), page.Value, size.Value);

				return Ok(pages);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetTeacher(Guid id)
		{
			try
			{
				var teacher = await _service.GetById(id);
				return Ok(teacher);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddTeacher([FromBody]TeacherRest teacherRest)
		{
			try
			{
				await _service.Insert(teacherRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody]TeacherRest teacherRest)
		{
			try
			{
				await _service.Update(id, teacherRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteTeacher([FromBody]TeacherRest teacherRest)
		{
			try
			{
				await _service.Delete(teacherRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteTeacher(Guid id)
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
