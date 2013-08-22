using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mysoft.Platform.Component.Attributes;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
	[Auth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
