using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Day3.Models;

namespace Day3.Database
{
	public static class CourseDatabase
	{
		public static void Add(Course course)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "INSERT INTO mono.dbo.Course(Name) VALUES(@Name);";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Name", course.Name);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Remove(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "DELETE FROM mono.dbo.Course WHERE ID = @ID;";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@ID", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Update(Guid id, Course course)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "UPDATE mono.dbo.Course SET Name = @Name WHERE ID = @ID;";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@Name", course.Name);
				command.Parameters.AddWithValue("@ID", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static IEnumerable<string> Get(Guid id)
        {
            DatabaseHelper.GetInstance();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);
            string[] course;

            connection.Open();
            const string query = "SELECT Name FROM mono.dbo.Course WHERE ID = @ID;";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                var reader = command.ExecuteReader();
                course = reader.Read() ? new[] { id.ToString(), reader["Name"].ToString() } : new[] {"empty"};
            }
            connection.Close();
            return course;
        }

        public static List<List<string>> GetAll()
        {
            DatabaseHelper.GetInstance();
            var courses = new List<List<string>>();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string query = "SELECT * FROM mono.dbo.Course;";
            using (var command = new SqlCommand(query, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new List<string> {
                        reader["ID"].ToString(),
                        reader["Name"].ToString(),
                    });
                }

                if (courses.Count == 0)
                    courses.Add(new List<string> { "empty" });
            }
            connection.Close();
            return courses;
        }
	}
}
