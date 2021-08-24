using System.Data.SQLite;

namespace Day3.Database
{
	public class DatabaseHelper
	{
		public static string ConnectionString { get; private set; }
		private static DatabaseHelper instance;

		private DatabaseHelper()
		{
			var builder = new SQLiteConnectionStringBuilder()
			{
				["Data Source"] = "db.sqlite"
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
