using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Day5.Models;
using Day5.DAL.Common;

namespace Day5.DAL
{
	public sealed class CourseDatabase : Database<Course>
	{
		public override async Task Add(Course course)
		{
			await Task.Factory.StartNew(() => {
				var dbConnection = DbConnection();
				dbConnection.Open();
				var idNotFound = false;

				course.Id ??= Guid.NewGuid();
				do
				{
					if (idNotFound) course.Id = Guid.NewGuid();
					idNotFound = Get(course.Id).Result.Tables[0].Rows.Count != 0;
				} while (idNotFound);

				const string statemenet = "INSERT INTO Course VALUES(@Id, @CourseName, @TeacherFirstName, @TeacherLastName, @Ects);";
				var command = new SqlCommand(statemenet, dbConnection);
				command.Parameters.AddWithValue("@Id", course.Id);
				command.Parameters.AddWithValue("@CourseName", course.CourseName);
				command.Parameters.AddWithValue("@TeacherFirstName", course.TeacherFirstName);
				command.Parameters.AddWithValue("@TeacherLastName", course.TeacherLastName);
				command.Parameters.AddWithValue("@Ects", course.Ects);

				DbCommand("Course", command);
				dbConnection.Close();
			});
		}

		public override async Task Delete(Guid? id)
		{
			await Task.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				const string statement = "DELETE FROM Course WHERE Id = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@Id", id);

				DbCommand("Course", command);
				dbConnection.Close();
			});
		}

		public override async Task Update(Guid? id, Course course)
		{
			await Task.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				var dataTable = Get(id).Result.Tables["Course"];
				course.CourseName ??= dataTable.Rows[0]["CourseName"].ToString();
				course.TeacherFirstName ??= dataTable.Rows[0]["TeacherFirstName"].ToString();
				course.TeacherLastName ??= dataTable.Rows[0]["TeacherLastName"].ToString();
				course.Ects ??= int.Parse(dataTable.Rows[0]["Ects"].ToString());

				const string statement =
					"UPDATE Course SET CourseName = @CourseName, TeacherFirstName = @TeacherFirstName, "
					+ "TeacherLastName = @TeacherLastName, Ects = @Ects WHERE Id = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@CourseName", course.CourseName);
				command.Parameters.AddWithValue("@TeacherFirstName", course.TeacherFirstName);
				command.Parameters.AddWithValue("@TeacherLastName", course.TeacherLastName);
				command.Parameters.AddWithValue("@Ects", course.Ects);
				command.Parameters.AddWithValue("@Id", id);

				DbCommand("Course", command);
				dbConnection.Close();
			});
		}

		public override async Task<DataSet> Get(Guid? id)
		{
			return await Task<DataSet>.Factory.StartNew(() =>
			{
				var dbConnection = DbConnection();
				dbConnection.Open();

				const string statement = "SELECT * From Course WHERE ID = @Id;";
				var command = new SqlCommand(statement, dbConnection);
				command.Parameters.AddWithValue("@Id", id);

				var dataSet = DbCommand("Course", command);
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

				const string statement = "SELECT * From Course;";
				var command = new SqlCommand(statement, dbConnection);
				var dataSet = DbCommand("Course", command);
				dbConnection.Close();

				return dataSet;
			});
		}
	}
}
