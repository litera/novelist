using System.ComponentModel.DataAnnotations;

namespace Novelist.Model
{
	/// <summary>
	/// Represents user application model entity.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Gets or sets user identifier.
		/// </summary>
		/// <value>
		/// User identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets user display name.
		/// </summary>
		/// <value>
		/// User display name.
		/// </value>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets user email (also serves as login username).
		/// </summary>
		/// <value>
		/// User email.
		/// </value>
		[Required]
		public string Email { get; set; }
	}
}
