using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Mysoft.Map.Extensions.CodeDom;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// DataTable扩展方法
	/// </summary>
	public static class DataTableExtensions
	{
		/// <summary>
		/// 从DataTable获取一个实体列表
		/// </summary>
		/// <param name="table">DataTable实例</param>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>建议不要直接从DataTable返回实体,而是通过CPQuery或者StoreProcedure返回实体</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		///		<para>下面的代码演示了从DataTable获取一个实体列表的用法</para>
		///		<code>
		///		<![CDATA[
		///		//存储过程中包含两个SELECT语句,返回两个结果集
		///		DataSet ds = StoreProcedure.Create("usp_GetTestDataType").FillDataSet();
		///	
		///		foreach( DataTable table in ds2.Tables ) {
		///
		///			//将DataTable转换为实体集合
		///			List<TestDataType> list = table.ToList<TestDataType>();
		///		
		///		}
		///		]]>
		///		</code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <exception cref="ArgumentNullException">table参数为null</exception>
		/// <returns>实体列表</returns>
		public static List<T> ToList<T>(this DataTable table) where T : class, new(){

			if (table == null)
				throw new ArgumentNullException("table");

			Type type = typeof(T);

			TypeDescription description = TypeDescriptionCache.GetTypeDiscription(type);

			if( description.ExecuteFunc != null )
				try {
					return description.ExecuteFunc(10, new object[] { table }) as List<T>;
				}
				catch( System.Exception ex ) {
					//这里不希望调用者看到代码生成器产生的代码结构,于是在这里抛出捕获到的异常
					throw ex;
				}
			else if( type.IsSubclassOf(typeof(BaseEntity)) ) 
				throw new InvalidProgramException(
						string.Format("类型 {0} 找不到ToList的操作方法，请确认已将实体类型定义在*.Entity.dll结尾的程序集中，且不是嵌套类，并已提供无参的构造函数。", type.FullName));
			else
				return DbHelper.ToList<T>(table, description);
			
		}
	}
}
