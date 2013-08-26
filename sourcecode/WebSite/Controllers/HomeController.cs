using System.Web.Mvc;
using Mysoft.Platform.Component.Attributes;
using Mysoft.Platform.DataAccess;
using Mysoft.Platform.OnlineLibrary.Models;

namespace Mysoft.Platform.OnlineLibrary.Controllers
{
	[Auth]
	public class HomeController : Controller
	{
		private readonly BooksRepository<BookModel> _repository = new BooksRepository<BookModel>();
		private const int PageSize = 3;

		/// <summary>
		/// 图书馆
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(int pageIndex = 1)
		{
			int totalPage = 0;
			return View(_repository.SelectAll(pageIndex, PageSize, out totalPage));
		}

		/// <summary>
		/// 图书详情
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns></returns>
		public ActionResult Detail(int? bookId)
		{
			ViewBag.BookId = bookId != null ? bookId.Value : 0;
			return View();
		}

		/// <summary>
		/// 分类图书
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		public ActionResult Category(int? categoryId)
		{
			int totalPage = 0;
			var dt = _repository.GetBooksByCategoryId(categoryId == null ? 0 : categoryId.Value, 1, PageSize, out totalPage);
			return View("Index", dt);
		}

		/// <summary>
		/// 图书搜索
		/// </summary>
		/// <param name="bookName"></param>
		/// <returns></returns>
		public ActionResult Search(string bookName)
		{
			if( string.IsNullOrEmpty(bookName) ) {
				return View("Index");
			}

			int totalPage = 0;
			var dt = _repository.GetBooksByBookName(bookName, 1, PageSize, out totalPage);
			return View("Index", dt);
		}
	}
}