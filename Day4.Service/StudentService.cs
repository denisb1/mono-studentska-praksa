using System;
using System.Collections.Generic;
using Day4.Models;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
    public sealed class StudentService : IService<Student>
    {
	    public void Add(Student student)
	    {
		    new StudentRepository().Add(student);
	    }

	    public void Delete(Guid? id)
	    {
		    if (!id.HasValue) throw new ArgumentNullException();
		    new StudentRepository().Delete(id);
	    }

	    public void Update(Guid? id, Student student)
	    {
		    if (!id.HasValue || student == null) throw new ArgumentNullException();
		    new StudentRepository().Update(id, student);
	    }

	    public Student Get(Guid? id)
	    {
		    return new StudentRepository().Get(id);
	    }

	    public List<Student> GetAll()
        {
            return new StudentRepository().GetAll();
        }
    }
}
