using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using day9.API.Pagination;
using day9.Model;
using day9.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace day9.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly IGenericService<StudentRest, CreateStudentRest, UpdateStudentRest> _service;

		public StudentController(IGenericService<StudentRest, CreateStudentRest, UpdateStudentRest> service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetStudents(string where,
			string order,
			int? page = 1,
			int? size = 10)
		{
			try
			{
				if (page == null && size == null) return BadRequest();

				var students = await _service.GetAll(where, order);
				var pages = PaginatedList<StudentRest>
					.Create(students.AsQueryable().AsNoTracking(), page.Value, size.Value);

				return Ok(pages);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetStudent(Guid id)
		{
			try
			{
				var student = await _service.GetById(id);
				return Ok(student);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddStudent([FromBody]CreateStudentRest createStudentRest)
		{
			try
			{
				await _service.Insert(createStudentRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateStudent(
			Guid id,
			[FromBody]UpdateStudentRest studentRest)
		{
			try
			{
				await _service.Update(id, studentRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteStudent(Guid id)
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
