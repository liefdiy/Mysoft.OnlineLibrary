using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.CodeDom;
using Mysoft.Map.Extensions.Json;

namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 表示初始化配置信息(连接字符串,预编译实体)
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>通过UnSafeInit(connectionString)方法指定连接字符串;</description></item>
	/// <item><description>本类在非Map项目中使用,如果需要在Map项目中使用数据访问层,请使用<see cref="InitializerModule"/>类;</description></item>
	/// </list>
	/// </remarks>
	/// <exception cref="ArgumentNullException">连接字符串为null或为空</exception>
	/// <example>
	/// <para>下面的代码演示了如何初始化连接字符串</para>
	/// <code>
	/// using System;
	/// using System.Collections.Generic;
	/// using System.Linq;
	/// using System.Text;
	/// using Mysoft.Map.Extensions.DAL;
	/// 
	/// namespace SmokingTest.DAL
	/// {
	/// 	class Program
	/// 	{
	/// 		static void Main(string[] args)
	/// 		{
	/// 			string connectionString = @"server=localhost\sqlexpress;database=MyNorthwind;Integrated Security=SSPI";
	/// 
	/// 			//指定连接字符串
	/// 			Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
	/// 
	/// 		}
	/// 	}
	/// }
	/// </code>
	/// </example>
	public static class Initializer
	{

		private static bool s_inited = false;

		/// <summary>
		/// 初始化连接字符串,预编译实体
		/// </summary>
		/// <exception cref=" Mysoft.Map.Extensions.Exception.BuildException">预编译实体过程中产生异常</exception>
		/// <exception cref="InvalidOperationException">多次调用本函数</exception>
		/// <param name="connectionString">连接字符串</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void UnSafeInit(string connectionString)
		{
			if( s_inited ) 
				throw new InvalidOperationException("请不要多次调用UnSafeInit方法!");
			

			// 设置默认的连接字符串。
			ConnectionScope.SetDefaultConnection(connectionString);

			s_inited = true;
			
			BuildManager.StartAutoCompile();

		}
	}
}
