using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Day5.DAL;
using Day5.Models;
using Day5.Models.Common;
using Day5.Repository.Common;

namespace Day5.Repository
{
	public sealed class EnrollmentRepository : IRepository<Enrollment>
	{
		public async Task Add(Enrollment enrollment)
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			await enrollmentDatabase.Add(enrollment);
		}

		public async Task Delete(Guid? id)
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			await enrollmentDatabase.Delete(id);
		}

		public async Task Update(Guid? id, Enrollment enrollment)
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			await enrollmentDatabase.Update(id, enrollment);
		}

		public async Task<Enrollment> Get(Guid? id)
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			var dataSet = await enrollmentDatabase.Get(id);
			var dataTable = dataSet.Tables["Enrollment"];

			return new Enrollment(
				Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
				Guid.Parse(dataTable.Rows[0]["StudentId"].ToString()),
				Guid.Parse(dataTable.Rows[0]["CourseId"].ToString())
			);
		}

		public async Task<List<Enrollment>> GetAll()
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			var dataSet = await enrollmentDatabase.GetAll();
			var dataTable = dataSet.Tables["Enrollment"];

			return (from DataRow dataRow in dataTable.Rows select new Enrollment(
						Guid.Parse(dataRow["Id"].ToString()),
						Guid.Parse(dataRow["StudentId"].ToString()),
						Guid.Parse(dataRow["CourseId"].ToString())
					)
				)
				.ToList();
		}

		public async Task<EnrollmentJoin> GetJoined(Guid? id)
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			var dataSet = await enrollmentDatabase.GetJoined(id);
			var dataTable = dataSet.Tables["Enrollment"];

			var enrollment = new Enrollment(
				Guid.Parse(dataTable.Rows[0]["Id"].ToString()),
				Guid.Parse(dataTable.Rows[0]["StudentId"].ToString()),
				Guid.Parse(dataTable.Rows[0]["CourseId"].ToString())
			);
			var student = new Student(
				enrollment.StudentId,
				new Name(dataTable.Rows[0]["FirstName"].ToString(), dataTable.Rows[0]["LastName"].ToString()),
				dataTable.Rows[0]["College"].ToString(),
				int.Parse(dataTable.Rows[0]["CollegeYear"].ToString()),
				int.Parse(dataTable.Rows[0]["Age"].ToString())
			);
			var course = new Course(
				enrollment.CourseId,
				dataTable.Rows[0]["CourseName"].ToString(),
				new Name(dataTable.Rows[0]["TeacherFirstName"].ToString(), dataTable.Rows[0]["TeacherLastName"].ToString()),
				double.Parse(dataTable.Rows[0]["Ects"].ToString())
			);

			return new EnrollmentJoin(enrollment, student, course);
		}

		public async Task<List<EnrollmentJoin>> GetAllJoined()
		{
			var enrollmentDatabase = new EnrollmentDatabase();
			var dataSet = await enrollmentDatabase.GetAllJoined();
			var dataTable = dataSet.Tables["Enrollment"];

			return (from DataRow dataRow in dataTable.Rows select new EnrollmentJoin(
						new Enrollment(
							Guid.Parse(dataRow["Id"].ToString()),
							Guid.Parse(dataRow["StudentId"].ToString()),
							Guid.Parse(dataRow["CourseId"].ToString())),
						new Student(
							Guid.Parse(dataRow["Id"].ToString()),
							new Name(dataTable.Rows[0]["FirstName"].ToString(), dataTable.Rows[0]["LastName"].ToString()),
							dataTable.Rows[0]["College"].ToString(),
							int.Parse(dataTable.Rows[0]["CollegeYear"].ToString()),
							int.Parse(dataTable.Rows[0]["Age"].ToString())),
						new Course(
							Guid.Parse(dataRow["Id"].ToString()),
							dataTable.Rows[0]["CourseName"].ToString(),
							new Name(dataTable.Rows[0]["TeacherFirstName"].ToString(), dataTable.Rows[0]["TeacherLastName"].ToString()),
							double.Parse(dataTable.Rows[0]["Ects"].ToString()))
					)).ToList();
		}
	}
}
