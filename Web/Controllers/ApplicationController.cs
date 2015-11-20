using System.Web.Mvc;

namespace Novelist.Web.Controllers
{
	[RoutePrefix("")]
	[Route("{action=start}")]
	public class ApplicationController : Controller
	{
		// GET: Application
		[Route]
		public ActionResult Start()
		{
			return View();
		}
	}
}