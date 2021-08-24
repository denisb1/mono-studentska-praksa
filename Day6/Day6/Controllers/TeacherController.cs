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
	public class TeacherController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<TeacherDb> _repo;

		public TeacherController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.TeacherDbRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetTeacher()
		{
			try
			{
				var teachers = await _repo.GetAll();
				return Ok(_mapper.Map<IList<TeacherRest>>(teachers));
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
				var teacher = await _repo
					.Get(q => q.Id == id);
				return Ok(_mapper.Map<TeacherRest>(teacher));
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
				var teacher = _mapper.Map<TeacherDb>(teacherRest);
				await _repo.Insert(teacher);
				await _unitOfWork.Save();

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
				var teacher = await _repo.Get(q => q.Id == id);
				if (teacher == null) return BadRequest();

				_mapper.Map(teacherRest, teacher);
				_repo.Update(teacher);
				await _unitOfWork.Save();
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
				var teachers = await _repo.GetAll(q =>
					q.FirstName == teacherRest.FirstName &&
					q.LastName == teacherRest.LastName &&
					q.Department == teacherRest.Department
				);

				if (teachers == null) return BadRequest();

				_repo.DeleteRange(teachers);
				await _unitOfWork.Save();
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
				var teacher = await _repo.Get(q => q.Id == id);
				if (teacher == null) return BadRequest();

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
