#region

using System;

#endregion

namespace Mysoft.Platform.OnlineLibrary.Entity
{
	/// <summary>
	/// BooksEntity
	/// </summary>
	[Serializable]
	public class BooksEntity
	{
		/// <summary>
		///     BookId
		/// </summary>
		public int BookId { get; set; }

		/// <summary>
		///     类别id
		/// </summary>
		public int? CategoryId { get; set; }

		/// <summary>
		///     图书编号，用于图书管理，需要唯一性约束
		/// </summary>
		public string BookSerialNo { get; set; }

		/// <summary>
		///     图书名称
		/// </summary>
		public string BookName { get; set; }

		/// <summary>
		///     作者，多个用逗号分隔
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		///     译著者
		/// </summary>
		public string TranslationAuthor { get; set; }

		/// <summary>
		///     ISBN
		/// </summary>
		public string ISBN { get; set; }

		/// <summary>
		///     出版社
		/// </summary>
		public string Press { get; set; }

		/// <summary>
		///     出版日期
		/// </summary>
		public DateTime? PublishDate { get; set; }

		/// <summary>
		///     单价
		/// </summary>
		public decimal? Price { get; set; }

		/// <summary>
		///     评分，0~5分
		/// </summary>
		public int? Grade { get; set; }

		/// <summary>
		///     图书封面url
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
		///     简介
		/// </summary>
		public string Brief { get; set; }

		/// <summary>
		///     图书状态，1在馆，2外借，3遗失，4计划购买
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		///     创建时间
		/// </summary>
		public DateTime? CreateDate { get; set; }
	}
}