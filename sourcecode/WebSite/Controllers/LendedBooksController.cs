using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
    public class LendedBooksController : Controller
    {
        //
        // GET: /LendedBooks/

        public ActionResult Index()
        {
            return View();
        }

    }
}
