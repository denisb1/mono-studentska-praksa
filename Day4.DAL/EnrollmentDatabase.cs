using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Day4.DAL.Common;
using Day4.Models;

namespace Day4.DAL
{
	public sealed class EnrollmentDatabase : Database<Enrollment>
	{
		public override void Add(Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();
			var idNotFound = false;

			enrollment.Id ??= Guid.NewGuid();
			do
			{
				if (idNotFound) enrollment.Id = Guid.NewGuid();
				idNotFound = Get(enrollment.Id).Tables[0].Rows.Count != 0;
			} while (idNotFound);

			const string statemenet = "INSERT INTO Enrollment VALUES(@Id, @StudentId, @CourseId);";
			var command = new SqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", enrollment.Id);
			command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
			command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);

			DbCommon("Enrollment", command);
			dbConnection.Close();
		}

		public override void Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "DELETE FROM Enrollment WHERE Id = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Enrollment", command);
			dbConnection.Close();
		}

		public override void Update(Guid? id, Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			var dataTable = Get(id).Tables["Enrollment"];
			enrollment.StudentId ??= Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
			enrollment.CourseId ??= Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());

			const string statement = "UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId WHERE Id = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
			command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Enrollment", command);
			dbConnection.Close();
		}

		public override DataSet Get(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Enrollment WHERE ID = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = DbCommon("Enrollment", command);
			dbConnection.Close();

			return dataSet;
		}

		public IEnumerable<string> GetJoined(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();
			string[] enrollment;

			const string query =
				"SELECT e.Id AS 'Id', s.Id AS 'StudentId', c.Id AS 'CourseId', "
				+ "s.FirstName, s.LastName, s.College, s.CollegeYear, s.Age, "
				+ "c.CourseName, c.TeacherFirstName, c.TeacherLastName, c.Ects "
				+ "FROM Enrollment AS e, Student AS s, Course AS c "
				+ "WHERE e.StudentId = s.Id AND e.CourseId = c.Id AND e.Id = @Id;";
			using (var command = new SqlCommand(query, dbConnection))
			{
				command.Parameters.AddWithValue("@Id", id);
				var reader = command.ExecuteReader();
				enrollment = reader.Read()
					? new[] {
						reader["Id"].ToString(),
						reader["StudentId"].ToString(),
						reader["CourseId"].ToString(),
						reader["FirstName"].ToString(),
						reader["LastName"].ToString(),
						reader["College"].ToString(),
						reader["CollegeYear"].ToString(),
						reader["Age"].ToString(),
						reader["TeacherFirstName"].ToString(),
						reader["TeacherLastName"].ToString(),
						reader["Ects"].ToString() }
					: null;
			}

			dbConnection.Close();
			return enrollment;
		}

		public override DataSet GetAll()
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Enrollment;";
			var command = new SqlCommand(statement, dbConnection);
			var dataSet = DbCommon("Enrollment", command);
			dbConnection.Close();

			return dataSet;
		}

		public List<List<string>> GetAllJoined()
		{
			var dbConnection = DbConnection();
			dbConnection.Open();
			var enrollments = new List<List<string>>();

			const string query =
				"SELECT e.Id AS 'Id', s.Id AS 'StudentId', c.Id AS 'CourseId', "
				+ "s.FirstName, s.LastName, s.College, s.CollegeYear, s.Age, "
				+ "c.CourseName, c.TeacherFirstName, c.TeacherLastName, c.Ects "
				+ "FROM Enrollment AS e, Student AS s, Course AS c "
				+ "WHERE e.StudentId = s.Id AND e.CourseId = c.Id";
			using (var command = new SqlCommand(query, dbConnection))
			{
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					enrollments.Add(new List<string> {
						reader["Id"].ToString(),
						reader["StudentId"].ToString(),
						reader["CourseId"].ToString(),
						reader["FirstName"].ToString(),
						reader["LastName"].ToString(),
						reader["College"].ToString(),
						reader["CollegeYear"].ToString(),
						reader["Age"].ToString(),
						reader["TeacherFirstName"].ToString(),
						reader["TeacherLastName"].ToString(),
						reader["Ects"].ToString()
					});
				}

				if (enrollments.Count == 0) enrollments = null;
			}

			dbConnection.Close();
			return enrollments;
		}
	}
}
