using System.ComponentModel.DataAnnotations;
using Novelist.Model;

namespace Novelist.Web.ViewModels
{
	/// <summary>
	/// Represents post creation view model entity.
	/// </summary>
	public class PostCreation: Post
	{
		/// <summary>
		/// Gets or sets the post intro.
		/// </summary>
		/// <value>
		/// Post intro.
		/// </value>
		[Required]
		public string Intro { get; set; }
	}
}