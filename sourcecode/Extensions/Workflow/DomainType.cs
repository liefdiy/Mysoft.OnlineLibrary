using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.Workflow
{
	/// <summary>
	/// 表示业务域Domain的type属性值,与数据库的类型对应
	/// </summary>
	public enum DomainType
	{
		/// <summary>
		/// 表示数据库中DateTime类型
		/// </summary>
		DateTime,
		/// <summary>
		/// 表示数据库中Decimal类型
		/// </summary>
		Decimal,
		/// <summary>
		/// 表示数据库中Numeric类型
		/// </summary>
		Numeric,
		/// <summary>
		/// 表示数据库中Money类型
		/// </summary>
		Money,
		/// <summary>
		/// 表示数据库中Real类型
		/// </summary>
		Real,
		/// <summary>
		/// 表示数据库中Float类型
		/// </summary>
		Float,
		/// <summary>
		/// 表示数据库中Int类型
		/// </summary>
		Int,
		/// <summary>
		/// 表示数据库中SmallInt类型
		/// </summary>
		SmallInt,
		/// <summary>
		/// 表示数据库中BigInt类型
		/// </summary>
		BigInt
	}
}
