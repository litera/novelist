using System;
using System.Collections.Generic;
using System.Linq;
using Novelist.Model;

namespace Novelist.Data
{
	/// <summary>
	/// Provides post related repository functionality.
	/// </summary>
	public class PostRepository: RepositoryBase, IPostRepository
	{
		#region IContentRepository Members

		/// <summary>
		/// Creates a new <see cref="Novelist.Model.Post" /> object instance using specified data.
		/// </summary>
		/// <param name="title">Post title.</param>
		/// <param name="details">Post details.</param>
		/// <param name="authorId">Post author identifier.</param>
		/// <returns>
		/// Returns the newly created <see cref="Novelist.Model.Post" /> object instance.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// String 'title' should not be null, empty or whitespace only.;title
		/// or
		/// String 'details' should not be null, empty or whitespace only.;details
		/// </exception>
		public Post Create(string title, string details, int authorId)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new ArgumentException("String 'title' should not be null, empty or whitespace only.", "title");
			}
			if (string.IsNullOrWhiteSpace(details))
			{
				throw new ArgumentException("String 'details' should not be null, empty or whitespace only.", "details");
			}

			using (var db = this.GetDatabase())
			{
				return db
					.Fetch<Post, User>(StoredProcedures.Post.Create(authorId,title,details))
					.Single();
			}
		}

		/// <summary>
		/// Gets the specified <see cref="NovelistModel.Post" /> object instance specified identifier.
		/// </summary>
		/// <param name="id">Post identifier.</param>
		/// <returns>
		/// Returns a <see cref="Novelist.Model.Post" /> object instance.
		/// </returns>
		public Post Get(int id)
		{
			using (var db = this.GetDatabase())
			{
				return db
					.Fetch<Post, User>(StoredProcedures.Post.Get(id))
					.Single();
			}
		}

		/// <summary>
		/// Gets all posts from data store.
		/// </summary>
		/// <returns>
		/// Returns an <see cref="IEnumerable{T}" /> of <see cref="Novelist.Model.Post" /> objects.
		/// </returns>
		public IEnumerable<Post> GetAll()
		{
			using (var db = this.GetDatabase())
			{
				return db
					.Fetch<Post, User>(StoredProcedures.Post.GetAll());
			}
		}

		#endregion
	}
}
