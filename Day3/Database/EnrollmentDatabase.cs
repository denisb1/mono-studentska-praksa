using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Day3.Models;

namespace Day3.Database
{
	public static class EnrollmentDatabase
	{
		public static void Add(Enrollment enrollment)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "INSERT INTO mono.dbo.Enrollment(StudentID, CourseID) VALUES(@StudentID, @CourseID);";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@StudentID", enrollment.StudentID);
				command.Parameters.AddWithValue("@CourseID", enrollment.CourseID);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Remove(Guid id)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "DELETE FROM mono.dbo.Enrollment WHERE ID = @ID;";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@ID", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static void Update(Guid id, Enrollment enrollment)
		{
			DatabaseHelper.GetInstance();
			var connection = new SqlConnection(DatabaseHelper.ConnectionString);

			connection.Open();
			const string statement = "UPDATE mono.dbo.Enrollment SET StudentID = @StudentID, CourseID = @CourseID WHERE ID = @ID;";
			using (var command = new SqlCommand(statement, connection))
			{
				command.Parameters.AddWithValue("@StudentID", enrollment.StudentID);
				command.Parameters.AddWithValue("@CourseID", enrollment.CourseID);
				command.Parameters.AddWithValue("@ID", id);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

		public static IEnumerable<string> Get(Guid id)
        {
	        DatabaseHelper.GetInstance();
	        var connection = new SqlConnection(DatabaseHelper.ConnectionString);
	        string[] enrollment;

	        connection.Open();
	        const string query = "SELECT e.ID AS 'eid', s.ID AS 'sid', c.ID AS 'cid', s.FName, s.LName, s.Age, s.College, s.CYear, c.Name AS 'CName' FROM mono.dbo.Enrollment AS e, mono.dbo.Student AS s, mono.dbo.Course AS C WHERE e.StudentID = s.ID AND e.CourseID = c.ID AND e.ID = @ID;";
	        using (var command = new SqlCommand(query, connection))
	        {
		        command.Parameters.AddWithValue("@ID", id);
		        var reader = command.ExecuteReader();
		        enrollment = reader.Read()
			        ? new[] {
				        reader["eid"].ToString(),
				        reader["sid"].ToString(),
				        reader["cid"].ToString(),
				        reader["FName"].ToString(),
				        reader["LName"].ToString(),
				        reader["Age"].ToString(),
				        reader["College"].ToString(),
				        reader["CYear"].ToString(),
				        reader["CName"].ToString() }
			        : new[] {"empty"};
	        }
	        connection.Close();
	        return enrollment;
        }

        public static List<List<string>> GetAll()
        {
            DatabaseHelper.GetInstance();
            var enrollments = new List<List<string>>();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string query = "SELECT e.ID AS 'eid', s.ID AS 'sid', c.ID AS 'cid', s.FName, s.LName, s.Age, s.College, s.CYear, c.Name AS 'CName' FROM mono.dbo.Enrollment AS e, mono.dbo.Student AS s, mono.dbo.Course AS C WHERE e.StudentID = s.ID AND e.CourseID = c.ID;";
            using (var command = new SqlCommand(query, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    enrollments.Add(new List<string> {
	                    reader["eid"].ToString(),
	                    reader["sid"].ToString(),
	                    reader["cid"].ToString(),
	                    reader["FName"].ToString(),
	                    reader["LName"].ToString(),
	                    reader["Age"].ToString(),
	                    reader["College"].ToString(),
	                    reader["CYear"].ToString(),
	                    reader["CName"].ToString(),
                    });
                }

                if (enrollments.Count == 0)
                    enrollments.Add(new List<string> { "empty" });
            }
            connection.Close();
            return enrollments;
        }
	}
}
