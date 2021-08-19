using System;
using System.Data;
using System.Data.SqlClient;
using Day4.DAL.Common;
using Day4.Models;

namespace Day4.DAL
{
	public sealed class StudentDatabase : Database<Student>
	{
		public override void Add(Student student)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();
			var idNotFound = false;

			student.Id ??= Guid.NewGuid();
			do
			{
				if (idNotFound) student.Id = Guid.NewGuid();
				idNotFound = Get(student.Id).Tables[0].Rows.Count != 0;
			} while (idNotFound);

			const string statemenet = "INSERT INTO Student VALUES(@Id, @FirstName, @LastName, @College, @CollegeYear, @Age);";
			var command = new SqlCommand(statemenet, dbConnection);
			command.Parameters.AddWithValue("@Id", student.Id);
			command.Parameters.AddWithValue("@FirstName", student.FirstName);
			command.Parameters.AddWithValue("@LastName", student.LastName);
			command.Parameters.AddWithValue("@College", student.College);
			command.Parameters.AddWithValue("@CollegeYear", student.CollegeYear);
			command.Parameters.AddWithValue("@Age", student.Age);

			DbCommon("Student", command);
			dbConnection.Close();
		}

		public override void Delete(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "DELETE FROM Student WHERE Id = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Student", command);
			dbConnection.Close();
		}

		public override void Update(Guid? id, Student student)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			var dataTable = Get(id).Tables["Student"];
			student.FirstName ??= dataTable.Rows[0]["FirstName"].ToString();
			student.LastName ??= dataTable.Rows[0]["LastName"].ToString();
			student.College ??= dataTable.Rows[0]["College"].ToString();
			student.CollegeYear ??= int.Parse(dataTable.Rows[0]["CollegeYear"].ToString());
			student.Age ??= int.Parse(dataTable.Rows[0]["Age"].ToString());

			const string statement = "UPDATE Student SET FirstName = @FirstName, LastName = @LastName, "
									+ "College = @College, CollegeYear = @CollegeYear, Age = @Age WHERE Id = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@FirstName", student.FirstName);
			command.Parameters.AddWithValue("@LastName", student.LastName);
			command.Parameters.AddWithValue("@College", student.College);
			command.Parameters.AddWithValue("@CollegeYear", student.CollegeYear);
			command.Parameters.AddWithValue("@Age", student.Age);
			command.Parameters.AddWithValue("@Id", id);

			DbCommon("Student", command);
			dbConnection.Close();
		}

		public override DataSet Get(Guid? id)
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Student WHERE ID = @Id;";
			var command = new SqlCommand(statement, dbConnection);
			command.Parameters.AddWithValue("@Id", id);

			var dataSet = DbCommon("Student", command);
			dbConnection.Close();

			return dataSet;
		}

		public override DataSet GetAll()
		{
			var dbConnection = DbConnection();
			dbConnection.Open();

			const string statement = "SELECT * From Student;";
			var command = new SqlCommand(statement, dbConnection);
			var dataSet = DbCommon("Student", command);
			dbConnection.Close();

			return dataSet;
		}
	}
}
