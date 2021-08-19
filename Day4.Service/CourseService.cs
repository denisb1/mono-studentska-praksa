using System;
using System.Collections.Generic;
using Day4.Models;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
	public sealed class CourseService : IService<Course>
	{
		public void Add(Course course)
		{
			new CourseRepository().Add(course);
		}

		public void Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			new CourseRepository().Delete(id);
		}

		public void Update(Guid? id, Course course)
		{
			if (!id.HasValue || course == null) throw new ArgumentNullException();
			new CourseRepository().Update(id, course);
		}

		public Course Get(Guid? id)
		{
			return new CourseRepository().Get(id);
		}

		public List<Course> GetAll()
		{
			return new CourseRepository().GetAll();
		}
	}
}
