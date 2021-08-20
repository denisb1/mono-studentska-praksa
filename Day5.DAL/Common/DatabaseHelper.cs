using System.Data.SqlClient;

namespace Day5.DAL.Common
{
	public class DatabaseHelper
	{
		public string ConnectionString { get; }
		private static DatabaseHelper instance;

		private DatabaseHelper()
		{
			var builder = new SqlConnectionStringBuilder
			{
				["Data Source"] = "DESKTOP-KSF4HEK",
				["User id"] = "sa",
				["Password"] = "MSSQLSERVER",
				["Initial Catalog"] = "mono"
			};
			ConnectionString = builder.ConnectionString;
		}

		public static DatabaseHelper GetInstance()
		{
			instance ??= new DatabaseHelper();
			return instance;
		}
	}
}
