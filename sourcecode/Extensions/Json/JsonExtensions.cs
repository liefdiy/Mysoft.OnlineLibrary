using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.Json;


namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 包含Json操作的扩展方法类
	/// </summary>
	public static class JsonExtensions
	{

		/// <summary>
		/// 将指定对象转换为Json字符串
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了ToJson的用法</para>
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
		/// <param name="o">对象</param>
		/// <returns>Json字符串</returns>
		public static string ToJson(this object o)
		{
			return JsonHelper.JsonSerialize(o);
		}

		/// <summary>
		/// 将指Json字符串反序列化为对象实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了FromJson的用法</para>
		/// <code>
		/// <![CDATA[
		///		string json = @"{"Result":true,"KeyValue":"FD4D2306-1023-49FD-8D06-A873541AE222","ErrorMessage":null}";
		///		HttpResult hr = json.FromJson<HttpResult>();
		///		
		///		//hr的Result属性将被赋值为true
		///		//hr的KeyValue属性将被赋值为FD4D2306-1023-49FD-8D06-A873541AE222
		/// ]]>
		/// </code>
		/// </example>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="s">Json字符串</param>
		/// <returns>对象实例</returns>
		public static T FromJson<T>(this string s) 
		{
			return JsonHelper.JsonDeserialize<T>(s);
		}
	}
}
