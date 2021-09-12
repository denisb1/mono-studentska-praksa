using System;
using System.Linq;
using System.Threading.Tasks;
using day9.API.Pagination;
using day9.Model;
using day9.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day9.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TeacherController : ControllerBase
	{
		private readonly IGenericService<TeacherRest, CreateTeacherRest, UpdateTeacherRest> _service;

		public TeacherController(IGenericService<TeacherRest, CreateTeacherRest, UpdateTeacherRest> service)
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
		public async Task<IActionResult> AddTeacher([FromBody]CreateTeacherRest createTeacherRest)
		{
			try
			{
				await _service.Insert(createTeacherRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody]UpdateTeacherRest teacherRest)
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
