using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Day3.Models;

namespace Day3.Database
{
    public static class StudentDatabase
    {
        public static void Add(Student student)
        {
            DatabaseHelper.GetInstance();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string statement = "INSERT INTO mono.dbo.Student(FName, LName, College, Age, CYear) VALUES(@FName, @LName, @College, @Age, @CYear);";
            using (var command = new SqlCommand(statement, connection))
            {
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
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string statement = "DELETE FROM mono.dbo.Student WHERE ID = @ID;";
            using (var command = new SqlCommand(statement, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static void Update(Guid id, Student student)
        {
            DatabaseHelper.GetInstance();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string statement = "UPDATE mono.dbo.Student SET FName = @FName, LName = @LName, College = @College, Age = @Age, CYear = @CYear WHERE ID = @ID;";
            using (var command = new SqlCommand(statement, connection))
            {
                command.Parameters.AddWithValue("@FName", student.FName);
                command.Parameters.AddWithValue("@LName", student.LName);
                command.Parameters.AddWithValue("@College", student.College);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@CYear", student.CYear);
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static IEnumerable<string> Get(Guid id)
        {
            DatabaseHelper.GetInstance();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);
            string[] student;

            connection.Open();
            const string query = "SELECT * FROM mono.dbo.Student WHERE ID = @ID;";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                var reader = command.ExecuteReader();

                if (reader.Read())
                    student = new[] {
                        reader["ID"].ToString(),
                        reader["FName"].ToString(),
                        reader["LName"].ToString(),
                        reader["College"].ToString(),
                        reader["Age"].ToString(), 
                        reader["CYear"].ToString()
                    };
                else
                    student = new[] { "empty" };
            }
            connection.Close();
            return student;
        }

        public static List<List<string>> GetAll()
        {
            DatabaseHelper.GetInstance();
            var students = new List<List<string>>();
            var connection = new SqlConnection(DatabaseHelper.ConnectionString);

            connection.Open();
            const string query = "SELECT * FROM mono.dbo.Student;";
            using (var command = new SqlCommand(query, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new List<string> {
                        reader["ID"].ToString(),
                        reader["FName"].ToString(),
                        reader["LName"].ToString(),
                        reader["College"].ToString(),
                        reader["Age"].ToString(), 
                        reader["CYear"].ToString()
                    });
                }

                if (students.Count == 0)
                    students.Add(new List<string> { "empty" });
            }
            connection.Close();
            return students;
        }
    }
}
