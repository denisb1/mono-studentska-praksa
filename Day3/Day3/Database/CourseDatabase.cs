using System;
using System.Collections.Generic;
using System.Data;
using Day3.Models;
using Microsoft.Data.Sqlite;

namespace Day3.Database
{
	public static class CourseDatabase
	{
		public static void Add(Course course)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "INSERT INTO Course VALUES(@Id, @Name);";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", course.Id);
				command.Parameters.AddWithValue("@Name", course.Name);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Remove(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "DELETE FROM Course WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Update(Guid id, Course course)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "UPDATE Course SET Name = @Name WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Name", course.Name);
				command.Parameters.AddWithValue("@Id", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static Course Get(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
			Course course;

			connection.Open();
			const string query = "SELECT Name FROM Course WHERE Id = @Id;";
			using (var command = new SqliteCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				var reader = command.ExecuteReader();
				course = reader.Read() ? ParseCourse(reader, id) : null;
			}

			connection.Close();
			return course;
		}

		public static IEnumerable<Course> GetAll()
		{
			DatabaseHelper.GetInstance();
			var courses = new List<Course>();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string query = "SELECT * FROM Course;";
			using (var command = new SqliteCommand(query, connection))
			{
				var reader = command.ExecuteReader();
				while (reader.Read()) courses.Add(ParseCourse(reader));
			}

			connection.Close();
			return courses;
		}

		private static Course ParseCourse(IDataRecord reader, Guid? id = null)
		{
			return new Course(
				id ?? Guid.Parse(reader["Id"].ToString() ?? string.Empty),
				new CourseDto(reader["Name"].ToString()
			));
		}
	}
}
