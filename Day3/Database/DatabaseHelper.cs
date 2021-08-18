using System.Data.SqlClient;

namespace Day3.Database
{
	public class DatabaseHelper
	{
		public static string ConnectionString { get; private set; }
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
