using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Json;


namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 表示初始化HttpModule,用于初始化连接字符串,预编译实体
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>本类通过反射方式加载Mysoft.Map.Core.dll程序集中MyDB类中的链接字符串,并初始化</description></item>
	/// <item><description>本类只能使用在Map项目中.不支持在其他项目中调用本类,如果需要在其他项目中使用数据访问层,请使用<see cref="Initializer"/>类</description></item>
	/// </list>
	/// </remarks>
	/// <exception cref="System.IO.FileNotFoundException">Mysoft.Map.Core.dll文件不存在或Mysoft.Map.Data.MyDB类不存在</exception>
	/// <example>
	/// <para>下面的代码演示了如何在web.config文件中配置HttpModule</para>
	/// <code>
	///&lt;?xml version="1.0"?&gt;
	///&lt;configuration&gt;
	///	 &lt;system.web&gt;
	///    &lt;httpModules&gt;
	///      &lt;add name="MapExtends" type="Mysoft.Map.Extensions.InitializerModule,Mysoft.Map.Extensions" /&gt;
	///    &lt;/httpModules&gt;
	///  &lt;/system.web&gt;
	///  &lt;system.webServer&gt;
	///    &lt;modules&gt;
	///      &lt;remove name="ScriptModule" /&gt;
	///	  &lt;add name="MapExtends" preCondition="managedHandler" type="Mysoft.Map.Extensions.InitializerModule,Mysoft.Map.Extensions" /&gt;
	///    &lt;/modules&gt;
	///  &lt;/system.webServer&gt;
	///&lt;/configuration&gt;
	/// </code>
	/// </example>
	public class InitializerModule : IHttpModule
	{
		/// <summary>
		/// 初始化连接字符串,预编译实体
		/// </summary>
		static InitializerModule()
		{
			//初始化MyDB的连接字符串
			InitMyDBAPI();

			//modify by 李俊峰 2013-05-29 由于现在提供了直接转换xml为实体的功能,此处不需要初始化这个dll了
			//初始化InitAdditionaAPI
			//InitAdditionalAPI();

			//初始化AjaxAPI
			//InitAjaxAPI();

			//初始化Map的TableTrace行为
			TableTrace.InitTrace();
		}




		private static void InitMyDBAPI()
		{
			string path = Mysoft.Map.Extensions.CodeDom.BuildManager.BinDirectory + "Mysoft.Map.Core.dll";
			if( !System.IO.File.Exists(path) ) 
				throw new System.IO.FileNotFoundException("Mysoft.Map.Core.dll文件不存在!");
			
			Assembly assembly = Assembly.LoadFile(path);

			Type type = assembly.GetType("Mysoft.Map.Data.MyDB");
			if( type == null ) {
				throw new InvalidProgramException("Mysoft.Map.Core.dll中未找到MyDB类型!");
			}
			

			string connectionString = (string)type.InvokeMember("GetSqlConnectionString",
				BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, null);

			//初始化连接字符串
			Initializer.UnSafeInit(connectionString);
		}

		private static void InitAdditionalAPI()
		{
			//string path = Mysoft.Map.Extensions.CodeDom.BuildManager.BinDirectory + "Mysoft.Map.AdditionalAPI.dll";
			//if( !System.IO.File.Exists(path) ) {
			//    throw new System.IO.FileNotFoundException("Mysoft.Map.AdditionalAPI.dll文件不存在!");
			//}

			//Assembly assembly = Assembly.LoadFile(path);

			//Type type = assembly.GetType("Mysoft.Map.AdditionalAPI.XmlConvertor");
			//if( type == null ) {
			//    throw new System.IO.FileNotFoundException("Mysoft.Map.AdditionalAPI.dll中未找到XmlConvertor类型!");
			//    throw new InvalidProgramException("Mysoft.Map.AdditionalAPI.dll中未找到XmlConvertor类型!");
			//}

			//MethodInfo miSaveXml = type.GetMethod("ParseAppFormXmlToSqlCommand", BindingFlags.Static | BindingFlags.Public);

			//if( miSaveXml == null )
			//    throw new InvalidProgramException("没有找到期望的 Mysoft.Map.AdditionalAPI.XmlConvertor.ParseAppFormXmlToSqlCommand 方法。");

			//Mysoft.Map.Extensions.Xml.XmlDataEntity.SaveXmlFunc = Delegate.CreateDelegate(typeof(Func<string, string, List<KeyValuePair<string, SqlCommand>>>), miSaveXml) 
			//    as Func<string, string, List<KeyValuePair<string, SqlCommand>>>;
		}

		//private static void InitAjaxAPI()
		//{
		//    string path = Mysoft.Map.Extensions.CodeDom.BuildManager.BinDirectory + "Mysoft.MAP2.Ajax.dll";
		//    if( !System.IO.File.Exists(path) ) {
		//        throw new System.IO.FileNotFoundException("Mysoft.MAP2.Ajax.dll文件不存在!");
		//    }

		//    Assembly assembly = Assembly.LoadFile(path);

		//    Type type = assembly.GetType("Mysoft.MAP2.Ajax.Json");
		//    if( type == null ) {
		//        throw new InvalidProgramException("Mysoft.MAP2.Ajax.dll中未找到Json类型!");
		//    }

		//    MethodInfo miSeri = type.GetMethod("Serialize", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string) }, null);
		//    MethodInfo miDesri = type.GetMethod("Deserialize", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string), typeof(Type) }, null);

		//    if( miSeri == null )
		//        throw new InvalidProgramException("没有找到期望的 Mysoft.MAP2.Ajax.Json.Serialize 方法。");
		//    if( miDesri == null )
		//        throw new InvalidProgramException("没有找到期望的 Mysoft.MAP2.Ajax.Json.Deserialize 方法。");

		//    JsonHelper.ToJsonFunc = Delegate.CreateDelegate(typeof(Func<object, string>), miSeri) as Func<object, string>;
		//    JsonHelper.FromJsonFunc = Delegate.CreateDelegate(typeof(Func<string, Type, object>), miDesri) as Func<string, Type, object>;

		//}

		/// <summary>
		/// 此方法为Asp.net运行时调用.不支持在代码中直接调用.
		/// </summary>
		/// <param name="app">HttpApplication实例</param>
		public void Init(HttpApplication app)
		{
			app.EndRequest += app_EndRequest;
		}

		void app_EndRequest(object sender, EventArgs e)
		{
			ConnectionScope.ForceClose();
		}


		/// <summary>
		/// 此方法为Asp.net运行时调用.不支持在代码中直接调用.
		/// </summary>
		public void Dispose()
		{
		}

	}
}
