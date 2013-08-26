using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.IO;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Xml;
using Mysoft.Map.Extensions.DAL;

namespace Mysoft.Map.Extensions.Workflow
{
	/// <summary>
	/// BusinessType包装类
	/// </summary>
	/// <example>
	/// <para>下面的代码演示了BusinessTypeManager的用法</para>
	/// <code>
	/// <![CDATA[
	/// 
	///		BusinessTypeManager btm = BusinessTypeManager.FromFile("/Map/Cbgl/Workflow/Demo1_HTML.xml");
	///		
	///		//真实的业务场景中,需要将BusinessGUID属性赋值为业务GUID
	///		btm.BusinessType.BusinessGUID = Guid.NewGuid();
	///		
	///		btm.SetFormat(DomainType.DateTime, "yyyy-MM-dd");
	///		btm.SetFormat(DomainType.Int, "#,##0.00");
	///		btm.SetFormat(DomainType.Money, "#,##0.00");
	///		DataTable dt;
	///		string sql = @"SELECT
	///					'Code1'  AS [合同编码],
	///					'合同名称'  AS [合同名称],
	///					GETDATE() AS [签订日期],
	///					'abc' AS [经办人],
	///					555 AS [IntVal]";
	///	
	///		dt = CPQuery.From(sql).FillDataTable();
	///		btm.Bind(dt);
	/// ]]>
	/// </code>
	/// </example>
	public sealed class BusinessTypeManager
	{
		private BusinessType _businessType;
		private Dictionary<DomainType, string> _dictFormat = new Dictionary<DomainType, string>();

		/// <summary>
		/// 从xml文件反序列化为对象
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了FromFile的用法</para>
		/// <code>
		/// <![CDATA[
		/// 
		///	//使用站点相对路径
		///	usinessTypeManager btm = BusinessTypeManager.FromFile("/Map/Cbgl/Workflow/Demo1_HTML.xml");
		///		
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="file">xml文件</param>
		/// <returns>BusinessTypeManager类实例</returns>
		public static BusinessTypeManager FromFile(string file)
		{
			if( string.IsNullOrEmpty(file) ) {
				throw new ArgumentNullException("file");
			}

			string fileName;

			HttpContext context = HttpContext.Current;
			if( context == null ) {
				string runtimeFolder = AppDomain.CurrentDomain.BaseDirectory;
				fileName = Path.Combine(runtimeFolder, file);
			}
			else{
				fileName = context.Server.MapPath(file);
			}

			string xml = File.ReadAllText(fileName, Encoding.Default);
			BusinessType bt = xml.FromXml<BusinessType>();
			BusinessTypeManager btm = new BusinessTypeManager();
			btm.BusinessType = bt;
			return btm;
		}

		/// <summary>
		/// 从xml字符串反序列化为对象
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了FromXML的用法</para>
		/// <code>
		/// <![CDATA[
		/// 
		///	string xml = "<BusinessType BusinessGUID="" ...>...";
		///	usinessTypeManager btm = BusinessTypeManager.FromXml(xml);
		///		
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="xml">xml字符串</param>
		/// <returns>BusinessTypeManager类实例</returns>
		public static BusinessTypeManager FromXml(string xml)
		{
			if( string.IsNullOrEmpty(xml) ) {
				throw new ArgumentNullException("xml");
			}
			BusinessType bt = xml.FromXml<BusinessType>();
			BusinessTypeManager btm = new BusinessTypeManager();
			btm.BusinessType = bt;
			return btm;
		}
		
		private BusinessTypeManager()
		{
		}
		
		/// <summary>
		/// BusinessType对象
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了BusinessType的用法</para>
		/// <code>
		/// <![CDATA[
		/// 
		///	BusinessTypeManager btm = BusinessTypeManager.FromFile("/Map/Cbgl/Workflow/Demo1_HTML.xml");
		///	btm.BusinessType.BusinessGUID = guid.ToString();
		///
		/// //工作流业务域XML将被序列化到xml变量中
		///	string xml = btm.BusinessType.ToXml();
		/// ]]>
		/// </code>
		/// </example>
		public BusinessType BusinessType
		{
			get { return _businessType; }
			private set { _businessType = value; }
		}
		

