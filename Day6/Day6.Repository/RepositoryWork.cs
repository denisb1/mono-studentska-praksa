using System;
using System.Threading.Tasks;
using Day6.DAL;

namespace Day6.Repository
{
	public class RepositoryWork : IRepositoryWork
	{
		private readonly DatabaseContext _context;
		private IGenericRepository<CourseDb> _courseDbRepository;
		private IGenericRepository<EnrollmentDb> _enrollmentDbRepository;
		private IGenericRepository<StudentDb> _studentDbRepository;
		private IGenericRepository<TeacherDb> _teacherDbRepository;

		public IGenericRepository<CourseDb> CourseDbRepository =>
			_courseDbRepository ??= new GenericRepository<CourseDb>(_context);

		public IGenericRepository<EnrollmentDb> EnrollmentDbRepository =>
			_enrollmentDbRepository ??= new GenericRepository<EnrollmentDb>(_context);

		public IGenericRepository<StudentDb> StudentDbRepository =>
			_studentDbRepository ??= new GenericRepository<StudentDb>(_context);

		public IGenericRepository<TeacherDb> TeacherDbRepository =>
			_teacherDbRepository ??= new GenericRepository<TeacherDb>(_context);

		public RepositoryWork(DatabaseContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
