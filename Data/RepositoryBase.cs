using System.Diagnostics.CodeAnalysis;
using NPoco;

namespace Novelist.Data
{
	/// <summary>
	/// Defines functionality for all inheriting repository classes.
	/// </summary>
	public abstract class RepositoryBase
	{
		private const string connectionName = "NovelistSQLConnection";

		/// <summary>
		/// Gets a disposable database instance.
		/// </summary>
		/// <returns>Returns a <see cref="NPoco.IDatabase"/> object instance.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This needs to stay as a method because it always creates a new instance.")]
		protected IDatabase GetDatabase()
		{
			return new Database(connectionName);
		}
	}
}
