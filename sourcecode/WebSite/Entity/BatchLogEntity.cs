#region

using System;

#endregion

namespace Mysoft.Platform.OnlineLibrary.Entity
{
	/// <summary>
	/// BatchLogEntity
	/// </summary>
	[Serializable]
	public class BatchLogEntity
	{
		/// <summary>
		///     BatchId
		/// </summary>
		public decimal BatchId { get; set; }

		/// <summary>
		///     Reader
		/// </summary>
		public string Reader { get; set; }

		/// <summary>
		///     已藏书最多10W本计算，限定每人最多借阅10本书，BookList长度等于5*10+1*10,5是BookId最大长度，1是分隔符
		/// </summary>
		public int? BooksCount { get; set; }

		/// <summary>
		///     本次借阅图书id列表，用逗号分隔
		/// </summary>
		public string BookList { get; set; }

		/// <summary>
		///     CreateDate
		/// </summary>
		public DateTime? CreateDate { get; set; }
	}
}