#region

using System;

#endregion

namespace Mysoft.Platform.OnlineLibrary.Entity
{
	/// <summary>
	/// HistoryEntity
	/// </summary>
	[Serializable]
	public class HistoryEntity
	{
		/// <summary>
		///     HistoryId
		/// </summary>
		public int HistoryId { get; set; }

		/// <summary>
		///     借阅批次号
		/// </summary>
		public string BatchId { get; set; }

		/// <summary>
		///     读者名称
		/// </summary>
		public string Reader { get; set; }

		/// <summary>
		///     BookId
		/// </summary>
		public int? BookId { get; set; }

		/// <summary>
		///     借阅日期
		/// </summary>
		public DateTime? BorrowDate { get; set; }

		/// <summary>
		///     预计归还日期
		/// </summary>
		public DateTime? ReturnDate { get; set; }

		/// <summary>
		///     实际归还日期
		/// </summary>
		public DateTime? ActualReturnDate { get; set; }

		/// <summary>
		///     1借阅，2归还，3续借，4挂失
		/// </summary>
		public int? OperationType { get; set; }

		/// <summary>
		///     创建日期
		/// </summary>
		public DateTime? CreateDate { get; set; }
	}
}