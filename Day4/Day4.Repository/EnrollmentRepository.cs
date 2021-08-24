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
	public sealed class EnrollmentRepository : IGenericRepository<EnrollmentDto>
	{
		public void Add(EnrollmentDto enrollmentDto)
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

			new EnrollmentDatabase().Add(new Enrollment(id, enrollmentDto));
		}

		public void Delete(Guid? id)
		{
			new EnrollmentDatabase().Delete(id);
		}

		public void Update(Guid? id, EnrollmentDto enrollmentDto)
		{
			new EnrollmentDatabase().Update(new Enrollment(id, enrollmentDto));
		}

		public EnrollmentDto Get(Guid? id)
		{
			var dt = new EnrollmentDatabase().Get(id).Tables["Enrollment"];
			return new EnrollmentDto(
				Guid.Parse(dt.Rows[0]["StudentId"].ToString()),
				Guid.Parse(dt.Rows[0]["CourseId"].ToString())
			);
		}

		public IEnumerable<EnrollmentDto> GetAll()
		{
			var dt = new EnrollmentDatabase().GetAll().Tables["Enrollment"];
			return (from DataRow dr in dt.Rows select new EnrollmentDto(
				Guid.Parse(dr["StudentId"].ToString()),
				Guid.Parse(dr["CourseId"].ToString())
			)).ToList();
		}

		public StudentCourse GetJoin(Guid id)
		{
			var dt = new EnrollmentDatabase().GetJoin(id).Tables["Enrollment"];
			return new StudentCourse(
				dt.Rows[0]["FirstName"].ToString(),
				dt.Rows[0]["LastName"].ToString(),
				dt.Rows[0]["College"].ToString(),
				int.Parse(dt.Rows[0]["CollegeYear"].ToString()),
				dt.Rows[0]["CourseName"].ToString(),
				double.Parse(dt.Rows[0]["Ects"].ToString())
			);
		}

		public IEnumerable<StudentCourse> GetJoinAll()
		{
			var dt = new EnrollmentDatabase().GetJoinAll().Tables["Enrollment"];
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
