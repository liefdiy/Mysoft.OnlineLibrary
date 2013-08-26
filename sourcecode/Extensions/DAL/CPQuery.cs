using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Mysoft.Map.Extensions.Reflection;
using System.Data;
using System.Xml;

//========================================================

// CPQuery 是一个解决拼接SQL的新方法。
// CPQuery 可以让你采用拼接SQL的方式来编写参数化SQL 。

// 关于CPQuery的更多介绍请浏览：http://www.cnblogs.com/fish-li/archive/2012/09/10/CPQuery.html

// CPQuery 是一个开源的工具类，请在使用 CPQuery 时保留这段注释。

// 【 删除开源代码中的注释是可耻的行为！ 】

//========================================================



namespace Mysoft.Map.Extensions.DAL
{

	/// <summary>
	/// 表示存SQL查询调用的封装
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>CPQuery使用参数化查询SQL,可以通过匿名对象、SqlParameter数组的方式添加参数</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <para>下面的代码演示了通过拼接字符串参数,创建CPQuery对象实例的用法</para>
	/// <code>
	/// //字符串可以直接转换为CPQuery(需要引用using Mysoft.Map.Extensions.DAL命名空间)
	/// //对于非字符串类型参数,可以直接用+拼接,例如Guid,Int,DateTime...
	/// //对于字符串类型参数,需要调用AsQueryParameter()来进行拼接,否则就直接变为字符串了.
	/// var query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
	///         + Guid.NewGuid()
	///         + "," + "dddddddddd".AsQueryParameter() + ")";
	/// //执行命令
	/// query.ExecuteNonQuery();
	/// </code>
	/// <para>下面的代码演示了通过匿名对象添加参数,创建CPQuery对象实例的用法</para>
	/// <code>
	/// //声明匿名类型
	/// var product = new {
	///		ProductName = "产品名称",
	///		Quantity = 10
	/// };
	/// 
	/// //SQL中的参数名就是@加匿名类型的属性名
	/// CPQuery.From("INSERT INTO Products(ProductName, Quantity) VALUES(@ProductName, @Quantity)", product).ExecuteNonQuery();
	/// </code>
	/// <para>下面的代码演示了通过SqlParameter数组添加参数,创建CPQuery对象实例的用法</para>
	/// <code>
	/// //声明参数数组
	/// SqlParameter[] parameters2 = new SqlParameter[2];
	///	parameters2[0] = new SqlParameter("@ProductID", SqlDbType.Int);
	///	parameters2[0].Value = newProductId;
	///	parameters2[1] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
	///	parameters2[1].Value = "测试产品名";
	///	//执行查询并返回实体
	///	Products product = CPQuery.From("SELECT * FROM Products WHERE ProductID = @ProductID AND ProductName=@ProductName", parameters2).ToSingle&lt;Products&gt;();
	/// </code>
	/// </example>
	public sealed class CPQuery : IDbExecute
	{
		private enum SPStep	// 字符串参数的处理进度
		{
			NotSet,		// 没开始或者已完成一次字符串参数的拼接。
			EndWith,	// 拼接时遇到一个单引号结束
			Skip		// 已跳过一次拼接
		}

		private int _count;
		private StringBuilder _sb = new StringBuilder(512);

		private SqlCommand _command = new SqlCommand();

		/// <summary>
		/// 获取当前CPQuery内部的SqlCommand对象
		/// </summary>
		public SqlCommand Command
		{
			get { return _command; }
		}

		internal SqlCommand GetCommand()
		{
			_command.CommandText = _sb.ToString();
			return _command;
		}

		internal CPQuery(string text)
		{
			this.AddSqlText(text);
		}

		/// <summary>
		/// 创建一个CPQuery实例
		/// </summary>
		/// <returns>CPQuery实例</returns>
		public static CPQuery Create()
		{
			return new CPQuery(null);
		}


		/// <summary>
		/// 返回CPQuery中生成的SQL语句
		/// </summary>
		/// <returns>SQL语句</returns>
		public override string ToString()
		{
			return _sb.ToString();
		}

		
		private void AddSqlText(string s)
		{
			if( string.IsNullOrEmpty(s) )
				return;

			_sb.Append(s);
		}

		private void AddParameter(QueryParameter p)
		{
			string name = "@p" + (_count++).ToString();

			_sb.Append(name);

			_command.Parameters.AddWithValue(name, p.Value);
		}

