using System;
using System.Data;
using System.Data.SqlClient;

namespace Day4.DAL.Common
{
	public abstract class Database<T> where T : class
	{
		protected static DataSet DbCommon(string tableName, SqlCommand command)
		{
			var adapter = new SqlDataAdapter();
			var dataSet = new DataSet(tableName);

			adapter.TableMappings.Add("Table", tableName);
			command.CommandType = CommandType.Text;
			adapter.SelectCommand = command;
			adapter.Fill(dataSet);

			return dataSet;
		}

		protected static SqlConnection DbConnection()
		{
			return new SqlConnection(DatabaseHelper.GetInstance().ConnectionString);
		}

		public abstract void Add(T obj);
		public abstract void Delete(Guid? id);
		public abstract void Update(Guid? id, T obj);
		public abstract DataSet Get(Guid? id);
		public abstract DataSet GetAll();
	}
}
