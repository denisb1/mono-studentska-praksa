using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Day4.DAL;
using Day4.Models;
using Day4.Repository.Common;

namespace Day4.Repository
{
	public sealed class EnrollmentRepository : IGenericRepository<Enrollment>
	{
		public void Add(Enrollment enrollment)
		{
			new EnrollmentDatabase().Add(enrollment);
		}

		public void Delete(Guid? id)
		{
			new EnrollmentDatabase().Delete(id);
		}

		public void Update(Guid? id, Enrollment enrollment)
		{
			new EnrollmentDatabase().Update(id, enrollment);
		}

		public Enrollment Get(Guid? id)
		{
			var dataTable = new EnrollmentDatabase().Get(id).Tables["Enrollment"];
			return new Enrollment(
				Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
				Guid.Parse(dataTable.Rows[0]["StudentId"].ToString()),
				Guid.Parse(dataTable.Rows[0]["CourseId"].ToString())
			);
		}

		public IEnumerable<string> GetJoined(Guid? id)
		{
			return new EnrollmentDatabase().GetJoined(id);
		}

		public List<Enrollment> GetAll()
		{
			var dataTable = new EnrollmentDatabase().GetAll().Tables["Enrollment"];
			return (from DataRow dataRow in dataTable.Rows select new Enrollment(
						Guid.Parse(dataRow["Id"].ToString()),
						Guid.Parse(dataRow["StudentId"].ToString()),
						Guid.Parse(dataRow["CourseId"].ToString())
					)
				)
				.ToList();
		}

		public List<List<string>> GetAllJoined()
		{
			return new EnrollmentDatabase().GetAllJoined();
		}
	}
}
