using System;
using Day3.Database;
using Day3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				return Ok(StudentDatabase.GetAll());
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
				return Ok(StudentDatabase.Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]StudentDto studentDto)
		{
			try
			{
				StudentDatabase.Add(new Student(studentDto));
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public IActionResult Put(Guid id, [FromBody]StudentDto studentDto)
		{
			try
			{
				StudentDatabase.Update(id, studentDto);
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
			StudentDatabase.Remove(id);
			return Ok();
		}
	}
}
