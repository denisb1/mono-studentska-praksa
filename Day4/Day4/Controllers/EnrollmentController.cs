using System;
using Day4.Models.Common;
using Day4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				return Ok(new EnrollmentService().GetAll());
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("{id:Guid}")]
		public IActionResult Get(Guid id)
		{
			try
			{
				return Ok(new EnrollmentService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("join")]
		public IActionResult GetJoin()
		{
			try
			{
				return Ok(new EnrollmentService().GetJoinAll());
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("join/{id:Guid}")]
		public IActionResult GetJoin(Guid id)
		{
			try
			{
				return Ok(new EnrollmentService().GetJoin(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				new EnrollmentService().Add(enrollmentDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public IActionResult Put(Guid id, [FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				new EnrollmentService().Update(id, enrollmentDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpDelete("{id:Guid}")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				new EnrollmentService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
