using System;
using System.Collections.Generic;
using Day4.Models;
using Day4.Repository;
using Day4.Service.Common;

namespace Day4.Service
{
	public class EnrollmentService : IService<Enrollment>
	{
		public void Add(Enrollment enrollment)
		{
			new EnrollmentRepository().Add(enrollment);
		}

		public void Delete(Guid? id)
		{
			if (!id.HasValue) throw new ArgumentNullException();
			new EnrollmentRepository().Delete(id);
		}

		public void Update(Guid? id, Enrollment enrollment)
		{
			if (!id.HasValue || enrollment == null) throw new ArgumentNullException();
			new EnrollmentRepository().Update(id, enrollment);
		}

		public Enrollment Get(Guid? id)
		{
			return new EnrollmentRepository().Get(id);
		}

		public IEnumerable<string> GetJoined(Guid? id)
		{
			return new EnrollmentRepository().GetJoined(id);
		}

		public List<Enrollment> GetAll()
		{
			return new EnrollmentRepository().GetAll();
		}

		public List<List<string>> GetAllJoined()
		{
			return new EnrollmentRepository().GetAllJoined();
		}
	}
}
