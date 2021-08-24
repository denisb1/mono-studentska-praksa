using System;
using System.Data;
using System.Threading.Tasks;
using Day5.DAL.Common;
using Day5.Models;
using Npgsql;

namespace Day5.DAL
{
	public sealed class EnrollmentDatabase : Database<Enrollment>
	{
		public override async Task Add(Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statemenet =
				"INSERT INTO Enrollment VALUES(@Id, @StudentId, @CourseId);";
			var command = new NpgsqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", enrollment.Id);
			command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
			command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);

			await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "DELETE FROM Enrollment WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Update(Enrollment enrollment)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			var dt = await Get(enrollment.Id);
			var dataTable = dt.Tables["Enrollment"];
			enrollment.StudentId ??= Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
			enrollment.CourseId ??= Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());

			const string statement =
				"UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
			command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
			command.Parameters.AddWithValue("@Id", enrollment.Id);

			await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();
		}

		public override async Task<DataSet> Get(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Enrollment WHERE ID = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}

		public override async Task<DataSet> GetAll()
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Enrollment;";
			var command = new NpgsqlCommand(statement, dbConnection);
			var dataSet = await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}

		public async Task<DataSet> GetJoin(Guid? id)
		{
			return await GetJoinCommon(id, " WHERE Enrollment.Id = @Id;");
		}

		public async Task<DataSet> GetJoinAll()
		{
			return await GetJoinCommon();
		}

		private async Task<DataSet> GetJoinCommon(Guid? id = null, string where = null)
		{
			var statement = "SELECT S.FirstName, S.LastName, S.College, "
					+ "S.CollegeYear, C.CourseName, C.Ects FROM Enrollment "
					+ "LEFT OUTER JOIN Student S ON Enrollment.StudentId = S.Id "
					+ "LEFT OUTER JOIN Course C on Enrollment.CourseId = C.Id";

			if (where != null) statement += where;
			else statement += ";";

			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();
			var command = new NpgsqlCommand(statement, dbConnection);

			if (id.HasValue) command.Parameters.AddWithValue("@Id", id);
			var dataset = await DbCommon("Enrollment", command);
			await dbConnection.CloseAsync();

			return dataset;
		}
	}
}
