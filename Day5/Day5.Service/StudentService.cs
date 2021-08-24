using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models.Common;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
	public sealed class StudentService : IGenericService<StudentDto>
	{
		public async Task Add(StudentDto studentDto)
		{
			await new StudentRepository().Add(studentDto);
		}

		public async Task Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			await new StudentRepository().Delete(id);
		}

		public async Task Update(Guid? id, StudentDto studentDto)
		{
			if (!id.HasValue || studentDto == null) throw new ArgumentNullException();
			await new StudentRepository().Update(id, studentDto);
		}

		public async Task<StudentDto> Get(Guid? id)
		{
			return await new StudentRepository().Get(id);
		}

		public async Task<IEnumerable<StudentDto>> GetAll()
		{
			return await new StudentRepository().GetAll();
		}
	}
}
