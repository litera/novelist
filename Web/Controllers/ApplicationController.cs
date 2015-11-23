using System.Web.Mvc;

namespace Novelist.Web.Controllers
{
	[RoutePrefix("")]
	[Route("{action=start}")]
	public class ApplicationController : Controller
	{
		// GET: Application
		[Route]
		[Route("{*catchall}")] // we need this to make it possible for Angular html5Mode to work as expected
		public ActionResult Start()
		{
			return View();
		}
	}
}