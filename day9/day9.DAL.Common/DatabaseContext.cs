using Microsoft.EntityFrameworkCore;

namespace day9.DAL.Common
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Course>()
				.HasOne(c => c.Teacher)
				.WithMany(t => t.Courses);

			modelBuilder.Entity<Enrollment>()
				.HasKey(e => new { e.StudentId, e.CourseId });

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Course)
				.WithMany(c => c.Enrollments)
				.HasForeignKey(e => e.CourseId);

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Student)
				.WithMany(s => s.Enrollments)
				.HasForeignKey(e => e.StudentId);
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
	}
}
