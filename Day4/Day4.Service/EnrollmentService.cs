using System;
using System.Collections.Generic;
using System.Linq;
using Day4.Models;
using Day4.Models.Common;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
	public class EnrollmentService : IGenericService<EnrollmentDto>
	{
		public void Add(EnrollmentDto enrollmentDto)
		{
			new EnrollmentRepository().Add(enrollmentDto);
		}

		public void Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			new EnrollmentRepository().Delete(id);
		}

		public void Update(Guid? id, EnrollmentDto enrollmentDto)
		{
			if (!id.HasValue || enrollmentDto == null) throw new ArgumentNullException();
			new EnrollmentRepository().Update(id, enrollmentDto);
		}

		public EnrollmentDto Get(Guid? id)
		{
			return new EnrollmentRepository().Get(id);
		}

		public IEnumerable<EnrollmentDto> GetAll()
		{
			return new EnrollmentRepository().GetAll();
		}

		public StudentCourse GetJoin(Guid id)
		{
			return new EnrollmentRepository().GetJoin(id);
		}

		public IEnumerable<StudentCourse> GetJoinAll()
		{
			return new EnrollmentRepository().GetJoinAll();
		}
	}
}
