using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace Worker
{
	public class Program
	{
		private static readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NovelistDBConnection"].ConnectionString);
		private static readonly SqlCommand comm = new SqlCommand(";exec dbo.Post_Featurize", conn);

		public static void Main(string[] args)
		{
			// start timer thread
			Timer timer = new Timer(Featurize);
			timer.Change(60000, 60000);
			Thread.Sleep(Timeout.Infinite);
		}

		private static void Featurize(object obj)
		{
			Console.WriteLine("Featurize started on {0}", DateTime.Now.ToLocalTime());
			try
			{
				conn.Open();
				comm.ExecuteNonQuery();
			}
			finally
			{
				// make sure we close connection
				if (conn.State != System.Data.ConnectionState.Closed)
				{
					conn.Close();
				}
			}
			Console.WriteLine("Featurize completed on {0}", DateTime.Now.ToLocalTime());
		}
	}
}
