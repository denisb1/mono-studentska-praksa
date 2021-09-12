using System;
using day9.DAL;
using day9.DAL.Common;
using Microsoft.EntityFrameworkCore;

namespace day9.API.Configurations
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

			modelBuilder.Entity<Teacher>().HasData(
				new Teacher()
				{
					Id = teacherIds[0],
					Department = "Data visualization",
					FirstName = "Richard",
					LastName = "Jameson"
				},
				new Teacher()
				{
					Id = teacherIds[1],
					Department = "Robotics",
					FirstName = "John",
					LastName = "Green"
				}
			);

			modelBuilder.Entity<Course>().HasData(
				new Course()
				{
					Id = courseIds[0],
					CourseName = "Programming 1",
					Ects = 4.5M,
					TeacherId = teacherIds[1]
				},
				new Course()
				{
					Id = courseIds[1],
					CourseName = "Data structures",
					Ects = 6M,
					TeacherId = teacherIds[0]
				},
				new Course()
				{
					Id = courseIds[2],
					CourseName = "Mathematics",
					Ects = 5,
					TeacherId = teacherIds[0]
				}
			);

			modelBuilder.Entity<Student>().HasData(
				new Student
				{
					Id = studentIds[0],
					FirstName = "Peter",
					LastName = "Johnson",
					Year = 2,
					Age = 21
				},
				new Student
				{
					Id = studentIds[1],
					FirstName = "Gregory",
					LastName = "Armstrong",
					Year = 2,
					Age = 22
				},
				new Student
				{
					Id = studentIds[2],
					FirstName = "Poe",
					LastName = "Wilson",
					Year = 1,
					Age = 20
				},
				new Student
				{
					Id = studentIds[3],
					FirstName = "Luke",
					LastName = "Washington",
					Year = 3,
					Age = 25
				}
			);

			modelBuilder.Entity<Enrollment>().HasData(
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[0],
					CourseId = courseIds[0]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[0],
					CourseId = courseIds[1]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[1],
					CourseId = courseIds[2]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[0]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[1]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[2],
					CourseId = courseIds[2]
				},
				new Enrollment
				{
					Id = Guid.NewGuid(),
					StudentId = studentIds[3],
					CourseId = courseIds[1]
				}
			);
		}
	}
}