		/// <summary>
		/// 通过参数化SQL、匿名对象的方式,创建CPQuery对象实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了通过参数化SQL,匿名对象的方式,创建CPQuery对象实例的用法</para>
		/// <code>
		/// //声明匿名类型
		/// var product = new {
		///		ProductName = "产品名称",
		///		Quantity = 10
		/// };
		/// 
		/// //SQL中的参数名就是@加匿名类型的属性名
		/// CPQuery.From("INSERT INTO Products(ProductName, Quantity) VALUES(@ProductName, @Quantity)", product).ExecuteNonQuery();
		/// </code>
		/// </example>
		/// <param name="parameterizedSQL">参数化的SQL字符串</param>
		/// <param name="argsObject">匿名对象</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery From(string parameterizedSQL, object argsObject)
		{
			if( string.IsNullOrEmpty(parameterizedSQL) )
				throw new ArgumentNullException("parameterizedSQL");


			CPQuery query = new CPQuery(parameterizedSQL);

			if( argsObject != null ) {
				PropertyInfo[] properties = argsObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
				foreach( PropertyInfo pInfo in properties ) {
					object value = pInfo.FastGetValue(argsObject);
					string name = "@" + pInfo.Name;

					if( value == null || value == DBNull.Value ) {
						query._command.Parameters.AddWithValue(name, DBNull.Value);
						//SqlParameter paramter = new SqlParameter(name, DBNull.Value);
						//paramter.SqlDbType = SqlDbType.Variant;
						//query._command.Parameters.Add(paramter);
						//throw new ArgumentException("输入参数的属性值不能为空。");
					}
					else {
						SqlParameter parameter = value as SqlParameter;
						if( parameter != null ) {
							query._command.Parameters.Add(parameter);
						}
						else {
							query._command.Parameters.AddWithValue(name, value);
						}
					}
				}
			}

			return query;
		}


		/// <summary>
		/// 通过参数化SQL、SqlParameter数组的方式，创建CPQuery实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了通过参数化SQL、SqlParameter数组的方式，创建CPQuery实例的用法</para>
		/// <code>
		/// //声明参数数组
		/// SqlParameter[] parameters2 = new SqlParameter[2];
		/// parameters2[0] = new SqlParameter("@ProductID", SqlDbType.Int);
		/// parameters2[0].Value = 0;
		/// parameters2[1] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
		/// parameters2[1].Value = "测试产品名";
		/// //执行查询并返回实体
		/// Products product = CPQuery.From("SELECT * FROM Products WHERE ProductID = @ProductID AND ProductName=@ProductName", parameters2).ToSingle&lt;Products&gt;();
		/// </code>
		/// </example>
		/// <param name="parameterizedSQL">参数化的SQL字符串</param>
		/// <param name="parameters">SqlParameter参数数组</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery From(string parameterizedSQL, params SqlParameter[] parameters)
		{
			CPQuery query = new CPQuery(parameterizedSQL);
			if( parameters != null ) {
				query._command.Parameters.AddRange(parameters);
			}
			return query;
		}



		/// <summary>
		/// 通过SQL语句,创建CPQuery对象实例
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了通过SQL语句,创建CPQuery对象实例的用法</para>
		/// <code>
		/// //本SQL不需要任何参数,从数据库中获取GUID
		/// Guid guid = CPQuery.From("SELECT NEWID()").ExecuteScalar&lt;Guid&gt;();
		/// </code>
		/// </example>
		/// <param name="parameterizedSQL">参数化的SQL字符串</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery From(string parameterizedSQL)
		{
			return From(parameterizedSQL, null);
		}


		/// <summary>
		/// 将字符串拼接到CPQuery对象
		/// </summary>
		/// <param name="query">CPQuery对象实例</param>
		/// <param name="s">字符串</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery operator +(CPQuery query, string s)
		{
			query.AddSqlText(s);
			return query;
		}

		/// <summary>
		/// 将QueryParameter实例拼接到CPQuery对象
		/// </summary>
		/// <param name="query">CPQuery对象实例</param>
		/// <param name="p">QueryParameter对象实例</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery operator +(CPQuery query, QueryParameter p)
		{
			query.AddParameter(p);
			return query;
		}


