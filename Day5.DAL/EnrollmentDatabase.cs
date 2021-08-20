using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Day5.Models;
using Day5.DAL.Common;

namespace Day5.DAL
{
	public sealed class EnrollmentDatabase : Database<Enrollment>
	{
		public override async Task Add(Enrollment enrollment)
		{
			await Task.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();
				var idNotFound = false;

				enrollment.Id ??= Guid.NewGuid();
				do
				{
					if (idNotFound) enrollment.Id = Guid.NewGuid();
					idNotFound = Get(enrollment.Id).Result.Tables[0].Rows.Count != 0;
				} while (idNotFound);

				const string statemenet = "INSERT INTO Enrollment VALUES(@Id, @StudentId, @CourseId);";
				var command = new SqlCommand(statemenet, dbConnection);
				command.Parameters.AddWithValue("@Id", enrollment.Id);
				command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
				command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);

				DbCommand("Enrollment", command);
				dbConnection.Close();
			});
		}

		public override async Task Delete(Guid? id)
		{
			await Task.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				const string statement = "DELETE FROM Enrollment WHERE Id = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@Id", id);

				DbCommand("Enrollment", command);
				dbConnection.Close();
			});
		}

		public override async Task Update(Guid? id, Enrollment enrollment)
		{
			await Task.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				var dataTable = Get(id).Result.Tables["Enrollment"];
				enrollment.StudentId ??= Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
				enrollment.CourseId ??= Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());

				const string statement =
					"UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId WHERE Id = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
				command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
				command.Parameters.AddWithValue("@Id", id);

				DbCommand("Enrollment", command);
				dbConnection.Close();
			});
		}

		public override async Task<DataSet> Get(Guid? id)
		{
			return await Task<DataSet>.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				const string statement = "SELECT * From Enrollment WHERE ID = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@Id", id);

				var dataSet = DbCommand("Enrollment", command);
				dbConnection.Close();

				return dataSet;
			});
		}

		public override async Task<DataSet> GetAll()
		{
			return await Task<DataSet>.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				const string statement = "SELECT * From Enrollment;";
				var command = new SqlCommand(statement, dbConnection);
				var dataSet = DbCommand("Enrollment", command);
				dbConnection.Close();

				return dataSet;
			});
		}

		private static async Task<DataSet> GetJoinCommon(Guid? id, string sqlQery)
		{
			var query =
				"SELECT Enrollment.Id, Enrollment.StudentId, Enrollment.CourseId, "
				+ "Student.FirstName, Student.LastName, Student.College, Student.CollegeYear, Student.Age, "
				+ "Course.CourseName, Course.TeacherFirstName, Course.TeacherLastName, Course.Ects "
				+ "FROM Enrollment "
				+ "LEFT OUTER JOIN Student ON Enrollment.StudentId = Student.Id "
				+ "LEFT OUTER JOIN Course ON Enrollment.CourseId = Course.Id";

			if (sqlQery != null) query += sqlQery;
			else query += ";";

			return await Task<DataSet>.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();
				var command = new SqlCommand(query, dbConnection);

				if (id.HasValue) command.Parameters.AddWithValue("@Id", id);
				var dataSet = DbCommand("Enrollment", command);
				dbConnection.Close();

				return dataSet;
			});
		}

		public async Task<DataSet> GetJoined(Guid? id)
		{
			return await GetJoinCommon(id, " WHERE Enrollment.Id = @Id;");
		}

		public async Task<DataSet> GetAllJoined()
		{
			return await GetJoinCommon(null, null);
		}
	}
}
