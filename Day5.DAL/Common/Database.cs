using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Day5.DAL.Common
{
	public abstract class Database<T> where T : class
	{
		protected static DataSet DbCommand(string tableName, SqlCommand command)
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

		public abstract Task Add(T obj);
		public abstract Task Delete(Guid? id);
		public abstract Task Update(Guid? id, T obj);
		public abstract Task<DataSet> Get(Guid? id);
		public abstract Task<DataSet> GetAll();
	}
}
