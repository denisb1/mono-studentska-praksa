using System;
using System.Collections.Generic;
using System.Data;
using Day3.Models;
using Microsoft.Data.Sqlite;

namespace Day3.Database
{
	public static class StudentDatabase
	{
		public static void Add(Student student)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "INSERT INTO Student VALUES(@Id, @FName, @LName, @College, @Age, @CYear);";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", student.Id);
				command.Parameters.AddWithValue("@FName", student.FName);
				command.Parameters.AddWithValue("@LName", student.LName);
				command.Parameters.AddWithValue("@College", student.College);
				command.Parameters.AddWithValue("@Age", student.Age);
				command.Parameters.AddWithValue("@CYear", student.CYear);

				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Remove(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "DELETE FROM Student WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Update(Guid id, StudentDto student)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement =
				"UPDATE Student SET FName = @FName, LName = @LName, College"
				+ " = @College, Age = @Age, CYear = @CYear WHERE Id = @Id;";
			using (var command = new SqliteCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@FName", student.FName);
				command.Parameters.AddWithValue("@LName", student.LName);
				command.Parameters.AddWithValue("@College", student.College);
				command.Parameters.AddWithValue("@Age", student.Age);
				command.Parameters.AddWithValue("@CYear", student.CYear);
				command.Parameters.AddWithValue("@Id", id);

				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static Student Get(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
			Student student;

			connection.Open();
			const string query = "SELECT * FROM Student WHERE Id = @Id;";
			using (var command = new SqliteCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Id", id);
				var reader = command.ExecuteReader();
				student = reader.Read() ? ParseStudent(reader, id) : null;
			}

			connection.Close();
			return student;
		}

		public static IEnumerable<Student> GetAll()
		{
			DatabaseHelper.GetInstance();
			var students = new List<Student>();
			var connection = new SqliteConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string query = "SELECT * FROM Student;";
			using (var command = new SqliteCommand(query, connection))
			{
				var reader = command.ExecuteReader();
				while (reader.Read()) students.Add(ParseStudent(reader));
			}

			connection.Close();
			return students;
		}

		private static Student ParseStudent(IDataRecord reader, Guid? id = null)
		{
			return new Student(
				id ?? Guid.Parse(reader["Id"].ToString() ?? string.Empty),
				new StudentDto(
					reader["FName"].ToString(),
					reader["LName"].ToString(),
					reader["College"].ToString(),
					int.Parse(reader["Age"].ToString() ?? string.Empty),
					int.Parse(reader["CYear"].ToString() ?? string.Empty)
				)
			);
		}
	}
}
