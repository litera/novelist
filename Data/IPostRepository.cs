using System.Collections.Generic;
using Novelist.Model;

namespace Novelist.Data
{
	/// <summary>
	/// Defines post respository functionality to implmenting classes.
	/// </summary>
	public interface IPostRepository
	{
		/// <summary>
		/// Creates a new <see cref="Novelist.Model.Post"/> object instance using specified data.
		/// </summary>
		/// <param name="title">Post title.</param>
		/// <param name="details">Post details.</param>
		/// <param name="authorId">Post author identifier.</param>
		/// <returns>Returns the newly created <see cref="Novelist.Model.Post"/> object instance.</returns>
		Post Create(string title, string details, int authorId);

		/// <summary>
		/// Gets the specified <see cref="NovelistModel.Post"/> object instance specified identifier.
		/// </summary>
		/// <param name="id">Post identifier.</param>
		/// <returns>Returns a <see cref="Novelist.Model.Post"/> object instance.</returns>
		Post Get(int id);

		/// <summary>
		/// Gets all posts from data store.
		/// </summary>
		/// <returns>Returns an <see cref="IEnumerable{T}"/> of <see cref="Novelist.Model.Post"/> objects.</returns>
		IEnumerable<Post> GetAll();
	}
}
