using System.Configuration;
using System.Data.SqlClient;
using System.Web.Hosting;

namespace Novelist.Web.General
{
	/// <summary>
	/// Represents the actual worker task.
	/// </summary>
	public class WorkerTask : IRegisteredObject
	{
		private static readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NovelistDBConnection"].ConnectionString);
		private static readonly SqlCommand comm = new SqlCommand(";exec dbo.Post_Featurize", conn);

		private static readonly object threadLock = new object();
		private bool shuttingDown = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkerTask"/> class.
		/// </summary>
		public WorkerTask()
		{
			HostingEnvironment.RegisterObject(this);
		}

		/// <summary>
		/// Executes the task in thread save manner.
		/// </summary>
		public void ExecuteTask()
		{
			lock (threadLock)
			{
				if (this.shuttingDown)
				{
					return;
				}

				// perform task
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
			}
		}

		#region IRegisteredObject Members

		/// <summary>
		/// Requests a registered object to unregister.
		/// </summary>
		/// <param name="immediate">true to indicate the registered object should unregister from the hosting environment before returning; otherwise, false.</param>
		public void Stop(bool immediate)
		{
			lock (threadLock)
			{
				shuttingDown = true;
			}

			HostingEnvironment.UnregisterObject(this);
		}

		#endregion
	}
}