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
	public sealed class StudentRepository : IGenericRepository<StudentDto>
	{
		public async Task Add(StudentDto studentDto)
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

			await new StudentDatabase().Add(new Student(id, studentDto));
		}

		public async Task Delete(Guid? id)
		{
			await new StudentDatabase().Delete(id);
		}

		public async Task Update(Guid? id, StudentDto studentDto)
		{
			await new StudentDatabase().Update(new Student(id, studentDto));
		}

		public async Task<StudentDto> Get(Guid? id)
		{
			var dataSet = await new StudentDatabase().Get(id);
			var dt = dataSet.Tables["Student"];
			return new StudentDto(
				dt.Rows[0]["FirstName"].ToString(),
				dt.Rows[0]["LastName"].ToString(),
				dt.Rows[0]["College"].ToString(),
				int.Parse(dt.Rows[0]["CollegeYear"].ToString()),
				int.Parse(dt.Rows[0]["Age"].ToString())
			);
		}

		public async Task<IEnumerable<StudentDto>> GetAll()
		{
			var dataSet = await new StudentDatabase().GetAll();
			var dt = dataSet.Tables["Student"];
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
