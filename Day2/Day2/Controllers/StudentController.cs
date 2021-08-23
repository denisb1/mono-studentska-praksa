using System;
using Day2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		// GET api/student
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				return Ok(Database.GetAll());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// GET api/student/5
		[HttpGet("{id:int}")]
		public IActionResult Get(int id)
		{
			try
			{
				return Ok(Database.Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// POST api/student
		[HttpPost]
		public IActionResult Post([FromBody]Student student)
		{
			try
			{
				Database.Add(student);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// PUT api/student/5
		[HttpPut("{id:int}")]
		public IActionResult Put(int id, [FromBody]Student student)
		{
			try
			{
				Database.Update(id, student);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// DELETE api/student/5
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id)
		{
			try
			{
				Database.Remove(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
