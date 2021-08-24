using System;
using System.Data;
using System.Threading.Tasks;
using Day5.DAL.Common;
using Day5.Models;
using Npgsql;

namespace Day5.DAL
{
	public sealed class StudentDatabase : Database<Student>
	{
		public override async Task Add(Student student)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statemenet = "INSERT INTO Student VALUES(@Id, @FirstName, @LastName, @College, @CollegeYear, @Age);";
			var command = new NpgsqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", student.Id);
			command.Parameters.AddWithValue("@FirstName", student.FirstName);
			command.Parameters.AddWithValue("@LastName", student.LastName);
			command.Parameters.AddWithValue("@College", student.College);
			command.Parameters.AddWithValue("@CollegeYear", student.CollegeYear);
			command.Parameters.AddWithValue("@Age", student.Age);

			await DbCommon("Student", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "DELETE FROM Student WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			await DbCommon("Student", command);
			await dbConnection.CloseAsync();
		}

		public override async Task Update(Student student)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			var dt = await Get(student.Id);
			var dataTable = dt.Tables["Course"];
			student.FirstName ??= dataTable.Rows[0]["FirstName"].ToString();
			student.LastName ??= dataTable.Rows[0]["LastName"].ToString();
			student.College ??= dataTable.Rows[0]["College"].ToString();
			student.CollegeYear ??= int.Parse(dataTable.Rows[0]["CollegeYear"].ToString());
			student.Age ??= int.Parse(dataTable.Rows[0]["Age"].ToString());

			const string statement = "UPDATE Student SET FirstName = @FirstName, LastName = @LastName, "
						+ "College = @College, CollegeYear = @CollegeYear, Age = @Age WHERE Id = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@FirstName", student.FirstName);
			command.Parameters.AddWithValue("@LastName", student.LastName);
			command.Parameters.AddWithValue("@College", student.College);
			command.Parameters.AddWithValue("@CollegeYear", student.CollegeYear);
			command.Parameters.AddWithValue("@Age", student.Age);
			command.Parameters.AddWithValue("@Id", student.Id);

			await DbCommon("Student", command);
			await dbConnection.CloseAsync();
		}

		public override async Task<DataSet> Get(Guid? id)
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Student WHERE ID = @Id;";
			var command = new NpgsqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = await DbCommon("Student", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}

		public override async Task<DataSet> GetAll()
		{
			var dbConnection = DbConnection();
			await dbConnection.OpenAsync();

			const string statement = "SELECT * From Student;";
			var command = new NpgsqlCommand(statement, dbConnection);
			var dataSet = await DbCommon("Student", command);
			await dbConnection.CloseAsync();

			return dataSet;
		}
	}
}
