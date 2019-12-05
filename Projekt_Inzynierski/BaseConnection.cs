using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Projekt_Inzynierski
{
    public static class BaseConnection
	{
		private static SqlConnectionStringBuilder connectionString;
        private static readonly SqlConnection connection;
		static BaseConnection()
		{
            connectionString = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "Program",
                UserID = "sa",
                Password = "qwerty12345",
                IntegratedSecurity = false
            };
            connection = new SqlConnection(connectionString.ConnectionString);
		}
		public static SqlConnection GetConnection
		{
			get { return connection; }
		}
		public static SqlParameter execProcedure(string procedure)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = procedure;
			var wynik = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
			wynik.Direction = System.Data.ParameterDirection.ReturnValue;
			cmd.ExecuteNonQuery();
			return wynik;
		}
        public static SqlParameter execProcedure(string procedure, Dictionary<string, string> parameters, SqlParameter outParametr = null)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = procedure;
			foreach (var parametr in parameters)
			{
				cmd.Parameters.Add(new SqlParameter(parametr.Key, parametr.Value));
			}
			var result = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
			result.Direction = System.Data.ParameterDirection.ReturnValue;
			if (outParametr != null)
			{
				outParametr.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParametr);
			}
			cmd.ExecuteNonQuery();
			return result;
		}
		public static int execCommand(string command)
		{
			var cmd = new SqlCommand(command, connection);
			return cmd.ExecuteNonQuery();
		}
		public static object execScalar(string command)
		{
			var cmd = new SqlCommand(command, connection);
			object temp = cmd.ExecuteScalar();
			return temp;
		}

		public static SqlDataReader execReader(string command)
		{
			var cmd = new SqlCommand(command, connection);
			SqlDataReader temp = cmd.ExecuteReader();
			return temp;
		}
		public static bool openConnection()
		{
			bool ifOK = false;
			try
			{
				connection.Open();
				ifOK = true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ifOK;
		}
		public static void closeConnection()
		{
			connection.Close();
		}
	}
}