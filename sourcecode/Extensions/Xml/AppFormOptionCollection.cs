using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using System.Data;
using System.Xml;
using System.IO;

namespace Mysoft.Map.Extensions.Xml
{
	/// <summary>
	/// 表示AppForm DefaultXml属性的封装
	/// </summary>
	public sealed class AppFormOptionCollection : List<AppFormOption>
	{

		/// <summary>
		/// 创建一个AppForm DefaultXml 集合
		/// </summary>
		/// <param name="query">CPQuery查询</param>
		/// <returns>DefaultXml集合</returns>
		public static AppFormOptionCollection Create(CPQuery query)
		{
			if( query == null ) {
				throw new ArgumentNullException("query");
			}

			AppFormOptionCollection options = new AppFormOptionCollection();
			DataTable table = query.FillDataTable();

			if( table.Rows.Count > 0 ) {
				DataRow row = table.Rows[0];
				foreach( DataColumn col in table.Columns ) {
					object objValue = row[col];
					string strValue;
					if( objValue == DBNull.Value || objValue == null ) {
						strValue = "";
					}
					else {
						strValue = objValue.ToString();
					}

					AppFormOption option = new AppFormOption() { FieldName = col.ColumnName, DefaultValue = strValue };

					options.Add(option);
				}
			}

			return options;
		}

		/// <summary>
		/// 转换为DefaultXml格式
		/// </summary>
		/// <returns>转换后的xml字符串</returns>
		public string ToXml()
		{
			StringBuilder sb = new StringBuilder(1024);
			XmlWriterSettings setting = new XmlWriterSettings();

			setting.Indent = true;
			setting.OmitXmlDeclaration = true;

			using( XmlWriter writer = XmlTextWriter.Create(sb, setting) ) {
				writer.WriteStartElement("xml");
				foreach( AppFormOption option in this ) {
					option.WriteXml(writer);
				}
				writer.WriteEndElement();
			}
			return sb.ToString();
		}

		/// <summary>
		/// 根据FieldName返回集合中对应的AppFromParameter对象
		/// </summary>
		/// <param name="key">FiledName属性</param>
		/// <returns>获取到的AppFromParameter对象</returns>
		public AppFormOption this[string key]
		{
			get
			{
				foreach( AppFormOption option in this ) {
					if( string.Compare(option.FieldName, key, false) == 0 ) {
						return option;
					}
				}
				return null;
			}
		}
	}
}
