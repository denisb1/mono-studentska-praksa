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
	public sealed class CourseRepository : IGenericRepository<Course>
	{
		public void Add(Course course)
		{
			new CourseDatabase().Add(course);
		}

		public void Delete(Guid? id)
		{
			new CourseDatabase().Delete(id);
		}

		public void Update(Guid? id, Course course)
		{
			new CourseDatabase().Update(id, course);
		}

		public Course Get(Guid? id)
		{
			var dataTable = new CourseDatabase().Get(id).Tables["Course"];
			return new Course(
				Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
				dataTable.Rows[0]["CourseName"].ToString(),
				new Name(dataTable.Rows[0]["TeacherFirstName"].ToString(), dataTable.Rows[0]["TeacherLastName"].ToString()),
				int.Parse(dataTable.Rows[0]["Ects"].ToString())
			);
		}

		public List<Course> GetAll()
		{
			var dataTable = new CourseDatabase().GetAll().Tables["Course"];
			return (from DataRow dataRow in dataTable.Rows select new Course(
						Guid.Parse(dataRow["Id"].ToString()),
						dataRow["CourseName"].ToString(),
						new Name(dataRow["TeacherFirstName"].ToString(), dataRow["TeacherLastName"].ToString()),
						int.Parse(dataRow["Ects"].ToString())
					)
				)
				.ToList();
		}
	}
}
