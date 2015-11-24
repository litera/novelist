using System.Threading;
using Novelist.Web.General;

namespace Novelist.Web
{
	/// <summary>
	/// Represents a background web worker that runs off of IIS.
	/// </summary>
	public static class WebWorker
	{
		private static readonly Timer workerTimer = new Timer(Process);
		private static readonly WorkerTask worker = new WorkerTask();

		/// <summary>
		/// Starts the specified web worker by initializing periodic timer.
		/// </summary>
		public static void Start()
		{
			workerTimer.Change(60000, 60000);
		}

		/// <summary>
		/// Executes the periodic task.
		/// </summary>
		/// <param name="sender">The sender.</param>
		public static void Process(object sender)
		{
			worker.ExecuteTask();
		}
	}
}