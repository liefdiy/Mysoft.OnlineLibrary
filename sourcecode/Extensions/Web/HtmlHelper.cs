using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mysoft.Map.Extensions.Xml;


namespace Mysoft.Map.Extensions.Web
{
	/// <summary>
	/// HTML操作的工具类
	/// </summary>
	public static class HtmlHelper
	{
		/// <summary>
		/// 根据XSLT模板和一个数据对象生成HTML代码
		/// </summary>
		/// <param name="xsltFilePath">XSLT模板文件的路径</param>
		/// <param name="obj">数据对象</param>
		/// <returns>生成的HTML代码</returns>
		public static string GetHtml(string xsltFilePath, object obj)
		{
			if( obj == null )
				throw new ArgumentNullException("obj");

			string xmlString = XmlHelper.XmlSerialize(obj, Encoding.UTF8);

			return GetHtml(xsltFilePath, xmlString);
		}

		/// <summary>
		/// 根据XSLT模板和一个 XML字符串 生成HTML代码
		/// </summary>
		/// <param name="xsltFilePath">XSLT模板文件的路径</param>
		/// <param name="xmlString">XML字符串</param>
		/// <returns>生成的HTML代码</returns>
		public static string GetHtml(string xsltFilePath, string xmlString)
		{
			if( string.IsNullOrEmpty(xmlString) )
				throw new ArgumentNullException("xmlString");

			XslCompiledTransform xsltransform = new XslCompiledTransform();
			xsltransform.Load(xsltFilePath);

			XmlDocument xml = new XmlDocument();
			xml.LoadXml(xmlString);

			using( MemoryStream ms = new MemoryStream() ) {
				XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
				xsltransform.Transform(xml, null, writer);
				//writer.Close();

				ms.Position = 0;

				StreamReader streamReader = new StreamReader(ms);
				return streamReader.ReadToEnd();
			}

		}

		//modify by 李俊峰 2013-06-03 由于用法发生改变,不建议在隐藏域中注册大对象,所以这些方法隐藏掉了.
		///// <summary>
		///// 根据指定的名称和字符串值，向当前页面注册一个隐藏域。
		///// </summary>
		///// <param name="hiddenFieldName">隐藏域的名称</param>
		///// <param name="hiddenFieldValue">隐藏域的值</param>
		//public static void RegisterHiddenInput(string hiddenFieldName, string hiddenFieldValue)
		//{
		//    if( string.IsNullOrEmpty(hiddenFieldName) )
		//        throw new ArgumentNullException("hiddenFieldName");
		//    if( string.IsNullOrEmpty(hiddenFieldValue) )
		//        throw new ArgumentNullException("hiddenFieldValue");

		//    HttpContext context = HttpContext.Current;
		//    if( context == null )
		//        throw new InvalidOperationException("当前线程不是ASP.NET工作线程，无法找到HttpContext的实例。");

		//    Page page = context.CurrentHandler as Page;
		//    if( page == null )
		//        throw new InvalidOperationException("当前请求的处理器不是一个Page实例，不能执行这个调用。");

		//    page.ClientScript.RegisterHiddenField(hiddenFieldName, hiddenFieldValue);
		//}

		///// <summary>
		///// 根据指定的名称和数据实体对象，向当前页面注册一个隐藏域。
		///// </summary>
		///// <remarks>
		///// <list type="bullet">
		///// <item><description>如果类包含[Serializable]标记,则使用二进制序列化</description></item>
		///// <item><description>如果类不包含[Serializable]标记,则使用XML序列化,XML编码方式为UTF-8</description></item>
		///// <item><description>序列化的结果将使用Base64编码,注册到界面隐藏域中</description></item>
		///// </list>
		///// </remarks>
		///// <typeparam name="T">数据实体的类型</typeparam>
		///// <param name="hiddenFieldName">隐藏域的名称</param>
		///// <param name="obj">数据实体对象</param>
		//public static void RegisterHiddenInput<T>(string hiddenFieldName, T obj) where T : class, new()
		//{
		//    if( obj == null )
		//        throw new ArgumentNullException("obj");

		//    Type objType = typeof(T);
		//    if( objType == typeof(string) )
		//        RegisterHiddenInput(hiddenFieldName, obj.ToString());

		//    if( objType.IsSupportBinSerializable() ) {
		//        byte[] buffer = BinSerializerHelper.Serialize(obj);
		//        string base64 = Convert.ToBase64String(buffer);
		//        RegisterHiddenInput(hiddenFieldName, base64);
		//    }
		//    else {
		//        string xml = XmlHelper.XmlSerialize(obj, Encoding.Unicode);
		//        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
		//        RegisterHiddenInput(hiddenFieldName, base64);
		//    }
		//}

		///// <summary>
		///// 从调用RegisterHiddenInput生成的隐藏域中读取数据，并转成指定的对象。
		///// </summary>
		///// <typeparam name="T">数据实体的类型</typeparam>
		///// <param name="hiddenFieldName">隐藏域的名称</param>
		///// <returns>返回最初调用RegisterHiddenInput时的第二个传入参数。</returns>
		//public static T GetObjectFromHiddenInput<T>(string hiddenFieldName) where  T: class, new()
		//{
		//    if( string.IsNullOrEmpty(hiddenFieldName) )
		//        throw new ArgumentNullException("hiddenFieldName");

		//    HttpContext context = HttpContext.Current;
		//    if( context == null )
		//        throw new InvalidOperationException("当前线程不是ASP.NET工作线程，无法找到HttpContext的实例。");

		//    string hiddenFieldValue = context.Request.Form[hiddenFieldName];

		//    Type objType = typeof(T);
		//    if( objType == typeof(string) )
		//        return hiddenFieldValue as T;


		//    if( objType.IsSupportBinSerializable() ) {
		//        byte[] buffer = Convert.FromBase64String(hiddenFieldValue);
		//        return BinSerializerHelper.Deserialize<T>(buffer);
		//    }
		//    else {
		//        string xml = Encoding.UTF8.GetString(Convert.FromBase64String(hiddenFieldValue));
		//        return XmlHelper.XmlDeserialize<T>(xml, Encoding.UTF8);
		//    }
		//}

	}
}
