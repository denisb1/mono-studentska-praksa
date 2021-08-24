using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace Day5.DAL.Common
{
	public abstract class Database<T> where T : class
	{
		protected static async Task<DataSet> DbCommon(string tableName, NpgsqlCommand command)
		{
			var adapter = new NpgsqlDataAdapter();
			var dataSet = new DataSet(tableName);

			adapter.TableMappings.Add("Table", tableName);
			command.CommandType = CommandType.Text;
			adapter.SelectCommand = command;
			await Task.Run(() => adapter.Fill(dataSet));

			return dataSet;
		}

		protected static NpgsqlConnection DbConnection()
		{
			return new NpgsqlConnection(DatabaseHelper.GetInstance().ConnectionString);
		}

		public abstract Task Add(T entity);
		public abstract Task Delete(Guid? id);
		public abstract Task Update(T entity);
		public abstract Task<DataSet> Get(Guid? id);
		public abstract Task<DataSet> GetAll();
	}
}
