using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Day6.Common;
using Day6.DAL;
using Microsoft.AspNetCore.Mvc;
using Day6.Models.REST;
using Day6.Service;
using Microsoft.EntityFrameworkCore;

namespace Day6.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly IGenericService<StudentRest> _service;
		private readonly IMapper _mapper;

		public StudentController(IGenericService<StudentRest> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetStudents(string where, string order, int? page = 1, int? size = 10)
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
				return Ok(_mapper.Map<StudentDb>(student));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddStudent([FromBody]StudentRest studentRest)
		{
			try
			{
				await _service.Insert(studentRest);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateStudent(Guid id, [FromBody]StudentRest studentRest)
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

		[HttpDelete]
		public async Task<IActionResult> DeleteStudent([FromBody]StudentRest studentRest)
		{
			try
			{
				await _service.Delete(studentRest);
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
