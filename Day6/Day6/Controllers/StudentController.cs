using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Day6.DAL;
using Microsoft.AspNetCore.Mvc;
using Day6.Models.REST;
using Day6.Repository;

namespace Day6.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<StudentDb> _repo;

		public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.StudentDbRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetStudent()
		{
			try
			{
				var students = await _repo.GetAll();
				return Ok(_mapper.Map<IList<StudentRest>>(students));
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
				var student = await _repo.Get(q => q.Id == id);
				return Ok(_mapper.Map<StudentRest>(student));
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
				var student = _mapper.Map<StudentDb>(studentRest);
				await _repo.Insert(student);
				await _unitOfWork.Save();

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
				var student = await _repo.Get(q => q.Id == id);
				if (student == null) return BadRequest();

				_mapper.Map(studentRest, student);
				_repo.Update(student);
				await _unitOfWork.Save();
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
				var students = await _repo.GetAll(q =>
					q.FirstName == studentRest.FirstName &&
					q.LastName == studentRest.LastName &&
					q.Year == studentRest.Age &&
					q.Age == studentRest.Age
				);

				if (students == null) return BadRequest();

				_repo.DeleteRange(students);
				await _unitOfWork.Save();
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
				var student = await _repo.Get(q => q.Id == id);
				if (student == null) return BadRequest();

				await _repo.Delete(id);
				await _unitOfWork.Save();
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
