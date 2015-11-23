using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Novelist.Data;
using Novelist.Model;

namespace Novelist.Web.Api
{
	[RoutePrefix("api/posts")]
	public class PostController : ApiController
	{
		private IPostRepository postRepository = null;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PostController"/> class.
		/// </summary>
		/// <param name="postRepository"><see cref="IPostRepository"/> object instance to inject.</param>
		public PostController(IPostRepository postRepository)
		{
			this.postRepository = postRepository;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PostController"/> class.
		/// </summary>
		public PostController() :
			this(new PostRepository())
		{
			// does nothing
		}

		#endregion

		[Route]
		public Post Post(Post post)
		{
			if (post == null || !this.ModelState.IsValid)
			{
				throw new HttpResponseException(
					this.Request.CreateErrorResponse(
						HttpStatusCode.BadRequest, this.ModelState));
			}

			throw new NotImplementedException();
			//return this.postRepository.Create(post.Title, post.Details, post.Author.Id);
		}

		[Route]
		public IEnumerable<Post> Get()
		{
			return this.postRepository.GetAll();
		}

		[Route("{id:int}")]
		public Post Get(int id)
		{
			return this.postRepository.Get(id);
		}
	}
}
