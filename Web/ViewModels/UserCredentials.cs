
using System.ComponentModel.DataAnnotations;
namespace Novelist.Web.ViewModels
{
	/// <summary>
	/// Represents user authentication credentials view model entity.
	/// </summary>
	public class UserCredentials
	{
		/// <summary>
		/// Gets or sets the authentication credentials username.
		/// </summary>
		/// <value>
		/// Authentication credentials username.
		/// </value>
		[Required]
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the authentication credentials password.
		/// </summary>
		/// <value>
		/// Authentication credentials password.
		/// </value>
		[Required]
		public string Password { get; set; }
	}
}