		/// <summary>
		/// 设置格式化
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了SetFormat的用法</para>
		/// <code>
		/// <![CDATA[
		/// 
		///		BusinessTypeManager btm = BusinessTypeManager.FromXml("...");
		///		btm.SetFormat(DomainType.DateTime, "yyyy-MM-dd");
		///		btm.SetFormat(DomainType.Int, "#,##0.00");
		///		btm.SetFormat(DomainType.Money, "#,##0.00");
		///		
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="type">Domain类型</param>
		/// <param name="format">格式化字符串</param>
		public void SetFormat(DomainType type, string format)
		{
			if( string.IsNullOrEmpty(format) ) {
				throw new ArgumentNullException("format");
			}

			_dictFormat[type] = format;
		}

		/// <summary>
		/// 业务域绑定事件
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了OnDomainBinding的用法</para>
		/// <code>
		/// <![CDATA[
		/// 
		/// //订阅事件
		/// public void Main(){
		///		BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
		///		btm.OnDomainBinding += new EventHandler<BindEventArgs>(btm_DomainBinding);
		///		btm.Bind();
		///	}
		///	
		/// //响应函数
		/// private void btm_DomainBinding(object sender, BindEventArgs e)
		///	{
		///		if( e.Domain.Name == "经办人" ) {
		///			e.Domain.Value = e.Value.ToString() + "-" + DateTime.Now.ToString();
		///		}
		///	}
		/// ]]>
		/// </code>
		/// </example>
		public event EventHandler<BindEventArgs> OnDomainBinding;

		/// <summary>
		/// 绑定一个业务域
		/// </summary>
		/// <param name="domainName">业务域名称</param>
		/// <param name="value">业务域值</param>
		public void BindDomain(string domainName, string value)
		{
			//Bind可以绑定大部分业务域
			//但如果存在需要计算后才能赋值的域,则需要调用本方法.
			//例如,表中存储的是3级编码,业务域中要求的是[一级]-[二级]-[三级]这种格式数据.
			//	则需要在程序中单独处理,单独绑定这一个业务域

			if( string.IsNullOrEmpty(domainName) ) {
				throw new ArgumentNullException("domainName");
			}

			Domain domain = GetItem<Domain>(domainName);
			if( domain != null ) {
				domain.Value = value;
			}
		}



		/// <summary>
		/// 绑定业务域
		/// </summary>
		/// <param name="table">数据表</param>
		public void Bind(DataTable table) 
		{
			Bind(table, new BindOption());
		}

		/// <summary>
		/// 绑定业务域
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>绑定选项信息,参见:<see cref="BindOption"/>类</description></item>
		/// </list>
		/// </remarks>
		/// <param name="table">数据表</param>
		/// <param name="option">绑定选项</param>
		public void Bind(DataTable table, BindOption option)
		{
			if( option == null ) {
				throw new ArgumentNullException("option");
			}

			DataTable dt = table;
			if( dt == null ) {
				if( _businessType.SQL == null ) {
					throw new ArgumentException("未设置数据源,且XML文件中SQL节点为空");
				}
				if( string.IsNullOrEmpty(_businessType.SQL.Value) ) {
					throw new ArgumentException("未设置数据源,且XML文件中SQL节点为空");
				}
				string sql = _businessType.SQL.Value;
				if( sql.IndexOf("[BusinessGUID]") != -1 ) {
					sql = sql.Replace("[BusinessGUID]", "@BusinessGUID");
					var parameter = new { BusinessGUID = _businessType.BusinessGUID };
					dt = CPQuery.From(sql, parameter).FillDataTable();
				}
				else {
					dt = CPQuery.From(sql).FillDataTable();
				}
			}

			if( dt.Rows.Count > 0 ) {

				Dictionary<string, Domain> dictDomains =
					new Dictionary<string, Domain>(_businessType.Item.Count);

				foreach( BaseItem item in _businessType.Item ) {
					Domain domain = item as Domain;
					if( domain != null ) {
						dictDomains[domain.Name] = domain;
					}
				}

				DataRow row = dt.Rows[0];
				foreach( DataColumn col in dt.Columns ) {

					string domainName = "";

					if( option.ColumnMap != null ) {
						option.ColumnMap.TryGetValue(col.ColumnName, out domainName);
					}
					else {
						domainName = col.ColumnName;
					}

					if( string.IsNullOrEmpty(domainName) ) {
						continue;
					}

					Domain domain;
					if( dictDomains.TryGetValue(domainName, out domain) ) {
						BindValue(domain, null, row[col], col.DataType);
					}
				}
			}
		}


