#region

using System;

#endregion

namespace Mysoft.Platform.OnlineLibrary.Entity
{
	/// <summary>
	/// ReadersEntity
	/// </summary>
	[Serializable]
	public class ReadersEntity
	{
		/// <summary>
		///     ReaderId
		/// </summary>
		public int ReaderId { get; set; }

		/// <summary>
		///     UserId
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		///     DeptId
		/// </summary>
		public int? DeptId { get; set; }

		/// <summary>
		///     最多可借阅数量
		/// </summary>
		public int? MaxNum { get; set; }

		/// <summary>
		///     最多可借阅天数
		/// </summary>
		public int? MaxDays { get; set; }

		/// <summary>
		///     1可用，2警告，3冻结，4拉黑
		/// </summary>
		public int? Status { get; set; }
	}
}