using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Day4.DAL;
using Day4.Models;
using Day4.Repository.Common;

namespace Day4.Repository
{
	public sealed class StudentRepository : IGenericRepository<StudentDto>
	{
		public void Add(StudentDto studentDto)
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

			new StudentDatabase().Add(new Student(id, studentDto));
		}

		public void Delete(Guid? id)
		{
			new StudentDatabase().Delete(id);
		}

		public void Update(Guid? id, StudentDto studentDto)
		{
			new StudentDatabase().Update(new Student(id, studentDto));
		}

		public StudentDto Get(Guid? id)
		{
			var dt = new StudentDatabase().Get(id).Tables["Student"];
			return new StudentDto(
				dt.Rows[0]["FirstName"].ToString(),
				dt.Rows[0]["LastName"].ToString(),
				dt.Rows[0]["College"].ToString(),
				int.Parse(dt.Rows[0]["CollegeYear"].ToString()),
				int.Parse(dt.Rows[0]["Age"].ToString())
			);
		}

		public IEnumerable<StudentDto> GetAll()
		{
			var dt = new StudentDatabase().GetAll().Tables["Student"];
			return (from DataRow dr in dt.Rows select new StudentDto(
				dr["FirstName"].ToString(),
				dr["LastName"].ToString(),
				dr["College"].ToString(),
				int.Parse(dr["CollegeYear"].ToString()),
				int.Parse(dr["Age"].ToString())
				))
			.ToList();
		}
	}
}
