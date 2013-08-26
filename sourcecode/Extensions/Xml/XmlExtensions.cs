using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.Xml;

namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 包含XML操作的扩展方法类
	/// </summary>
	public static class XmlExtensions
	{
		/// <summary>
		/// 将指定对象序列化为XML字符串
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>序列化的xml时,使用Encoding.Default编码,在CodePage=936情况下,生成的xml头为GB2312</description></item>
		/// <item><description>扩展方法实际为<see cref="XmlHelper"/>类的XmlSerialize方法包装</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		/// <para>下面的代码演示了ToXml的用法</para>
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
		/// </example>
		/// <param name="o">对象</param>
		/// <returns>Xml字符串</returns>
		public static string ToXml(this object o)
		{
			return XmlHelper.XmlSerialize(o, Encoding.Default);
		}


		/// <summary>
		/// 将指定对象序列化为XML字符串
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了ToXml的用法</para>
		/// <code>
		/// <![CDATA[
		///		HttpResult hr = new HttpResult();
		///		hr.Result = true;
		///		hr.KeyValue = Guid.NewGuid().ToString();
		///		string xml = hr.ToXml(Encoding.UTF8);
		///		
		/// 	//xml的内容为:
		///		//<?xml version="1.0" encoding="utf-8" ?>
		///		//<xml result="true" keyvalue="生成的GUID值"/>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="o">对象</param>
		/// <param name="encoding">编码</param>
		/// <returns>Xml字符串</returns>
		public static string ToXml(this object o, Encoding encoding)
		{
			return XmlHelper.XmlSerialize(o, encoding);
		}




		/// <summary>
		/// 将指定Xml字符串反序列化为对象实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了FromXml的用法</para>
		/// <code>
		/// <![CDATA[
		///		string xml = @"<xml result='true' keyvalue='57013559-FDF3-4032-A784-2A4F45856B08'></xml>";
		///		HttpResult hr = xml.FromXml<HttpResult>();
		///		
		///		//hr的Result属性将被赋值为true
		///		//hr的KeyValue属性将被赋值为FD4D2306-1023-49FD-8D06-A873541AE222
		/// ]]>
		/// </code>
		/// </example>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="s">Xml字符串</param>
		/// <returns>对象实例</returns>
		public static T FromXml<T>(this string s) where T : class, new()
		{
			return XmlHelper.XmlDeserialize<T>(s, Encoding.Default);
		}

		/// <summary>
		/// 将指定Xml字符串反序列化为对象实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了FromXml的用法</para>
		/// <code>
		/// <![CDATA[
		///		string xml = @"<xml result='true' keyvalue='57013559-FDF3-4032-A784-2A4F45856B08'></xml>";
		///		HttpResult hr = xml.FromXml<HttpResult>(Encoding.Default);
		///		
		///		//hr的Result属性将被赋值为true
		///		//hr的KeyValue属性将被赋值为FD4D2306-1023-49FD-8D06-A873541AE222
		/// ]]>
		/// </code>
		/// </example>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="s">Xml字符串</param>
		/// <param name="encoding">xml编码方式</param>
		/// <returns>对象实例</returns>
		public static T FromXml<T>(this string s, Encoding encoding) where T : class, new()
		{
			return XmlHelper.XmlDeserialize<T>(s, encoding);
		}


	}
}
