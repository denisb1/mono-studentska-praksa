using System;
using System.Threading.Tasks;
using day9.DAL;
using day9.DAL.Common;
using day9.Repository.Common;

namespace day9.Repository
{
	public class RepositoryWork : IRepositoryWork
	{
		private readonly DatabaseContext _context;
		private IGenericRepository<Course> _courseRepository;
		private IGenericRepository<Enrollment> _enrollmentRepository;
		private IGenericRepository<Student> _studentRepository;
		private IGenericRepository<Teacher> _teacherRepository;

		public IGenericRepository<Course> CourseRepository =>
			_courseRepository ??= new GenericRepository<Course>(_context);

		public IGenericRepository<Enrollment> EnrollmentRepository =>
			_enrollmentRepository ??= new GenericRepository<Enrollment>(_context);

		public IGenericRepository<Student> StudentRepository =>
			_studentRepository ??= new GenericRepository<Student>(_context);

		public IGenericRepository<Teacher> TeacherRepository =>
			_teacherRepository ??= new GenericRepository<Teacher>(_context);

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
