using System.Collections.Generic;
using System.Data;
using Mysoft.Platform.Component.Extensions;
using Mysoft.Platform.OnlineLibrary.Entity;

namespace Mysoft.Platform.OnlineLibrary.Models
{
	public class BookModel : BooksEntity
	{
		public string CategoryName { get; set; }

		public int In { get; set; }

		public static List<BookModel> ToList(DataTable table)
		{
			List<BookModel> list = new List<BookModel>();

			if( table != null && table.Rows.Count > 0 ) {
				for( int i = 0; i < table.Rows.Count; i++ ) {
					BookModel book = new BookModel();
					book.BookId = table.Rows[i]["BookId"].GetInt();
					book.CategoryId = table.Rows[i]["CategoryId"].GetInt();
					book.BookSerialNo = table.Rows[i]["BookSerialNo"].GetString();
					book.BookName = table.Rows[i]["BookName"].GetString();
					book.Author = table.Rows[i]["Author"].GetString();
					book.TranslationAuthor = table.Rows[i]["TranslationAuthor"].GetString();
					book.ISBN = table.Rows[i]["ISBN"].GetString();
					book.Press = table.Rows[i]["Press"].GetString();
					book.PublishDate = table.Rows[i]["PublishDate"].GetDateTime();
					book.Price = table.Rows[i]["Price"].GetDecimal();
					book.Grade = table.Rows[i]["Grade"].GetInt();
					book.PicUrl = table.Rows[i]["PicUrl"].GetString("#");
					book.Brief = table.Rows[i]["Brief"].GetString();
					book.Status = table.Rows[i]["Status"].GetInt();
					book.CreateDate = table.Rows[i]["CreateDate"].GetDateTime();
					book.CategoryName = table.Rows[i]["CategoryName"].GetString();
					book.In = table.Rows[i]["In"].GetInt();

					list.Add(book);
				}
			}

			return list;
		}
	}
}