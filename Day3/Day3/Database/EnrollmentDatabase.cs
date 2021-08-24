using System;
using System.Collections.Generic;
using System.Data;
using Day3.Models;
using Microsoft.Data.Sqlite;

namespace Day3.Database
{
	public static class EnrollmentDatabase
	{
		public static void Add(Enrollment enrollment)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "INSERT INTO Enrollment VALUES(@Id, @StudentId, @CourseId);";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", enrollment.Id);
				command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
				command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Remove(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "DELETE FROM Enrollment WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Update(Guid id, Enrollment enrollment)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement =
				"UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
				command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
				command.Parameters.AddWithValue("@Id", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static Enrollment Get(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
			Enrollment enrollment;

			connection.Open();
			const string query = "SELECT * FROM Enrollment WHERE Id = @Id;";
			using (var command = new SqliteCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				var reader = command.ExecuteReader();
				enrollment = reader.Read() ? ParseEnrollment(reader, id) : null;
			}
			connection.Close();
			return enrollment;
		}

		public static IEnumerable<Enrollment> GetAll()
		{
			DatabaseHelper.GetInstance();
			var enrollments = new List<Enrollment>();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string query = "SELECT * FROM Enrollment;";
			using (var command = new SqliteCommand(query, connection))
			{
				var reader = command.ExecuteReader();
				while (reader.Read()) enrollments.Add(ParseEnrollment(reader));
			}
			connection.Close();
			return enrollments;
		}

		private static Enrollment ParseEnrollment(IDataRecord reader, Guid? id = null)
		{
			return new Enrollment(
				id ?? Guid.Parse(reader["Id"].ToString() ?? string.Empty),
				new EnrollmentDto(
					Guid.Parse(reader["StudentId"].ToString() ?? string.Empty),
					Guid.Parse(reader["CourseId"].ToString() ?? string.Empty)
				)
			);
		}
	}
}
