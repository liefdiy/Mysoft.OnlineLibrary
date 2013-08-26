using System.Collections.Generic;
using Mysoft.Map.Extensions.DAL;

namespace Mysoft.Platform.DataAccess
{
	public class KeptBooksRepository<T> where T : class, new()
	{
		public List<T> SelectAll(string user, int pageIndex, int pageSize, out int totalPage)
		{
			if( pageIndex <= 1 ) {
				pageIndex = 1;
			}
			int rowBegin = (pageIndex - 1) * pageSize + 1;
			int rowEnd = rowBegin + pageSize;
			totalPage = 0;

			return CPQuery.From("SELECT b.BookName, a.BorrowDate, a.ReturnDate FROM dbo.History a LEFT JOIN dbo.Books b ON a.BookId = b.BookId WHERE Reader = @reader"
				, new { reader = user }).ToList<T>();
		}
	}
}