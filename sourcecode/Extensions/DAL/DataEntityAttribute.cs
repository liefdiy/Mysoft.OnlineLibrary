using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// 表示数据实体属性
	/// </summary>
	/// <example>
	///		<para>下面的代码演示了数据实体属性的用法</para>
	///		<code>
	///		//Alias属性表示实体类别名,当数据库中的表明与实体类名不同时.需要使用Alias别名标记
	///		[DataEntity(Alias="cb_Contract")]
	///		public class cbContract{
	///			public string ContractName { get; set; }
	///		}
	///		</code>
	/// </example>
	[AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class DataEntityAttribute : Attribute
	{
		/// <summary>
		/// 别名
		/// </summary>
		public string Alias { get; set; }
	}
}
