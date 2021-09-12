using System;
using System.Threading.Tasks;
using day9.DAL;

namespace day9.Repository.Common
{
	public interface IRepositoryWork : IDisposable
	{
		IGenericRepository<Course> CourseRepository { get; }
		IGenericRepository<Enrollment> EnrollmentRepository { get; }
		IGenericRepository<Student> StudentRepository { get; }
		IGenericRepository<Teacher> TeacherRepository { get; }

		Task Save();
	}
}
