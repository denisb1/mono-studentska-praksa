using System;
using System.Collections.Generic;
using Day4.Models.Common;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
	public class CourseService : IGenericService<CourseDto>
	{
		public void Add(CourseDto courseDto)
		{
			new CourseRepository().Add(courseDto);
		}

		public void Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			new CourseRepository().Delete(id);
		}

		public void Update(Guid? id, CourseDto courseDto)
		{
			if (!id.HasValue || courseDto == null) throw new ArgumentNullException();
			new CourseRepository().Update(id, courseDto);
		}

		public CourseDto Get(Guid? id)
		{
			return new CourseRepository().Get(id);
		}

		public IEnumerable<CourseDto> GetAll()
		{
			return new CourseRepository().GetAll();
		}
	}
}
