using Novelist.Model;

namespace Novelist.Data
{
	/// <summary>
	/// Provides user related repository functionality.
	/// </summary>
	public class UserRepository : RepositoryBase, IUserRepository
	{
		#region IUserRepository Members

		/// <summary>
		/// Creates a new user using specified data.
		/// </summary>
		/// <param name="displayName">Display name.</param>
		/// <param name="email">Email address.</param>
		/// <param name="passwordHash">Password hash.</param>
		/// <returns>
		/// Returns the newly created <see cref="Novelist.Model.User" /> object instance.
		/// </returns>
		public User Create(string displayName, string email, int passwordHash)
		{
			using(var db = this.GetDatabase())
			{
				return db.SingleOrDefault<User>(
					StoredProcedures
						.User
						.Create(displayName, email, passwordHash));
			}
		}

		/// <summary>
		/// Gets user instance using specified authentication details.
		/// </summary>
		/// <param name="email">User email.</param>
		/// <param name="passwordHash">User password hash.</param>
		/// <returns>
		/// If a matching record is found in the data store a <see cref="Novelist.Model.User" /> object instance is returned; otherwise <c>null</c>.
		/// </returns>
		public User Authenticate(string email, int passwordHash)
		{
			using (var db = this.GetDatabase())
			{
				return db
					.SingleOrDefault<User>(StoredProcedures
						.User
						.Authenticate(email, passwordHash));
			}
		}

		#endregion
	}
}
