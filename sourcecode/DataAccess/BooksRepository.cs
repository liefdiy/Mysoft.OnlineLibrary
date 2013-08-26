using System.Collections.Generic;
using Mysoft.Map.Extensions.DAL;

namespace Mysoft.Platform.DataAccess
{
	public class BooksRepository<T> where T : class, new()
	{
		/// <summary>
		/// 查询全部图书
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPage"></param>
		/// <returns></returns>
		public List<T> SelectAll(int pageIndex, int pageSize, out int totalPage)
		{
			if( pageIndex <= 1 ) {
				pageIndex = 1;
			}
			int rowBegin = (pageIndex - 1) * pageSize + 1;
			int rowEnd = rowBegin + pageSize;
			totalPage = 0;

			return CPQuery.From("SELECT MAX(t.r) Row, MAX(t.BookId) BookId, MAX(t.CategoryId) CategoryId, " +
			                    "MAX(CategoryName) CategoryName, MAX(t.BookSerialNo) BookSerialNo, MAX(t.BookName) BookName, " +
			                    "MAX(t.Author) Author, MAX(t.TranslationAuthor) TranslationAuthor, MAX(t.ISBN) ISBN, " +
			                    "MAX(t.Press) Press, MAX(t.PublishDate) PublishDate, MAX(t.Price) Price, MAX(t.Grade) Grade, " +
			                    "MAX(t.PicUrl) PicUrl, MAX(t.Brief) Brief, MAX(t.Status) Status, MAX(t.CreateDate) CreateDate, " +
			                    "SUM(CASE [Status] WHEN 1 THEN 1 ELSE 0 END) [In] FROM    " +
			                    "( SELECT ROW_NUMBER() OVER ( ORDER BY BookId ) AS r , * FROM Books ) t " +
			                    "LEFT JOIN dbo.Category c ON t.CategoryId = c.CategoryId  WHERE r >= @rowBegin AND r <= @rowEnd GROUP BY t.BookName "
			                    , new {rowBegin = rowBegin, rowEnd = rowEnd}).ToList<T>();
		}

		/// <summary>
		/// 根据类别查找图书
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPage"></param>
		/// <returns></returns>
		public List<T> GetBooksByCategoryId(int categoryId, int pageIndex, int pageSize, out int totalPage)
		{
			if( pageIndex <= 1 ) {
				pageIndex = 1;
			}
			int rowBegin = (pageIndex - 1) * pageSize + 1;
			int rowEnd = rowBegin + pageSize;
			totalPage = 0;

			return CPQuery.From("SELECT MAX(t.r) Row, MAX(t.BookId) BookId, MAX(t.CategoryId) CategoryId, " +
						  "MAX(CategoryName) CategoryName, MAX(t.BookSerialNo) BookSerialNo, " +
						  "MAX(t.BookName) BookName, MAX(t.Author) Author, MAX(t.TranslationAuthor) TranslationAuthor, " +
						  "MAX(t.ISBN) ISBN, MAX(t.Press) Press, MAX(t.PublishDate) PublishDate, MAX(t.Price) Price, " +
						  "MAX(t.Grade) Grade, MAX(t.PicUrl) PicUrl, MAX(t.Brief) Brief, MAX(t.Status) Status, " +
						  "MAX(t.CreateDate) CreateDate, SUM(CASE [Status] WHEN 1 THEN 1 ELSE 0 END) [In] FROM    " +
						  "(SELECT ROW_NUMBER() OVER ( ORDER BY BookId ) AS r , * FROM Books ) t " +
						  "LEFT JOIN dbo.Category c ON t.CategoryId = c.CategoryId  WHERE r >= @rowBegin AND r <= @rowEnd AND t.CategoryId = @categoryId GROUP BY t.BookName "
						  , new { rowBegin = rowBegin, rowEnd = rowEnd, categoryId = categoryId }).ToList<T>();
		}

		/// <summary>
		/// 根据书名模糊查找图书
		/// </summary>
		/// <param name="bookName"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPage"></param>
		/// <returns></returns>
		public List<T> GetBooksByBookName(string bookName, int pageIndex, int pageSize, out int totalPage)
		{
			if( pageIndex <= 1 ) {
				pageIndex = 1;
			}
			int rowBegin = (pageIndex - 1) * pageSize + 1;
			int rowEnd = rowBegin + pageSize;
			totalPage = 0;

			return CPQuery.From("SELECT MAX(t.r) Row, MAX(t.BookId) BookId, MAX(t.CategoryId) CategoryId, " +
						 "MAX(CategoryName) CategoryName, MAX(t.BookSerialNo) BookSerialNo, " +
						 "MAX(t.BookName) BookName, MAX(t.Author) Author, MAX(t.TranslationAuthor) TranslationAuthor, " +
						 "MAX(t.ISBN) ISBN, MAX(t.Press) Press, MAX(t.PublishDate) PublishDate, MAX(t.Price) Price, " +
						 "MAX(t.Grade) Grade, MAX(t.PicUrl) PicUrl, MAX(t.Brief) Brief, MAX(t.Status) Status, " +
						 "MAX(t.CreateDate) CreateDate, SUM(CASE [Status] WHEN 1 THEN 1 ELSE 0 END) [In] FROM    " +
						 "(SELECT ROW_NUMBER() OVER ( ORDER BY BookId ) AS r , * FROM Books ) t " +
						 "LEFT JOIN dbo.Category c ON t.CategoryId = c.CategoryId  WHERE r >= @rowBegin AND r <= @rowEnd AND t.BookName LIKE @bookName GROUP BY t.BookName ORDER BY t.BookName"
						 , new { rowBegin = rowBegin, rowEnd = rowEnd, bookName = bookName }).ToList<T>();
		}
	}
}