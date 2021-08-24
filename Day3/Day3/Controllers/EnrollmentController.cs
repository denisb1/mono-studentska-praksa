using System;
using Day3.Database;
using Day3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day3.Controllers
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
				return Ok(EnrollmentDatabase.GetAll());
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
				return Ok(EnrollmentDatabase.Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				EnrollmentDatabase.Add(new Enrollment(enrollmentDto));
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
				EnrollmentDatabase.Update(id, new Enrollment(id, enrollmentDto));
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id:Guid}")]
		public IActionResult Delete(Guid id)
		{
			EnrollmentDatabase.Remove(id);
			return Ok();
		}
	}
}