		/// <summary>
		/// 将SqlParameter实例拼接到CPQuery对象
		/// </summary>
		/// <param name="query">CPQuery对象实例</param>
		/// <param name="p">SqlParameter对象实例</param>
		/// <returns>CPQuery对象实例</returns>
		public static CPQuery operator +(CPQuery query, SqlParameter p)
		{
			query.AddSqlText(p.ParameterName);
			query._command.Parameters.Add(p);
			return query;
		}

		internal static CPQuery Format(string format, params object[] parameters)
		{
			if( string.IsNullOrEmpty(format) )
				throw new ArgumentNullException("format");


			if( parameters == null || parameters.Length == 0 )
				return format.AsCPQuery();


			string[] arguments = new string[parameters.Length];
			for( int i = 0; i < parameters.Length; i++ )
				arguments[i] = "@p" + i.ToString();

			var query = string.Format(format, arguments).AsCPQuery();

			for(int i = 0; i < parameters.Length; i++) {
				object value = parameters[i];

				if( value == null || value == DBNull.Value ) {
					query._command.Parameters.AddWithValue(arguments[i], DBNull.Value);
					//SqlParameter paramter = new SqlParameter(arguments[i], DBNull.Value);
					//paramter.SqlDbType = SqlDbType.Variant;
					//query._command.Parameters.Add(paramter);
					//throw new ArgumentException("输入参数的属性值不能为空。");
				}
				else {
					query._command.Parameters.AddWithValue(arguments[i], value);
				}
			}

			return query;
		}



		#region Execute 方法

		/// <summary>
		/// 执行命令,并返回影响函数
		/// </summary>
		/// <returns>影响行数</returns>
		public int ExecuteNonQuery()
		{
			return DbHelper.ExecuteNonQuery(this.GetCommand());
		}
       

		/// <summary>
		/// 执行命令,并将结果集填充到DataTable
		/// </summary>
		/// <returns>数据集</returns>
		public DataTable FillDataTable()
		{
			return DbHelper.FillDataTable(this.GetCommand());
		}


		/// <summary>
		/// 执行查询,并将结果集填充到DataSet
		/// </summary>
		/// <returns>数据集</returns>
		public DataSet FillDataSet()
		{
			return DbHelper.FillDataSet(this.GetCommand());
		}

		/// <summary>
		/// 执行命令,返回第一行,第一列的值,并将结果转换为T类型
		/// </summary>
		/// <typeparam name="T">返回值类型</typeparam>
		/// <returns>结果集的第一行,第一列</returns>
		public T ExecuteScalar<T>()
		{
			return DbHelper.ExecuteScalar<T>(this.GetCommand());
		}

		/// <summary>
		/// 执行命令,将第一列的值填充到类型为T的行集合中
		/// </summary>
		/// <typeparam name="T">返回值类型</typeparam>
		/// <returns>结果集的第一列集合</returns>
		public List<T> FillScalarList<T>()
		{
			return DbHelper.FillScalarList<T>(this.GetCommand());
		}

		/// <summary>
		/// 执行命令,将结果集转换为实体集合
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了如何返回实体集合</para>
		/// <code>
		/// List&lt;TestDataType&gt; list = CPQuery.Format("SELECT * FROM TestDataType").ToList&lt;TestDataType&gt;();
		/// </code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <returns>实体集合</returns>
		public List<T> ToList<T>() where T : class, new()
		{
			return DbHelper.ToList<T>(this.GetCommand());
		}
		/// <summary>
		/// 执行命令,将结果集转换为实体
		/// </summary>
		/// <typeparam name="T">实体类型</typeparam>
		/// <returns>实体</returns>
		/// <example>
		/// <para>下面的代码演示了如何返回实体</para>
		/// <code>
		///  TestDataType obj = "SELECT TOP 1 * FROM TestDataType".AsCPQuery().ToSingle&lt;TestDataType&gt;();
		/// </code>
		/// </example>
		public T ToSingle<T>() where T : class, new()
		{
			return DbHelper.ToSingle<T>(this.GetCommand());
		}

		

		#endregion

	}

