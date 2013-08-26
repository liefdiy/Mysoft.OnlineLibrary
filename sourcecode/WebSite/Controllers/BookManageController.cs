using System.Web.Mvc;
using Mysoft.Platform.Component.Attributes;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
	[Auth(UserGroup.Admin)]
	public class BookManageController : Controller
	{
		//
		// GET: /BookDetail/

		public ActionResult Index()
		{
			return View();
		}
	}
}