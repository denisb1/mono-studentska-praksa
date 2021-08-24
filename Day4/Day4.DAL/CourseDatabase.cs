using System;
using System.Data;
using Day4.DAL.Common;
using Day4.Models;
using Npgsql;

namespace Day4.DAL
{
	public sealed class CourseDatabase : Database<Course>
	{
		public override void Add(Course course)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statemenet =
				"INSERT INTO Course VALUES(@Id, @CourseName, @TeacherFirstName, @TeacherLastName, @Ects);";
			var command = new NpgsqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", course.Id);
			command.Parameters.AddWithValue("@CourseName", course.CourseName);
			command.Parameters.AddWithValue("@TeacherFirstName", course.TeacherFirstName);
			command.Parameters.AddWithValue("@TeacherLastName", course.TeacherLastName);
			command.Parameters.AddWithValue("@Ects", course.Ects);

			DbCommon("Course", command);
			dbConnection.Close();
		}

		public override void Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "DELETE FROM Course WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Course", command);
			dbConnection.Close();
		}

		public override void Update(Course course)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			var dataTable = Get(course.Id).Tables["Course"];
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

			DbCommon("Course", command);
			dbConnection.Close();
		}

		public override DataSet Get(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Course WHERE ID = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = DbCommon("Course", command);
			dbConnection.Close();

			return dataSet;
		}

		public override DataSet GetAll()
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Course;";
			var command = new NpgsqlCommand(statement, dbConnection);
			var dataSet = DbCommon("Course", command);
			dbConnection.Close();

			return dataSet;
		}
	}
}
