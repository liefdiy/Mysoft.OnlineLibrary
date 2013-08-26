using System.Web.Mvc;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
	public class ErrorController : Controller
	{
		//
		// GET: /Error/

		public ActionResult Index(string returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}
	}
}