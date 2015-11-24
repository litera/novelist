using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Novelist.Data;
using Novelist.Model;
using Novelist.Web.ViewModels;

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
		public Post Post(PostCreation post)
		{
			if (post == null || !this.ModelState.IsValid)
			{
				throw new HttpResponseException(
					this.Request.CreateErrorResponse(
						HttpStatusCode.BadRequest, this.ModelState));
			}

			return this.postRepository.Create(
				HttpUtility.HtmlEncode(post.Title.Trim()),
				this.ConvertToSimpleHtml(
					HttpUtility.HtmlEncode(post.Intro.Trim()),
					HttpUtility.HtmlEncode(post.Content.Trim())
				),
				post.Author.Id);
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

		private string ConvertToSimpleHtml(string intro, string content)
		{
			// generate headings
			content = Regex.Replace(
				content,
				@"^[ ]*(?<heading>#+)(?<text>\s+\w+.*)$",
				new MatchEvaluator(match => {
					int count = Math.Min(6, Math.Max(2, match.Groups["heading"].Value.Length));
					return string.Format("<h{0}>{1}</h{0}>", count, match.Groups["text"].Value);
				}),
				RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

			// generate paragraphs
			content = Regex.Replace(
				content,
				@"\s*\n{2,}\s*",
				"</p><p>",
				RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

			// remove single linefeeds
			content = Regex.Replace(content, @"[\s\n]{2,}", " ", RegexOptions.Compiled | RegexOptions.Singleline);

			// unwrap headings in paragraphs
			content = Regex.Replace(content, @"(?:<p>)?\s*<(/?)h(\d)>\s*(?:</p>)?", "<${1}h${2}>");

			return string.Format(
				"<p class=\"intro\">{0}</p><p>{1}</p>",
				intro,
				content);
		}
	}
}
