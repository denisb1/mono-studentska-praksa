using System;
using Day4.Models;
using Day4.Models.Common;
using Day4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day4.Controllers
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
				return Ok(new StudentService().GetAll());
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
				return Ok(new StudentService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]StudentDto studentDto)
		{
			try
			{
				new StudentService().Add(studentDto);
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
				new StudentService().Update(id, studentDto);
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
				new StudentService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
