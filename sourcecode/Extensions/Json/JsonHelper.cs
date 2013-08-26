using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;

namespace Mysoft.Map.Extensions.Json
{
	/// <summary>
	/// JSON序列化帮助类
	/// </summary>
	public static class JsonHelper
	{

		internal static Func<object, string> ToJsonFunc { get; set; }

		internal static Func<string, Type, object> FromJsonFunc { get; set; }

		private static string InternalJsonSerialize(object o)
		{
			if( o == null ) {
				throw new ArgumentNullException("o");
			}

			JavaScriptSerializer seri = new JavaScriptSerializer();
			return seri.Serialize(o);
		}

		private static object InternalJsonDeserialize<T>(string s)
		{
			if( string.IsNullOrEmpty(s) ) {
				throw new ArgumentNullException("s");
			}

			JavaScriptSerializer seri = new JavaScriptSerializer();
			return seri.Deserialize<T>(s);
		}

		/// <summary>
		/// 将对象序列化为JSON字符串
		/// </summary>
		/// <param name="o">对象</param>
		/// <returns>JSON字符串</returns>
		public static string JsonSerialize(object o)
		{
			if( o == null ) {
				throw new ArgumentNullException("o");
			}

			Func<object, string> func = ToJsonFunc;
			if( func == null ) {
				return InternalJsonSerialize(o);
			}
			else {
				return ToJsonFunc(o);
			}
		}

		/// <summary>
		/// 将json字符串反序列化为对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="s">json字符串</param>
		/// <returns>对象实例</returns>
		public static T JsonDeserialize<T>(string s)
		{
			if( string.IsNullOrEmpty(s) ) {
				throw new ArgumentNullException("s");
			}

			Func<string, Type, object> func = FromJsonFunc;
			if( func == null ) {
				return (T)InternalJsonDeserialize<T>(s);
			}
			else {
				return (T)FromJsonFunc(s, typeof(T));
			}
			
		}
	}
}
