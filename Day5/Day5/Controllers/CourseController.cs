using System;
using System.Threading.Tasks;
using Day5.Models.Common;
using Day5.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day5.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await new CourseService().GetAll());
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			try
			{
				return Ok(await new CourseService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]CourseDto courseDto)
		{
			try
			{
				await new CourseService().Add(courseDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> Put(Guid id, [FromBody]CourseDto courseDto)
		{
			try
			{
				await new CourseService().Update(id, courseDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await new CourseService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
