using System;
using Day4.Models.Common;
using Day4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day4.Controllers
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
				return Ok(new CourseService().GetAll());
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
				return Ok(new CourseService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]CourseDto courseDto)
		{
			try
			{
				new CourseService().Add(courseDto);
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
				new CourseService().Update(id, courseDto);
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
				new CourseService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
