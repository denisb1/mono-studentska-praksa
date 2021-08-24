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
	public sealed class CourseRepository : IGenericRepository<CourseDto>
	{
		public async Task Add(CourseDto courseDto)
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

			await new CourseDatabase().Add(new Course(id, courseDto));
		}

		public async Task Delete(Guid? id)
		{
			await new CourseDatabase().Delete(id);
		}

		public async Task Update(Guid? id, CourseDto courseDto)
		{
			await new CourseDatabase().Update(new Course(id, courseDto));
		}

		public async Task<CourseDto> Get(Guid? id)
		{
			var dataSet = await new CourseDatabase().Get(id);
			var dt = dataSet.Tables["Course"];
			return new CourseDto(
				dt.Rows[0]["CourseName"].ToString(),
				dt.Rows[0]["TeacherFirstName"].ToString(),
				dt.Rows[0]["TeacherLastName"].ToString(),
				double.Parse(dt.Rows[0]["Ects"].ToString() ?? string.Empty)
			);
		}

		public async Task<IEnumerable<CourseDto>> GetAll()
		{
			var dataSet = await new CourseDatabase().GetAll();
			var dt = dataSet.Tables["Course"];
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
