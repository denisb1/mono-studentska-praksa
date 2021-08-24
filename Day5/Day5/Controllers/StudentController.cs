using System;
using System.Threading.Tasks;
using Day5.Models.Common;
using Day5.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day5.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await new StudentService().GetAll());
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
				return Ok(await new StudentService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]StudentDto studentDto)
		{
			try
			{
				await new StudentService().Add(studentDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> Put(Guid id, [FromBody]StudentDto studentDto)
		{
			try
			{
				await new StudentService().Update(id, studentDto);
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
				await new StudentService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
