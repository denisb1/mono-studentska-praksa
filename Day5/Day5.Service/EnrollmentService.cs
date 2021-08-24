using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models;
using Day5.Models.Common;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
	public sealed class EnrollmentService : IGenericService<EnrollmentDto>
	{
		public async Task Add(EnrollmentDto enrollmentDto)
		{
			await new EnrollmentRepository().Add(enrollmentDto);
		}

		public async Task Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			await new EnrollmentRepository().Delete(id);
		}

		public async Task Update(Guid? id, EnrollmentDto enrollmentDto)
		{
			if (!id.HasValue || enrollmentDto == null) throw new ArgumentNullException();
			await new EnrollmentRepository().Update(id, enrollmentDto);
		}

		public async Task<EnrollmentDto> Get(Guid? id)
		{
			return await new EnrollmentRepository().Get(id);
		}

		public async Task<IEnumerable<EnrollmentDto>> GetAll()
		{
			return await new EnrollmentRepository().GetAll();
		}

		public async Task<StudentCourse> GetJoin(Guid id)
		{
			return await new EnrollmentRepository().GetJoin(id);
		}

		public async Task<IEnumerable<StudentCourse>> GetJoinAll()
		{
			return await new EnrollmentRepository().GetJoinAll();
		}
	}
}
