using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using System.Data;
using System.Xml;

namespace Mysoft.Map.Extensions.Xml
{
	/// <summary>
	/// 表示AppFrom的DefaultXml属性中的一条记录
	/// </summary>
	public sealed class AppFormOption
	{
		/// <summary>
		/// 表示DefaultXml中的xml节点名称
		/// </summary>
		public string FieldName { get; set; }
		/// <summary>
		/// 表示DefaultXml中的defaultvalue属性
		/// </summary>
		public string DefaultValue { get; set; }
		/// <summary>
		/// 表示DefaultXml中的createapi属性
		/// </summary>
		public string CreateAPI { get; set; }
		/// <summary>
		/// 表示DefaultXml中的updateapi属性
		/// </summary>
		public string UpdateAPI { get; set; }
		/// <summary>
		/// 表示DefaultXml中的req属性
		/// </summary>
		public string Required { get; set; }
		/// <summary>
		/// 表示DefaultXml中的editvalue属性
		/// </summary>
		public string EditValue { get; set; }

		private Dictionary<string, string> _dictAttrs;

		internal void WriteXml(XmlWriter writer)
		{
			if( string.IsNullOrEmpty(FieldName) == false ) {
				writer.WriteStartElement(FieldName);
				if( string.IsNullOrEmpty(DefaultValue) == false ) {
					writer.WriteAttributeString("defaultvalue", DefaultValue);
				}
				if( string.IsNullOrEmpty(EditValue) == false ) {
					writer.WriteAttributeString("editvalue", EditValue);
				}
				if( string.IsNullOrEmpty(UpdateAPI) == false ) {
					writer.WriteAttributeString("updateapi", UpdateAPI);
				}
				if( string.IsNullOrEmpty(CreateAPI) == false ) {
					writer.WriteAttributeString("createapi", CreateAPI);
				}
				if( string.IsNullOrEmpty(Required) == false ) {
					writer.WriteAttributeString("req", Required);
				}

				if( _dictAttrs != null ) {
					foreach( KeyValuePair<string, string> kvp in _dictAttrs ) {
						writer.WriteAttributeString(kvp.Key, kvp.Value);
					}
				}

				writer.WriteEndElement();
			}
		}

		/// <summary>
		/// 表示未定义为属性成员的其他DefaultXml属性
		/// </summary>
		/// <param name="key">属性名称</param>
		/// <returns>属性值</returns>
		public string this[string key]
		{
			get
			{
				if( _dictAttrs == null ) {
					return null;
				}
				string value;
				_dictAttrs.TryGetValue(key, out value);
				return value;
			}
			set
			{
				if( string.IsNullOrEmpty(key) ) {
					throw new ArgumentNullException("key");
				}

				if( value == null ) {
					throw new ArgumentNullException("value");
				}

				switch(key.ToLower()){
					case "defaultvalue":
					case "editvalue":
					case "updateapi":
					case "createapi":
					case "req":
						throw new ArgumentOutOfRangeException("索引器不允许指定defaultvalue、editvalue、updateapi、updateapi、createapi、req作为索引器键值。");
				}

				if( _dictAttrs == null ) {
					_dictAttrs = new Dictionary<string, string>();
				}
				_dictAttrs[key] = value;
			}
		}
	}
}
