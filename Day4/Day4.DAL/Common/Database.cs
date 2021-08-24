using System;
using System.Data;
using Npgsql;

namespace Day4.DAL.Common
{
	public abstract class Database<T> where T : class
	{
		protected static DataSet DbCommon(string tableName, NpgsqlCommand command)
		{
			var adapter = new NpgsqlDataAdapter();
			var dataSet = new DataSet(tableName);

			adapter.TableMappings.Add("Table", tableName);
			command.CommandType = CommandType.Text;
			adapter.SelectCommand = command;
			adapter.Fill(dataSet);

			return dataSet;
		}

		protected static NpgsqlConnection DbConnection()
		{
			return new NpgsqlConnection(DatabaseHelper.GetInstance().ConnectionString);
		}

		public abstract void Add(T entity);
		public abstract void Delete(Guid? id);
		public abstract void Update(T entity);
		public abstract DataSet Get(Guid? id);
		public abstract DataSet GetAll();
	}
}
