using System;
using Day3.Database;
using Day3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				return Ok(CourseDatabase.GetAll());
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
				return Ok(CourseDatabase.Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]CourseDto courseDto)
		{
			try
			{
				CourseDatabase.Add(new Course(courseDto));
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public IActionResult Put(Guid id, [FromBody]CourseDto courseDto)
		{
			try
			{
				CourseDatabase.Update(id, new Course(id, courseDto));
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
			CourseDatabase.Remove(id);
			return Ok();
		}
	}
}
