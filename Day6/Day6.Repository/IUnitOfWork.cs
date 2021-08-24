using System;
using System.Threading.Tasks;
using Day6.DAL;

namespace Day6.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<CourseDb> CourseDbRepository { get; }
		IGenericRepository<EnrollmentDb> EnrollmentDbRepository { get; }
		IGenericRepository<StudentDb> StudentDbRepository { get; }
		IGenericRepository<TeacherDb> TeacherDbRepository { get; }

		Task Save();
	}
}
