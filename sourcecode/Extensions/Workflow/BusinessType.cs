using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mysoft.Map.Extensions.Workflow
{
	/// <summary>
	/// 表示工作流xml的封装
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>大多数场景下无需单独使用BusinessType类,请直接使用BusinessTypeManager包装来管理</description></item>
	/// <item><description>BusinessType类与工作流xml通过序列化的方式进行转换</description></item>
	/// </list>
	/// </remarks>
	public class BusinessType
	{
		/// <summary>
		/// 表示业务对象对应的程序集，由开发人员填写，其他人员不能删除、修改
		/// </summary>
		[XmlAttribute("businessAssembly")]
		public string BusinessAssembly { get; set; }

		/// <summary>
		/// 表示业务对象对应的类，由开发人员填写，其他人员不能删除、修改
		/// </summary>
		[XmlAttribute("businessClassName")]
		public string BusinessClassName { get; set; }

		/// <summary>
		/// 表示业务GUID,默认为空，不需要填写
		/// </summary>
		[XmlAttribute("BusinessGUID")]
		public string BusinessGUID { get; set; }

		/// <summary>
		/// 表示BusinessType节点的SQL语句值.
		/// </summary>
		public SQL SQL { get; set; }

		/// <summary>
		/// 表示Domain节点与Group节点的集合
		/// </summary>
		[XmlArrayItem(typeof(Domain)),
		XmlArrayItem(typeof(Group))]
		public List<BaseItem> Item = new List<BaseItem>();
	}


	/// <summary>
	/// 由于Group与Domain处在同一个层级中,于是需要一个统一的基类
	/// </summary>
	public class BaseItem {

		/// <summary>
		/// 表示域名称，可以为中英文字符
		/// </summary>
		[XmlAttribute("name")]
		public string Name { get; set; }
	}

	/// <summary>
	/// 表示Domain节点
	/// </summary>
	public class Domain : BaseItem
	{
		/// <summary>
		/// 表示是否在归档时更新，只能是1或0
		/// </summary>
		[XmlAttribute("isupdate")]
		public int IsUpdate { get; set; }

		/// <summary>
		/// 表示是否允许为空，只能是1或0
		/// </summary>
		[XmlAttribute("isnull")]
		public int IsNull { get; set; }

		/// <summary>
		/// 表示数据类型，需要按数据库中对应字段数据类型名称填写（不包含长度），如varchar、nvarchar、int、money
		/// </summary>
		[XmlAttribute("type")]
		public string Type { get; set; }

		/// <summary>
		/// 标识数据的最大长度
		/// </summary>
		[XmlAttribute("len")]
		public int Length { get; set; }

		/// <summary>
		/// 表示显示类型
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>文本：text</description></item>
		/// <item><description>下拉选择：select</description></item>
		/// <item><description>文本区：textarea</description></item>
		/// <item><description>辅助录入(只读)：assistant_r</description></item>
		/// <item><description>辅助录入(可写)：assistant_w</description></item>
		/// <item><description>隐藏：hidden</description></item>
		/// <item><description>计算：calc</description></item>
		/// <item><description>数字：number</description></item>
		/// <item><description>合计：sum</description></item>
		/// <item><description>datetime</description></item>
		/// </list>
		/// </remarks>
		[XmlAttribute("displaytype")]
		public string DisplayType { get; set; }

		/// <summary>
		/// 表示配置选项；暂时无用
		/// </summary>
		[XmlAttribute("dropdownoptions")]
		public string DropdownOptions { get; set; }

		/// <summary>
		/// 表示默认值
		/// </summary>
		[XmlAttribute("default")]
		public string DefaultValue { get; set; }

		/// <summary>
		/// 表示是否允许在审批时修改，只能是1或0
		/// </summary>
		[XmlAttribute("isapprovemodify")]
		public int IsApprovemodify { get; set; }

		/// <summary>
		/// 表示是否用户在文档定义界面添加的域
		/// </summary>
		[XmlAttribute("isuser")]
		public int IsUser { get; set; }

		/// <summary>
		/// 表示Domain节点中的businessdynamic属性
		/// </summary>
		[XmlAttribute("businessdynamic")]
		public int BusinessDynamic { get; set; }

		/// <summary>
		/// 表示Domain节点中的dynamic属性
		/// </summary>
		[XmlAttribute("dynamic")]
		public int Dynamic { get; set; }

		/// <summary>
		/// 表示Domain节点中的数据值
		/// </summary>
		[XmlText]
		public string Value { get; set; }
	}

	/// <summary>
	/// 表示xml文件中的Group节点
	/// </summary>
	public class Group : BaseItem
	{
		/// <summary>
		/// 表示Group节点下的SQL语句值
		/// </summary>
		public SQL SQL { get; set; }

		/// <summary>
		/// 表示Group节点下每行Item节点集合
		/// </summary>
		[XmlElement("Item")]
		public List<GroupItem> GroupItems = new List<GroupItem>();
	}

	/// <summary>
	/// 表示Group节点中的一行
	/// </summary>
	public class GroupItem
	{
		/// <summary>
		/// 标识循环域的行号
		/// </summary>
		[XmlAttribute("rowIndex")]
		public int RowIndex { get; set; }

		/// <summary>
		/// 当前Item行下的Domain集合
		/// </summary>
		[XmlElement("Domain")]
		public List<Domain> Domains = new List<Domain>();
	}

	/// <summary>
	/// 表示SQL节点.在商业地产及后续平台中可用
	/// </summary>
	public class SQL
	{
		/// <summary>
		/// 表示SQL节点内的值
		/// </summary>
		[XmlText]
		public string Value;
	}


}
