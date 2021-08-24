using Npgsql;

namespace Day4.DAL.Common
{
	public class DatabaseHelper
	{
		public string ConnectionString { get; }
		private static DatabaseHelper instance;

		private DatabaseHelper()
		{
			var builder = new NpgsqlConnectionStringBuilder()
			{
				["Host"] = "localhost",
				["Username"] = "postgres",
				["Password"] = "postgres",
				["Database"] = "monodb"
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
