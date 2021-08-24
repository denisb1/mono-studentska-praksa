using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models.Common;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
	public sealed class CourseService : IGenericService<CourseDto>
	{
		public async Task Add(CourseDto courseDto)
		{
			await new CourseRepository().Add(courseDto);
		}

		public async Task Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			await new CourseRepository().Delete(id);
		}

		public async Task Update(Guid? id, CourseDto courseDto)
		{
			if (!id.HasValue || courseDto == null) throw new ArgumentNullException();
			await new CourseRepository().Update(id, courseDto);
		}

		public async Task<CourseDto> Get(Guid? id)
		{
			return await new CourseRepository().Get(id);
		}

		public async Task<IEnumerable<CourseDto>> GetAll()
		{
			return await new CourseRepository().GetAll();
		}
	}
}
