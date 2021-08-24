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
	public class CourseController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<CourseDb> _repo;

		public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.CourseDbRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetCourse()
		{
			try
			{
				var courses = await _repo.GetAll();
				return Ok(_mapper.Map<IList<CourseRest>>(courses));
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
				var course = await _repo
					.Get(q => q.Id == id);
				return Ok(_mapper.Map<CourseDb>(course));
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
				var course = _mapper.Map<CourseDb>(courseRest);
				await _repo.Insert(course);
				await _unitOfWork.Save();

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
				var course = await _repo.Get(q => q.Id == id);
				if (course == null) return BadRequest();

				_mapper.Map(courseRest, course);
				_repo.Update(course);
				await _unitOfWork.Save();
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
				var courses = await _repo.GetAll(q =>
					q.CourseName == courseRest.CourseName &&
					q.Ects == courseRest.Ects
				);

				if (courses == null) return BadRequest();

				_repo.DeleteRange(courses);
				await _unitOfWork.Save();
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
				var course = await _repo.Get(s => s.Id == id);
				if (course == null) return BadRequest();

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
