using System;
using System.Data;
using System.Threading.Tasks;
using Day5.DAL.Common;
using Day5.Models;
using Npgsql;

namespace Day5.DAL
{
	public sealed class CourseDatabase : Database<Course>
	{
		public override async Task Add(Course course)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statemenet =
				"INSERT INTO Course VALUES(@Id, @CourseName, @TeacherFirstName, @TeacherLastName, @Ects);";
			var command = new NpgsqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", course.Id);
			command.Parameters.AddWithValue("@CourseName", course.CourseName);
			command.Parameters.AddWithValue("@TeacherFirstName", course.TeacherFirstName);
			command.Parameters.AddWithValue("@TeacherLastName", course.TeacherLastName);
			command.Parameters.AddWithValue("@Ects", course.Ects);

			await DbCommon("Course", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "DELETE FROM Course WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			await DbCommon("Course", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Update(Course course)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			var dt = await Get(course.Id);
			var dataTable = dt.Tables["Course"];
			course.CourseName ??= dataTable.Rows[0]["CourseName"].ToString();
			course.TeacherFirstName ??= dataTable.Rows[0]["TeacherFirstName"].ToString();
			course.TeacherLastName ??= dataTable.Rows[0]["TeacherLastName"].ToString();
			course.Ects ??= int.Parse(dataTable.Rows[0]["Ects"].ToString());

			const string statement = "UPDATE Course SET CourseName = @CourseName, TeacherFirstName = @TeacherFirstName, "
			                         + "TeacherLastName = @TeacherLastName, Ects = @Ects WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@CourseName", course.CourseName);
			command.Parameters.AddWithValue("@TeacherFirstName", course.TeacherFirstName);
			command.Parameters.AddWithValue("@TeacherLastName", course.TeacherLastName);
			command.Parameters.AddWithValue("@Ects", course.Ects);
			command.Parameters.AddWithValue("@Id", course.Id);

			await DbCommon("Course", command);
			await dbConnection.CloseAsync();
		}

		public override async Task<DataSet> Get(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Course WHERE ID = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = await DbCommon("Course", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}

		public override async Task<DataSet> GetAll()
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Course;";
			var command = new NpgsqlCommand(statement, dbConnection);
			var dataSet = await DbCommon("Course", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}
	}
}
