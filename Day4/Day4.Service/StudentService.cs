using System;
using System.Collections.Generic;
using Day4.Models;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
	public class StudentService : IGenericService<StudentDto>
	{
		public void Add(StudentDto studentDto)
		{
			new StudentRepository().Add(studentDto);
		}

		public void Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			new StudentRepository().Delete(id);
		}

		public void Update(Guid? id, StudentDto studentDto)
		{
			if (!id.HasValue || studentDto == null) throw new ArgumentNullException();
			new StudentRepository().Update(id, studentDto);
		}

		public StudentDto Get(Guid? id)
		{
			return new StudentRepository().Get(id);
		}

		public IEnumerable<StudentDto> GetAll()
		{
			return new StudentRepository().GetAll();
		}
	}
}
