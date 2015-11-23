using System.ComponentModel.DataAnnotations;
using Novelist.Model;

namespace Novelist.Web.ViewModels
{
	/// <summary>
	/// Represents user creation view model entity.
	/// </summary>
	public class UserCreation: User
	{
		/// <summary>
		/// Gets or sets user password.
		/// </summary>
		/// <value>
		/// User password.
		/// </value>
		[Required]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets user repeated password.
		/// </summary>
		/// <value>
		/// User repeated repeat.
		/// </value>
		[Required]
		[Compare("Password")]
		public string Repeat { get; set; }
	}
}