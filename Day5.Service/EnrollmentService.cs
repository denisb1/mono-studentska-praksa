using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day5.Models;
using Day5.Repository;
using Day5.Service.Common;

namespace Day5.Service
{
	public class EnrollmentService : IService<Enrollment>
	{
		public async Task Add(Enrollment enrollment)
		{
			var enrollmentRepository = new EnrollmentRepository();
			await enrollmentRepository.Add(enrollment);
		}

		public async Task Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();

			var enrollmentRepository = new EnrollmentRepository();
			await enrollmentRepository.Delete(id);
		}

		public async Task Update(Guid? id, Enrollment enrollment)
		{
			if (!id.HasValue || enrollment == null) throw new ArgumentNullException();

			var enrollmentRepository = new EnrollmentRepository();
			await enrollmentRepository.Update(id, enrollment);
		}

		public async Task<Enrollment> Get(Guid? id)
		{
			var enrollmentRepository = new EnrollmentRepository();
			return await enrollmentRepository.Get(id);
		}

		public async Task<List<Enrollment>> GetAll()
		{
			var enrollmentRepository = new EnrollmentRepository();
			return await enrollmentRepository.GetAll();
		}

		public async Task<EnrollmentJoin> GetJoined(Guid? id)
		{
			var enrollmentRepository = new EnrollmentRepository();
			return await enrollmentRepository.GetJoined(id);
		}

		public async Task<List<EnrollmentJoin>> GetAllJoined()
		{
			var enrollmentRepository = new EnrollmentRepository();
			return await enrollmentRepository.GetAllJoined();
		}
	}
}
