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
    public sealed class StudentRepository : IGenericRepository<Student>
    {
        public void Add(Student student)
        {
            new StudentDatabase().Add(student);
        }

        public void Delete(Guid? id)
        {
	        new StudentDatabase().Delete(id);
        }

        public void Update(Guid? id, Student student)
        {
            new StudentDatabase().Update(id, student);
        }

        public Student Get(Guid? id)
        {
            var dataTable = new StudentDatabase().Get(id).Tables["Student"];
            return new Student(
                Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
                new Name(dataTable.Rows[0]["FirstName"].ToString(), dataTable.Rows[0]["LastName"].ToString()),
                dataTable.Rows[0]["College"].ToString(),
                int.Parse(dataTable.Rows[0]["CollegeYear"].ToString()),
                int.Parse(dataTable.Rows[0]["Age"].ToString())
            );
        }

        public List<Student> GetAll()
        {
            var dataTable = new StudentDatabase().GetAll().Tables["Student"];
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
