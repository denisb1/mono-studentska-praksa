using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Day5.DAL;
using Day5.Models;
using Day5.Models.Common;
using Day5.Repository.Common;

namespace Day5.Repository
{
	public sealed class CourseRepository : IRepository<Course>
	{
		public async Task Add(Course course)
		{
			var courseDatabase = new CourseDatabase();
			await courseDatabase.Add(course);
		}

		public async Task Delete(Guid? id)
		{
			var courseDatabase = new CourseDatabase();
			await courseDatabase.Delete(id);
		}

		public async Task Update(Guid? id, Course course)
		{
			var courseDatabase = new CourseDatabase();
			await courseDatabase.Update(id, course);
		}

		public async Task<Course> Get(Guid? id)
		{
			var courseDatabase = new CourseDatabase();
			var dataSet = await courseDatabase.GetAll();
			var dataTable = dataSet.Tables["Course"];
			return new Course(
				Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
				dataTable.Rows[0]["CourseName"].ToString(),
				new Name(dataTable.Rows[0]["TeacherFirstName"].ToString(), dataTable.Rows[0]["TeacherLastName"].ToString()),
				int.Parse(dataTable.Rows[0]["Ects"].ToString())
			);
		}

		public async Task<List<Course>> GetAll()
		{
			var courseDatabase = new CourseDatabase();
			var dataSet = await courseDatabase.GetAll();
			var dataTable = dataSet.Tables["Course"];

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
