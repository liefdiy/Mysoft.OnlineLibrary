using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// 表示数据列属性
	/// </summary>
	/// <example>
	///		<para>下面的代码演示了数据列属性的用法</para>
	///		<code>
	///		public class cbContract{
	///			//DataColumn属性用来标记字段名的数据库特性
	///			//Alias表示别名,当出现数据字段名于代码中属性名不一致场景时,需要使用Alias标记
	///			//PrimaryKey表示该属性对应的数据库字段是否为主键
	///			//TimeStamp表示该属性对应的数据库字段是否为时间戳数据类型
	///			//Identity表示该属性对应的数据库字段是否为自增长列
	///			//SeqGuid表示该属性对应的数据库字段默认值是否为NewSequentialId()数据库函数,如果是则表示该列是有序GUID
	///			[DataColumn(Alias="Contract_Name", PrimaryKey=true, TimeStamp=true, Identity=true, SeqGuid=true)]
	///			public string ContractName { get; set; }
	///		}
	///		</code>
	/// </example>
	[AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class DataColumnAttribute : Attribute
	{
		/// <summary>
		/// 别名
		/// </summary>
		public string Alias { get; set; }
		/// <summary>
		/// 是否主键
		/// </summary>
		public bool PrimaryKey { get; set; }
		/// <summary>
		/// 是否为时间戳类
		/// </summary>
		public bool TimeStamp { get; set; }
		/// <summary>
		/// 是否为标识列
		/// </summary>
		public bool Identity { get; set; }
		/// <summary>
		/// 是否为有序GUID列
		/// </summary>
		public bool SeqGuid { get; set; }

		/// <summary>
		/// 是否允许为空
		/// </summary>
		public bool IsNullable { get; set; }

		/// <summary>
		/// 字段的默认值表达式
		/// </summary>
		public string DefaultValue { get; set; }
	}
}
