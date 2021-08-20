using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
	public sealed class CourseService : IService<Course>
	{
		public async Task Add(Course course)
		{
			var courseRepository = new CourseRepository();
			await courseRepository.Add(course);
		}

		public async Task Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();

			var courseRepository = new CourseRepository();
			await courseRepository.Delete(id);
		}

		public async Task Update(Guid? id, Course course)
		{
			if (!id.HasValue || course == null) throw new ArgumentNullException();

			var courseRepository = new CourseRepository();
			await courseRepository.Update(id, course);
		}

		public async Task<Course> Get(Guid? id)
		{
			var courseRepository = new CourseRepository();
			return await courseRepository.Get(id);
		}

		public async Task<List<Course>> GetAll()
		{
			var courseRepository = new CourseRepository();
			return await courseRepository.GetAll();
		}
	}
}
