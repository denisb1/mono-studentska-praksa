using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Day6.DAL;
using Day6.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Day6.Repository;

namespace Day6.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EnrollmentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<EnrollmentDb> _repo;

		public EnrollmentController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.EnrollmentDbRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetEnrollment()
		{
			try
			{
				var enrollments = await _repo
					.GetAll(null, null, new List<string>
				{
					"StudentDb", "CourseDb"
				});
				return Ok(enrollments);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetEnrollment(Guid id)
		{
			try
			{
				var enrollment = await _repo
					.Get(q => q.Id == id, new List<string>
					{
						"StudentDb", "CourseDb"
					});
				return Ok(enrollment);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddCourse([FromBody]EnrollmentDto enrollmentRest)
		{
			try
			{
				var enrollment = _mapper.Map<EnrollmentDb>(enrollmentRest);
				await _repo.Insert(enrollment);
				await _unitOfWork.Save();

				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateEnrollment(Guid id, [FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				var enrollment = await _repo.Get(q => q.Id == id);
				if (enrollment == null) return BadRequest();

				_mapper.Map(enrollmentDto, enrollment);
				_repo.Update(enrollment);
				await _unitOfWork.Save();
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteEnrollment([FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				var enrollments = await _repo.GetAll(q =>
					q.StudentId == enrollmentDto.StudentId &&
					q.CourseId == enrollmentDto.CourseId
				);

				if (enrollments == null) return BadRequest();

				_repo.DeleteRange(enrollments);
				await _unitOfWork.Save();
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteEnrollment(Guid id)
		{
			try
			{
				var enrollment = await _repo.Get(q => q.Id == id);
				if (enrollment == null) return BadRequest();

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
