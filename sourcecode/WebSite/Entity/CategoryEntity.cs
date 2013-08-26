#region

using System;

#endregion

namespace Mysoft.Platform.OnlineLibrary.Entity
{
	/// <summary>
	/// 所有类别
	/// </summary>
	[Serializable]
	public class CategoryEntity
	{
		/// <summary>
		/// CategoryId
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// 父级类别
		/// </summary>
		public int? FatherId { get; set; }

		/// <summary>
		/// 类别名称
		/// </summary>
		public string CategoryName { get; set; }
	}
}