using Microsoft.EntityFrameworkCore;

namespace Day6.DAL
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CourseDb>()
				.HasOne(c => c.TeacherDb)
				.WithMany(t => t.CourseDbs);

			modelBuilder.Entity<EnrollmentDb>()
				.HasKey(e => new { e.StudentId, e.CourseId });

			modelBuilder.Entity<EnrollmentDb>()
				.HasOne(e => e.CourseDb)
				.WithMany(c => c.EnrollmentDbs)
				.HasForeignKey(e => e.CourseId);

			modelBuilder.Entity<EnrollmentDb>()
				.HasOne(e => e.StudentDb)
				.WithMany(s => s.EnrollmentDbs)
				.HasForeignKey(e => e.StudentId);
		}

		public DbSet<CourseDb> CourseDbs { get; set; }
		public DbSet<StudentDb> StudentDbs { get; set; }
		public DbSet<TeacherDb> TeacherDbs { get; set; }
	}
}
