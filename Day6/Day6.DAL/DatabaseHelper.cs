using Npgsql;

namespace Day6.DAL
{
	public class DatabaseHelper
	{
		public string ConnectionString { get; }
		private static DatabaseHelper _instance;

		private DatabaseHelper()
		{
			var builder = new NpgsqlConnectionStringBuilder()
			{
				["Host"] = "localhost",
				["Username"] = "postgres",
				["Password"] = "postgres",
				["Database"] = "monodbef"
			};
			ConnectionString = builder.ConnectionString;
		}

		public static DatabaseHelper GetInstance()
		{
			_instance ??= new DatabaseHelper();
			return _instance;
		}
	}
}
