using System.Web.Http;
using Novelist.Data;
using Novelist.Model;
using Novelist.Web.ViewModels;

namespace Novelist.Web.Api
{
	[RoutePrefix("api")]
	public class SecurityController : ApiController
	{
		private IUserRepository userRepository = null;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SecurityController"/> class.
		/// </summary>
		/// <param name="userRepository"><see cref="IUserRepository"/> object instance to inject.</param>
		public SecurityController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SecurityController"/> class.
		/// </summary>
		public SecurityController()
			: this(new UserRepository())
		{
			// does nothing
		}

		#endregion

		[Route("login")]
		[HttpPost]
		public User Login(UserCredentials credentials)
		{
			if (!this.ModelState.IsValid)
			{
				return null;
			}

			return this.userRepository.Authenticate(credentials.Username, credentials.Password.GetHashCode());
		}

		[Route("register")]
		[HttpPost]
		public User Register(UserCreation user)
		{
			if (!this.ModelState.IsValid)
			{
				return null;
			}

			return this.userRepository.Create(user.Name, user.Email, user.Password.GetHashCode());
		}
	}
}
