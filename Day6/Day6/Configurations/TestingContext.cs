using System;
using Microsoft.EntityFrameworkCore;
using Day6.DAL;

namespace Day6.Configurations
{
	// Testing data
	public class TestingContext : DatabaseContext
	{
		public TestingContext(DbContextOptions options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var teacherIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
			var courseIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
			var studentIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

			modelBuilder.Entity<TeacherDb>().HasData(
				new TeacherDb()
				{
					Id = teacherIds[0],
					Department = "Data visualization",
					FirstName = "Richard",
					LastName = "Jameson"
				},
				new TeacherDb()
				{
					Id = teacherIds[1],
					Department = "Robotics",
					FirstName = "John",
					LastName = "Green"
				}
			);

			modelBuilder.Entity<CourseDb>().HasData(
				new CourseDb()
				{
					Id = courseIds[0],
					CourseName = "Programming 1",
					Ects = 4.5,
					TeacherId = teacherIds[1]
				},
				new CourseDb()
				{
					Id = courseIds[1],
					CourseName = "Data structures",
					Ects = 6,
					TeacherId = teacherIds[0]
				},
				new CourseDb()
				{
					Id = courseIds[2],
					CourseName = "Mathematics",
					Ects = 5,
					TeacherId = teacherIds[0]
				}
			);

			modelBuilder.Entity<StudentDb>().HasData(
				new StudentDb
				{
					Id = studentIds[0],
					FirstName = "Peter",
					LastName = "Johnson",
					Year = 2,
					Age = 21
				},
				new StudentDb
				{
					Id = studentIds[1],
					FirstName = "Gregory",
					LastName = "Armstrong",
					Year = 2,
					Age = 22
				},
				new StudentDb
				{
					Id = studentIds[2],
					FirstName = "Poe",
					LastName = "Wilson",
					Year = 1,
					Age = 20
				},
				new StudentDb
				{
					Id = studentIds[3],
					FirstName = "Luke",
					LastName = "Washington",
					Year = 3,
					Age = 25
				}
			);

			modelBuilder.Entity<EnrollmentDb>().HasData(
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[0],
					CourseId = courseIds[0]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[0],
					CourseId = courseIds[1]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[1],
					CourseId = courseIds[2]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[0]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[1]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[2]
				},
				new EnrollmentDb
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[3],
					CourseId = courseIds[1]
				}
			);
		}
	}
}
