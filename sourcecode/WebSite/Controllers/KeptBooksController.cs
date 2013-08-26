using System.Web.Mvc;
using Mysoft.Platform.DataAccess;
using Mysoft.Platform.OnlineLibrary.Models;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
	public class KeptBooksController : Controller
	{
		private readonly KeptBooksRepository<HistoryModel> _repository = new KeptBooksRepository<HistoryModel>();
		private const int PageSize = 10;

		/// <summary>
		/// 已借图书/续借
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(int pageIndex = 1)
		{
			int totalPage = 0;
			return View(_repository.SelectAll(User.Identity.Name, pageIndex, PageSize, out totalPage));
		}
	}
}