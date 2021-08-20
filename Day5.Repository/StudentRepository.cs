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
    public sealed class StudentRepository : IRepository<Student>
    {
        public async Task Add(Student student)
        {
	        var studentDatabase = new StudentDatabase();
			await studentDatabase.Add(student);
        }

        public async Task Delete(Guid? id)
        {
	        var studentDatabase = new StudentDatabase();
	        await studentDatabase.Delete(id);
        }

        public async Task Update(Guid? id, Student student)
        {
	        var studentDatabase = new StudentDatabase();
	        await studentDatabase.Update(id, student);
        }

        public async Task<Student> Get(Guid? id)
        {
	        var studentDatabase = new StudentDatabase();
	        var dataSet = await studentDatabase.Get(id);
	        var dataTable = dataSet.Tables["Student"];

            return new Student(
                Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
                new Name(dataTable.Rows[0]["FirstName"].ToString(), dataTable.Rows[0]["LastName"].ToString()),
                dataTable.Rows[0]["College"].ToString(),
                int.Parse(dataTable.Rows[0]["CollegeYear"].ToString()),
                int.Parse(dataTable.Rows[0]["Age"].ToString())
            );
        }

        public async Task<List<Student>> GetAll()
        {
	        var studentDatabase = new StudentDatabase();
	        var dataSet = await studentDatabase.GetAll();
	        var dataTable = dataSet.Tables["Student"];

	        return (from DataRow dataRow in dataTable.Rows select new Student(
                        Guid.Parse(dataRow["Id"].ToString()),
                        new Name(dataRow["FirstName"].ToString(), dataRow["LastName"].ToString()),
                        dataRow["College"].ToString(),
                        int.Parse(dataRow["CollegeYear"].ToString()),
                        int.Parse(dataRow["Age"].ToString())
                    )
                )
            .ToList();
        }
    }
}