		/// <summary>
		/// 绑定一个循环域
		/// </summary>
		/// <param name="groupName">循环域名称</param>
		/// <param name="table">数据表</param>
		public void BindGroup(string groupName, DataTable table) 
		{
			BindGroup(groupName, table, new BindOption());
		}

		/// <summary>
		/// 绑定一个循环域
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>绑定选项信息,参见:<see cref="BindOption"/>类</description></item>
		/// </list>
		/// </remarks>
		/// <param name="groupName">循环域名称</param>
		/// <param name="table">数据表</param>
		/// <param name="option">绑定选项</param>
		public void BindGroup(string groupName, DataTable table, BindOption option) 
		{
			DataTable dt = table;
			if( string.IsNullOrEmpty(groupName) ) {
				throw new ArgumentNullException("groupName");
			}

			if( option == null ) {
				throw new ArgumentNullException("option");
			}

			Group group = GetItem<Group>(groupName);
			if( group != null ) {
				if( dt == null ) {
					if( group.SQL == null ) {
						throw new ArgumentException("未设置数据源,且Group节点中未配置SQL节点");
					}

					if( string.IsNullOrEmpty(group.SQL.Value) ) {
						throw new ArgumentException("未设置数据源,且Group节点中SQL节点为空");
					}

					string sql = group.SQL.Value;
					if( sql.IndexOf("[BusinessGUID]") != -1 ) {
						sql = sql.Replace("[BusinessGUID]", "@BusinessGUID");
						var parameter = new { BusinessGUID = _businessType.BusinessGUID };
						dt = CPQuery.From(sql, parameter).FillDataTable();
					}
					else {
						dt = CPQuery.From(sql).FillDataTable();
					}
				}

				if( dt.Rows.Count == 0 || group.GroupItems.Count == 0 ) {
					return;
				}

				GroupItem baseItem = CloneGroupItem(group.GroupItems[0]);

				group.GroupItems.Clear();

				Dictionary<string, int> dictDomains =
					new Dictionary<string, int>(baseItem.Domains.Count);

				int index = 0;
				foreach( Domain domain in baseItem.Domains ) {
					dictDomains[domain.Name] = index;
					index++;
				}

				StringBuilder sb = new StringBuilder();
				string compareDomainName = "";

				if( group.Name.EndsWith("列表") ) {
					compareDomainName = group.Name + "比较域";
				}
				else {
					compareDomainName = group.Name + "列表比较域";
				}

				Domain compareDomain = GetItem<Domain>(compareDomainName);

				bool requireHash = false;
				if( compareDomain != null && option.HashColumns != null && option.HashColumns.Count > 0 ) {
					requireHash = true;
				}

				for( int i = 0; i < dt.Rows.Count; i++ ) {
					DataRow row = dt.Rows[i];
					GroupItem newItem = CloneGroupItem(baseItem);
					newItem.RowIndex = i + 1;
					foreach( DataColumn col in dt.Columns ) {

						string domainName = "";

						if( option.ColumnMap != null ) {
							option.ColumnMap.TryGetValue(col.ColumnName, out domainName);
						}
						else {
							domainName = col.ColumnName;
						}

						if( string.IsNullOrEmpty(domainName) ) {
							continue;
						}
						if( dictDomains.TryGetValue(domainName, out index) ) {
							Domain domain = newItem.Domains[index];
							BindValue(domain, group, row[col], col.DataType);
							if( requireHash && option.HashColumns.Contains(col.ColumnName) ) {
								object val = row[col];
								if( val != null && val != DBNull.Value ) {
									sb.AppendFormat("!{0}", val.ToString());
								}
							}
						}
					}
					if( !string.IsNullOrEmpty(option.IdentityDomain) ) {
						if( dictDomains.TryGetValue(option.IdentityDomain, out index) ) {
							Domain domain = newItem.Domains[index];
							domain.Value = newItem.RowIndex.ToString();
						}
					}
					group.GroupItems.Add(newItem);
				}

				if( requireHash ) {
					compareDomain.Value = sb.ToString().GetHashCode().ToString();
				}
			}

		}

