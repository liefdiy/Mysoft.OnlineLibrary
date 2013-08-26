using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mysoft.Map.Extensions.Web
{
	/// <summary>
	/// 表示通用的返回值封装
	/// </summary>
	/// <example>
	/// <para>下面的代码演示了HttpResult转化为xml字符串的用法</para>
	/// <code>
	/// <![CDATA[
	///		HttpResult hr = new HttpResult();
	///		hr.Result = true;
	///		hr.KeyValue = Guid.NewGuid().ToString();
	///		string xml = hr.ToXml();
	///		
	///		//xml的内容为:
	///		//<?xml version="1.0" encoding="GB2312" ?>
	///		//<xml result="true" keyvalue="生成的GUID值"/>
	/// ]]>
	/// </code>
	/// <para>下面的代码演示了HttpResult转化为json字符串的用法</para>
	/// <code>
	/// <![CDATA[
	///		HttpResult hr = new HttpResult();
	///		hr.Result = true;
	///		hr.KeyValue = Guid.NewGuid().ToString();
	///		string json = hr.ToJson();
	///		
	/// 	//json的内容为:
	///		//{Result : "true", KeyValue : "生成的GUID值", ErrorMessage : "null"}
	/// ]]>
	/// </code>
	/// </example>
	[XmlRoot(ElementName = "xml")]
	public class HttpResult
	{
		/// <summary>
		/// 表示结果是否正确
		/// </summary>
		[XmlAttribute("result")]
		public bool Result { get; set; }

		/// <summary>
		/// 表示结果的键值
		/// </summary>
		[XmlAttribute("keyvalue")]
		public string KeyValue { get; set; }

		/// <summary>
		/// 表示结果的错误消息
		/// </summary>
		[XmlAttribute("errormessage")]
		public string ErrorMessage { get; set; }
	}

	/// <summary>
	/// 表示包含一个数据值的通用返回值封装
	/// </summary>
	/// <typeparam name="T">数据值类型</typeparam>
	[XmlRoot(ElementName="xml")]
	public class HttpResult<T> : HttpResult
	{
		/// <summary>
		/// 返回值包含的数据
		/// </summary>
		[XmlAttribute("data")]
		public T Data { get; set; }
	}
}
