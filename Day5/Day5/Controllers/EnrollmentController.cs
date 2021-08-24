using System;
using System.Threading.Tasks;
using Day5.Models.Common;
using Day5.Service;
using Microsoft.AspNetCore.Mvc;

namespace Day5.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await new EnrollmentService().GetAll());
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
				return Ok(await new EnrollmentService().Get(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("join")]
		public async Task<IActionResult> GetJoin()
		{
			try
			{
				return Ok(await new EnrollmentService().GetJoinAll());
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("join/{id:Guid}")]
		public async Task<IActionResult> GetJoin(Guid id)
		{
			try
			{
				return Ok(await new EnrollmentService().GetJoin(id));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				await new EnrollmentService().Add(enrollmentDto);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> Put(Guid id, [FromBody]EnrollmentDto enrollmentDto)
		{
			try
			{
				await new EnrollmentService().Update(id, enrollmentDto);
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
				await new EnrollmentService().Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