	/// <summary>
	/// 表示一个SQL参数对象
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>string类型需要显示调用AsQueryParameter()扩展方法或通过通过强制类型转换的方式拼接,如:(QueryParameter)xxxx</description></item>
	/// <item><description>其他类型直接通过+操作即可</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <para>下面的代码演示了QueryParameter类的用法</para>
	/// <code>
	/// //字符串可以直接转换为AsQueryParameter(需要引用using Mysoft.Map.Extensions.DAL命名空间)
	/// var query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
	///         + Guid.NewGuid() 
	///         + "," + "dddddddddd".AsQueryParameter() + ")";
	/// //执行命令
	/// query.ExecuteNonQuery();
	/// </code>
	/// </example>
	public sealed class QueryParameter
	{
		private object _val;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="val">要包装的参数值</param>
		public QueryParameter(object val)
		{
			_val = val;
		}

		/// <summary>
		/// 参数值
		/// </summary>
		public object Value
		{
			get { return _val; }
		}


		/// <summary>
		/// 将string显式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static explicit operator QueryParameter(string value)
		{
			return new QueryParameter(value);
		}




		/// <summary>
		/// 将DBNull隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(DBNull value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将bool隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(bool value)
		{
			return new QueryParameter(value);
		}

		///// <summary>
		///// 将char隐式转换为QueryParameter
		///// </summary>
		///// <param name="value">要转换的值</param>
		///// <returns>QueryParameter实例</returns>
		//public static implicit operator QueryParameter(char value)
		//{
		//    return new QueryParameter(value);
		//}

		///// <summary>
		///// 将sbyte隐式转换为QueryParameter
		///// </summary>
		///// <param name="value">要转换的值</param>
		///// <returns>QueryParameter实例</returns>
		//public static implicit operator QueryParameter(sbyte value)
		//{
		//    return new QueryParameter(value);
		//}

		/// <summary>
		/// 将byte隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(byte value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将int隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(int value)
		{
			return new QueryParameter(value);
		}

		///// <summary>
		///// 将uint隐式转换为QueryParameter
		///// </summary>
		///// <param name="value">要转换的值</param>
		///// <returns>QueryParameter实例</returns>
		//public static implicit operator QueryParameter(uint value)
		//{
		//    return new QueryParameter(value);
		//}

		/// <summary>
		/// 将long隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(long value)
		{
			return new QueryParameter(value);
		}

		///// <summary>
		///// 将ulong隐式转换为QueryParameter
		///// </summary>
		///// <param name="value">要转换的值</param>
		///// <returns>QueryParameter实例</returns>
		//public static implicit operator QueryParameter(ulong value)
		//{
		//    return new QueryParameter(value);
		//}

		/// <summary>
		/// 将short隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(short value)
		{
			return new QueryParameter(value);
		}

		///// <summary>
		///// 将ushort隐式转换为QueryParameter
		///// </summary>
		///// <param name="value">要转换的值</param>
		///// <returns>QueryParameter实例</returns>
		//public static implicit operator QueryParameter(ushort value)
		//{
		//    return new QueryParameter(value);
		//}

		/// <summary>
		/// 将float隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(float value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将double隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(double value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将decimal隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(decimal value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将Guid隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(Guid value)
		{
			return new QueryParameter(value);
		}

		

		/// <summary>
		/// 将DateTime隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(DateTime value)
		{
			return new QueryParameter(value);
		}

		/// <summary>
		/// 将byte隐式转换为QueryParameter
		/// </summary>
		/// <param name="value">要转换的值</param>
		/// <returns>QueryParameter实例</returns>
		public static implicit operator QueryParameter(byte[] value)
		{
			return new QueryParameter(value);
		}
	}


	/// <summary>
	/// 提供CPQuery扩展方法的工具类
	/// </summary>
	public static class CPQueryExtensions
	{
		/// <summary>
		/// 将指定的字符串（T-SQL的片段）转成CPQuery对象
		/// 用法参见<see cref="CPQuery"/>类.
		/// </summary>
		/// <param name="s">T-SQL的片段的字符串</param>
		/// <returns>包含T-SQL的片段的CPQuery对象</returns>
		public static CPQuery AsCPQuery(this string s)
		{
			return new CPQuery(s);
		}
       



		/// <summary>
		/// 将对象转换成QueryParameter对象
		/// 用法参见<see cref="QueryParameter"/>类.
		/// </summary>
		/// <param name="b">要转换成QueryParameter的原对象</param>
		/// <returns>转换后的QueryParameter对象</returns>
		public static QueryParameter AsQueryParameter(this string b)
		{
			return new QueryParameter(b);
		}


	}

}
