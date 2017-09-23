using System.Data;
using System.Data.SqlClient;

namespace Business.Database
{
	public static class Connections
	{
		public static DataTable GetDataTable(string sql, string connectionString, int commandTimeout)
		{
			var dataTable = new DataTable();
			using (var connection = new SqlConnection(connectionString))
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandTimeout = commandTimeout;
				new SqlDataAdapter(sql, connection).Fill(dataTable);
			}
			return dataTable;
		}

		public static DataSet GetDataSet(string sql, string connectionString, int commandTimeout)
		{
			var dataSet = new DataSet();
			using (var connection = new SqlConnection(connectionString))
			using (var command = new SqlCommand(sql, connection))
			{
				command.CommandTimeout = commandTimeout;
				new SqlDataAdapter(sql, connection).Fill(dataSet);
			}
			return dataSet;
		}
	}
}