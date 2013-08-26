using System.Web.Mvc;
using System.Web.Routing;

namespace Mysoft.Platform.OnlineLibrary
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode,
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//Route bookDetail = new Route("Home/Detail/{bookId}", new MvcRouteHandler());
			//routes.Add("BookDetail", bookDetail);

			routes.MapRoute(
				"BookDetail", // Route name
				"Home/Detail/{bookId}", // URL with parameters
				new { controller = "Home", action = "Detail", bookId = UrlParameter.Optional }, // Parameter defaults
				new { bookId = @"\d*" }
			);

			routes.MapRoute(
				"Category", // Route name
				"Home/Category/{categoryId}", // URL with parameters
				new { controller = "Home", action = "Category", categoryId = UrlParameter.Optional }, // Parameter defaults
				new { categoryId = @"\d*" }
			);

			routes.MapRoute(
				"Search", // Route name
				"Home/Search/{bookName}", // URL with parameters
				new { controller = "Home", action = "Search", bookName = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{pageIndex}", // URL with parameters
				new { controller = "Home", action = "Index", pageIndex = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			Bootstrapper.Start();
		}
	}
}