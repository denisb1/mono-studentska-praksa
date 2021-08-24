using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Day4.DAL;
using Day4.Models;
using Day4.Models.Common;
using Day4.Repository.Common;

namespace Day4.Repository
{
	public sealed class CourseRepository : IGenericRepository<CourseDto>
	{
		public void Add(CourseDto courseDto)
		{
			var idNotFound = false;

			var id = Guid.NewGuid();
			do
			{
				if (idNotFound) id = Guid.NewGuid();
				try
				{
					idNotFound = Get(id) != null;
				}
				catch (Exception)
				{
					idNotFound = false;
				}
			} while (idNotFound);

			new CourseDatabase().Add(new Course(id, courseDto));
		}

		public void Delete(Guid? id)
		{
			new CourseDatabase().Delete(id);
		}

		public void Update(Guid? id, CourseDto courseDto)
		{
			new CourseDatabase().Update(new Course(id, courseDto));
		}

		public CourseDto Get(Guid? id)
		{
			var dt = new CourseDatabase().Get(id).Tables["Course"];
			return new CourseDto(
				dt.Rows[0]["CourseName"].ToString(),
				dt.Rows[0]["TeacherFirstName"].ToString(),
				dt.Rows[0]["TeacherLastName"].ToString(),
				double.Parse(dt.Rows[0]["Ects"].ToString() ?? string.Empty)
			);
		}

		public IEnumerable<CourseDto> GetAll()
		{
			var dt = new CourseDatabase().GetAll().Tables["Course"];
			return (from DataRow dr in dt.Rows select new CourseDto(
				dr["CourseName"].ToString(),
				dr["TeacherFirstName"].ToString(),
				dr["TeacherLastName"].ToString(),
				double.Parse(dr["Ects"].ToString() ?? string.Empty)
				))
			.ToList();
		}
	}
}
