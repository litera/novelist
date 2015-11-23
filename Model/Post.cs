using System;
using System.ComponentModel.DataAnnotations;

namespace Novelist.Model
{
	/// <summary>
	/// Represents post application model entity.
	/// </summary>
	public class Post
	{
		/// <summary>
		/// Gets or sets post identifier.
		/// </summary>
		/// <value>
		/// Post identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets post title.
		/// </summary>
		/// <value>
		/// Post title.
		/// </value>
		[Required]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets post details.
		/// </summary>
		/// <value>
		/// Post details.
		/// </value>
		[Required]
		public string Details { get; set; }

		/// <summary>
		/// Gets or sets post author.
		/// </summary>
		/// <value>
		/// Post author.
		/// </value>
		public User Author { get; set; }

		/// <summary>
		/// Gets or sets the post creation date.
		/// </summary>
		/// <value>
		/// Post creation date.
		/// </value>
		public DateTime CreatedOn { get; set; }
	}
}
