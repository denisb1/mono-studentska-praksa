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
	public sealed class EnrollmentRepository : IGenericRepository<EnrollmentDto>
	{
		public async Task Add(EnrollmentDto enrollmentDto)
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

			await new EnrollmentDatabase().Add(new Enrollment(id, enrollmentDto));
		}

		public async Task Delete(Guid? id)
		{
			await new EnrollmentDatabase().Delete(id);
		}

		public async Task Update(Guid? id, EnrollmentDto enrollmentDto)
		{
			await new EnrollmentDatabase().Update(new Enrollment(id, enrollmentDto));
		}

		public async Task<EnrollmentDto> Get(Guid? id)
		{
			var dataSet = await new EnrollmentDatabase().Get(id);
			var dt = dataSet.Tables["Enrollment"];
			return new EnrollmentDto(
				Guid.Parse(dt.Rows[0]["StudentId"].ToString()),
				Guid.Parse(dt.Rows[0]["CourseId"].ToString())
			);
		}

		public async Task<IEnumerable<EnrollmentDto>> GetAll()
		{
			var dataSet = await new EnrollmentDatabase().GetAll();
			var dt = dataSet.Tables["Enrollment"];
			return (from DataRow dr in dt.Rows select new EnrollmentDto(
				Guid.Parse(dr["StudentId"].ToString()),
				Guid.Parse(dr["CourseId"].ToString())
			)).ToList();
		}

		public async Task<StudentCourse> GetJoin(Guid id)
		{
			var dataSet = await new EnrollmentDatabase().GetJoin(id);
			var dt = dataSet.Tables["Enrollment"];
			return new StudentCourse(
				dt.Rows[0]["FirstName"].ToString(),
				dt.Rows[0]["LastName"].ToString(),
				dt.Rows[0]["College"].ToString(),
				int.Parse(dt.Rows[0]["CollegeYear"].ToString()),
				dt.Rows[0]["CourseName"].ToString(),
				double.Parse(dt.Rows[0]["Ects"].ToString())
			);
		}

		public async Task<IEnumerable<StudentCourse>> GetJoinAll()
		{
			var dataSet = await new EnrollmentDatabase().GetJoinAll();
			var dt = dataSet.Tables["Enrollment"];
			return (from DataRow dr in dt.Rows select new StudentCourse(
				dr["FirstName"].ToString(),
				dr["LastName"].ToString(),
				dr["College"].ToString(),
				int.Parse(dr["CollegeYear"].ToString()),
				dr["CourseName"].ToString(),
				double.Parse(dr["Ects"].ToString())
			)).ToList();
		}
	}
}
