using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Mysoft.Map.Extensions.Workflow
{
	/// <summary>
	/// 表示为BusinessType类型绑定数据的数据源
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>绑定时,需要指定数据源的不同行为时,请使用本类</description></item>
	/// <item><description>本类源自参照ERP258代码时,代码中有循环域哈希值自动生成,列名映射,标识列等用法</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <para>下面的代码演示了BindOption类的用法</para>
	/// <code>
	/// <![CDATA[
	/// //从文件加载BusinessType类实例
	/// 
	///	BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
	///	
	/// //查询SQL
	///	string sql = @"SELECT
	///		'A'  AS [A],
	///		'B'  AS [B],
	///		GETDATE() AS [C],
	///		CAST(55.5 AS MONEY) AS [D]";
	///		
	///	//映射关系 { ColumnName, DomainName }
	///	Dictionary<string, string> dict = new Dictionary<string, string>() { 
	///		{ "A", "拆分来源" },
	///		{ "B", "拆分类型" },
	///		{ "C", "科目编码" },
	///		{ "D", "拆分金额" }
	///	};
	///	
	/// //哈希字段,使用映射后的名称
	/// //拼接这两个字段的行值,并写入[合约规划使用明细列表比较域]中
	///	List<string> hash = new List<string>() { "拆分来源", "拆分类型" };
	///
	/// //数据表
	///	DataTable dt = CPQuery.From(sql).FillDataTable();
	///
	/// //声明绑定对象
	///	BindOption bo = new BindOption();
	///	bo.ColumnMap = dict;
	///	bo.HashColumns = hash;
	///	bo.IdentityDomain = "序号";  //循环域中的序号域将被填充为1,2,3,4....序列
	///	
	/// //绑定循环域
	///	btm.BindGroup("合约规划使用明细", dt, bo);
	/// ]]>
	/// </code>
	/// </example>
	public class BindOption
	{
		/// <summary>
		/// 表示需要计算哈希值的字段
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>在BindGroup函数中可用,Bind函数中本参数将被忽略。</description></item>
		/// <item><description>哈希值计算方法为:(哈希字段1!哈希字段2!哈希字段3...)(更多行...).ToString().GetHashCode().ToString()。</description></item>
		/// <item><description>哈希值将自动写入Name为[GroupName]+[比较域]或[GroupName]+[列表比较域]的Domain中</description></item>
		/// </list>
		/// </remarks>
		public List<string> HashColumns { get; set; }

		/// <summary>
		/// 表示字段名的映射关系
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>在BindGroup函数中可用,Bind函数中本参数将被忽略。</description></item>
		/// <item><description>映射格式为:KeyValuePair&lt;ColumnName,DomainName&gt;格式</description></item>
		/// </list>
		/// </remarks>
		public Dictionary<string, string> ColumnMap { get; set; }

		/// <summary>
		/// 表示循环域标识列的DomainName
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>在BindGroup函数中可用,Bind函数中本参数将被忽略。</description></item>
		/// <item><description>循环域中将为标识为1,2,3....的列</description></item>
		/// </list>
		/// </remarks>
		public string IdentityDomain { get; set; }
	}
}
