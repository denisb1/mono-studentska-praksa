using System;
using System.Data;
using Day4.DAL.Common;
using Day4.Models;
using Npgsql;

namespace Day4.DAL
{
	public sealed class EnrollmentDatabase : Database<Enrollment>
	{
		public override void Add(Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statemenet =
				"INSERT INTO Enrollment VALUES(@Id, @StudentId, @CourseId);";
			var command = new NpgsqlCommand(statemenet, dbConnection);
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
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Enrollment", command);
			dbConnection.Close();
		}

		public override void Update(Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			var dataTable = Get(enrollment.Id).Tables["Enrollment"];
			enrollment.StudentId ??= Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
			enrollment.CourseId ??= Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());

			const string statement =
				"UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
			command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
			command.Parameters.AddWithValue("@Id", enrollment.Id);

			DbCommon("Enrollment", command);
			dbConnection.Close();
		}

		public override DataSet Get(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Enrollment WHERE ID = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = DbCommon("Enrollment", command);
			dbConnection.Close();

			return dataSet;
		}

		public override DataSet GetAll()
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Enrollment;";
			var command = new NpgsqlCommand(statement, dbConnection);
			var dataSet = DbCommon("Enrollment", command);
			dbConnection.Close();

			return dataSet;
		}

		public DataSet GetJoin(Guid? id)
		{
			return GetJoinCommon(id, " WHERE Enrollment.Id = @Id;");
		}

		public DataSet GetJoinAll()
		{
			return GetJoinCommon();
		}

		private DataSet GetJoinCommon(Guid? id = null, string where = null)
		{
			var statement = "SELECT S.FirstName, S.LastName, S.College, "
					+ "S.CollegeYear, C.CourseName, C.Ects FROM Enrollment "
					+ "LEFT OUTER JOIN Student S ON Enrollment.StudentId = S.Id "
					+ "LEFT OUTER JOIN Course C on Enrollment.CourseId = C.Id";

			if (where != null) statement += where;
			else statement += ";";

			var dbConnection = DbConnection();
			dbConnection.Open();
			var command = new NpgsqlCommand(statement, dbConnection);

			if (id.HasValue) command.Parameters.AddWithValue("@Id", id);
			var dataset = DbCommon("Enrollment", command);
			dbConnection.Close();

			return dataset;
		}
	}
}
