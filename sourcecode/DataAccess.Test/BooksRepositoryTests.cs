using System.Collections.Generic;
using System.Data;
using Mysoft.Platform.OnlineLibrary.Models;
using NUnit.Framework;

namespace Mysoft.Platform.DataAccess.Test
{
	[TestFixture]
	public class BooksRepositoryTests : DataAccessBaseTests
	{
		[Test]
		public void TestSelectAll()
		{
			BooksRepository<BookModel> repository = new BooksRepository<BookModel>();
			int totalPage = 1;
			List<BookModel> books = repository.SelectAll(1, 20, out totalPage);
			Assert.IsTrue(books.Count > 0);
		}
	}
}