		private GroupItem CloneGroupItem(GroupItem rawItem)
		{
			GroupItem newItem = new GroupItem();
			foreach( Domain rawDomain in rawItem.Domains ) {
				Domain newDomain = new Domain();
				newDomain.DefaultValue = rawDomain.DefaultValue;
				newDomain.DisplayType = rawDomain.DisplayType;
				newDomain.DropdownOptions = rawDomain.DropdownOptions;
				newDomain.IsApprovemodify = rawDomain.IsApprovemodify;
				newDomain.IsNull = rawDomain.IsNull;
				newDomain.IsUpdate = rawDomain.IsUpdate;
				newDomain.Length = rawDomain.Length;
				newDomain.Name = rawDomain.Name;
				newDomain.Type = rawDomain.Type;
				newDomain.Value = "";
				newItem.Domains.Add(newDomain);
			}
			return newItem;
		}

		private void BindValue(Domain domain, Group group,  object val, Type type)
		{
			if( val != null && val.Equals(DBNull.Value) == false ) {

				string format;

				if( type == typeof(DateTime) && _dictFormat.TryGetValue(DomainType.DateTime, out format)) {
					DateTime dtmVal = (DateTime)val;
					domain.Value = dtmVal.ToString(format);
				}
				else if( type == typeof(decimal) && _dictFormat.TryGetValue(DomainType.Decimal, out format) ) {
					decimal decVal = (decimal)val;
					domain.Value = decVal.ToString(format);
				}
				else if( type == typeof(decimal) && _dictFormat.TryGetValue(DomainType.Numeric, out format) ) {
					decimal decVal = (decimal)val;
					domain.Value = decVal.ToString(format);
				}
				else if( type == typeof(decimal) && _dictFormat.TryGetValue(DomainType.Money, out format) ) {
					decimal decVal = (decimal)val;
					domain.Value = decVal.ToString(format);
				}
				else if( type == typeof(float) && _dictFormat.TryGetValue(DomainType.Real, out format) ) {
					float floatVal = (float)val;
					domain.Value = floatVal.ToString(format);
				}
				else if( type == typeof(double) && _dictFormat.TryGetValue(DomainType.Float, out format) ) {
					double dblValue = (double)val;
					domain.Value = dblValue.ToString(format);
				}
				else if( type == typeof(int) && _dictFormat.TryGetValue(DomainType.Int, out format) ) {
					int intValue = (int)val;
					domain.Value = intValue.ToString(format);
				}
				else if( type == typeof(short) && _dictFormat.TryGetValue(DomainType.SmallInt, out format) ) {
					short shortValue = (short)val;
					domain.Value = shortValue.ToString(format);
				}
				else if( type == typeof(long) && _dictFormat.TryGetValue(DomainType.BigInt, out format) ) {
					long lngValue = (long)val;
					domain.Value = lngValue.ToString(format);
				}
				else {
					domain.Value = val.ToString();
				}

				EventHandler<BindEventArgs> handler = OnDomainBinding;
				if( handler != null ) {

					BindEventArgs arg = new BindEventArgs();
					arg.Domain = domain;
					arg.Value = val;
					arg.Group = group;
					arg.DataType = type;

					handler(null, arg);
				}
			}
		}

		private T GetItem<T>(string name) where T : BaseItem
		{
			foreach( BaseItem item in _businessType.Item ) {
				if( item.Name == name ) {
					return item as T;
				}
			}
			return null;
		}

		/// <summary>
		/// 获取业务域
		/// </summary>
		/// <param name="domainName">业务域名称</param>
		/// <returns>业务域对象</returns>
		public Domain GetDomain(string domainName)
		{
			return GetItem<Domain>(domainName);
		}

		/// <summary>
		/// 获取循环域
		/// </summary>
		/// <param name="groupName">循环域名称</param>
		/// <returns>循环域对象</returns>
		public Group GetGroup(string groupName)
		{
			return GetItem<Group>(groupName);
		}
	}
}
