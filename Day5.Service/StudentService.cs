using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
    public sealed class StudentService : IService<Student>
    {
	    public async Task Add(Student student)
	    {
		    var studentRepository = new StudentRepository();
		    await studentRepository.Add(student);
	    }

	    public async Task Delete(Guid? id)
	    {
		    if (!id.HasValue) throw new ArgumentNullException();

		    var studentRepository = new StudentRepository();
		    await studentRepository.Delete(id);
	    }

	    public async Task Update(Guid? id, Student student)
	    {
		    if (!id.HasValue || student == null) throw new ArgumentNullException();

		    var studentRepository = new StudentRepository();
		    await studentRepository.Update(id, student);
	    }

	    public async Task<Student> Get(Guid? id)
	    {
		    var studentRepository = new StudentRepository();
		    return await studentRepository.Get(id);
	    }

	    public async Task<List<Student>> GetAll()
        {
	        var studentRepository = new StudentRepository();
            return await studentRepository.GetAll();
        }
    }
}
