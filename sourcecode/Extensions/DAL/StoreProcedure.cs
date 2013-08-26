using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Mysoft.Map.Extensions.Reflection;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// 表示存储过程调用的封装
	/// </summary>
	/// <example>
	/// <para>下面的代码演示了通过匿名对象,创建StoreProcedure对象实例的用法</para>
	/// <code>
	///		//声明匿名对象
	///		var product = new {
	///			ProductName = Guid.NewGuid().ToString(),
	///			CategoryID = 1,
	///			Unit = "个",
	///			UnitPrice = 12.36,
	///			Quantity = 25,
	///			Remark = "mmmmmmmmmmmmmm",
	///			ProductID = (SPOut)0		// 输出参数,参见SPOut类说明
	///		};
	///
	///		//创建StoreProcedure对象
	///		StoreProcedure sp = new StoreProcedure("InsertProduct", product);
	///	    //执行存储过程
	///		sp.ExecuteNonQuery();
	/// </code>
	/// <para>下面的代码演示了通过SqlParameter数组,创建StoreProcedure对象实例的用法</para>
	/// <code>
	///		//声明参数数组
	///		SqlParameter[] parameters1 = new SqlParameter[7];
	///
	///		parameters1[0] = new SqlParameter("@ProductName", SqlDbType.NVarChar, 50);
	///		parameters1[0].Value = "测试产品名";
	///		parameters1[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
	///		parameters1[1].Value = 1;
	///		parameters1[2] = new SqlParameter("@Unit", SqlDbType.NVarChar, 10);
	///		parameters1[2].Value = "个";
	///		parameters1[3] = new SqlParameter("@UnitPrice", SqlDbType.Money);
	///		parameters1[3].Value = 55;
	///		parameters1[4] = new SqlParameter("@Quantity", SqlDbType.Int);
	///		parameters1[4].Value = 44;
	///		parameters1[5] = new SqlParameter("@Remark", SqlDbType.NText);
	///		parameters1[5].Value = "产品备注";
	///		parameters1[6] = new SqlParameter("@ProductID", SqlDbType.Int);
	///		parameters1[6].Direction = ParameterDirection.Output;
	///
	/// 	//执行存储过程
	///		StoreProcedure.Create("InsertProduct", parameters1).ExecuteNonQuery();
	///		
	///		//获取输出参数
	///		int newProductId = (int)parameters1[6].Value;
	/// </code>
	/// </example>
	public sealed class StoreProcedure : IDbExecute
	{
		/// <summary>
		/// 存储过程内部的SqlCommand对象
		/// </summary>
		public SqlCommand Command { get; private set; }

		/// <summary>
		/// 通过存储过程名称、SqlParameter参数数组的方式,创建StoreProcedure对象的实例
		/// </summary>
		/// <param name="spName">存储过程名</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>存储过程对象</returns>
		/// <example>
		/// <para>下面的代码演示了通过存储过程名称、SqlParameter参数数组,创建StoreProcedure对象的实例的用法</para>
		/// <code>
		///		//声明参数数组
		///		SqlParameter[] parameters1 = new SqlParameter[7];
		///
		///		parameters1[0] = new SqlParameter("@ProductName", SqlDbType.NVarChar, 50);
		///		parameters1[0].Value = "测试产品名";
		///		parameters1[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
		///		parameters1[1].Value = 1;
		///		parameters1[2] = new SqlParameter("@Unit", SqlDbType.NVarChar, 10);
		///		parameters1[2].Value = "个";
		///		parameters1[3] = new SqlParameter("@UnitPrice", SqlDbType.Money);
		///		parameters1[3].Value = 55;
		///		parameters1[4] = new SqlParameter("@Quantity", SqlDbType.Int);
		///		parameters1[4].Value = 44;
		///		parameters1[5] = new SqlParameter("@Remark", SqlDbType.NText);
		///		parameters1[5].Value = "产品备注";
		///		parameters1[6] = new SqlParameter("@ProductID", SqlDbType.Int);
		///		parameters1[6].Direction = ParameterDirection.Output;
		///
		///		StoreProcedure.Create("InsertProduct", parameters1).ExecuteNonQuery();
		///		
		///		//获取输出参数
		///		int newProductId = (int)parameters1[6].Value;
		/// </code>
		/// </example>
		public StoreProcedure(string spName, params SqlParameter[] parameters)
		{
			if( string.IsNullOrEmpty(spName) )
				throw new ArgumentNullException("spName");

			// 有些存储过程是没有参数的，所以必须允许为 null ，所以不要删除下面被注释的代码。
			//if( parameters == null )
			//    throw new ArgumentNullException("parameters");


			Command = new SqlCommand();
			Command.CommandText = spName;
			Command.CommandType = CommandType.StoredProcedure;


			if( parameters != null )
				foreach( SqlParameter p in parameters )
					Command.Parameters.Add(p);
		}


		/// <summary>
		/// 通过存储过程名称、匿名对象的方式,创建StoreProcedure对象的实例
		/// </summary>
		/// <param name="spName">存储过程名称</param>
		/// <param name="parameterObject">匿名对象</param>
		/// <returns>存储过程对象</returns>
		/// <example>
		/// <para>下面的代码演示了通过存储过程名称、匿名对象,创建StoreProcedure对象的用法</para>
		/// <code>
		///		//声明匿名对象
		///		var product = new {
		///			ProductName = Guid.NewGuid().ToString(),
		///			CategoryID = 1,
		///			Unit = "个",
		///			UnitPrice = 12.36,
		///			Quantity = 25,
		///			Remark = "mmmmmmmmmmmmmm",
		///			ProductID = (SPOut)0		// 输出参数,参见SPOut类说明
		///		};
		///
		///		//创建StoreProcedure对象
		///		StoreProcedure sp = new StoreProcedure("InsertProduct", product);
		///		//执行存储过程
		///		sp.ExecuteNonQuery();
		/// </code>
		/// </example>
		public StoreProcedure(string spName, object parameterObject)
			: this(spName, GetSpParameters(parameterObject))
		{
		}

		/// <summary>
		/// 通过存储过程名称,创建StoreProcedure对象的实例
		/// </summary>
		/// <param name="spName">存储过程名称</param>
		public StoreProcedure(string spName)
			: this(spName, null)
		{
		}

		/// <summary>
		/// 创建StoreProcedure对象的实例,等同于 new StoreProcedure(string spName);
		/// </summary>
		/// <param name="spName">存储过程名称</param>
		/// <returns>StoreProcedure对象实例</returns>
		public static StoreProcedure Create(string spName)
		{
			return new StoreProcedure(spName, null);
		}

		/// <summary>
		/// 创建StoreProcedure对象的实例,等同于 new StoreProcedure(string spName, object parameterObject);
		/// </summary>
		/// <param name="spName">存储过程名称</param>
		/// <param name="parameterObject">匿名对象</param>
		/// <returns>StoreProcedure对象实例</returns>
		public static StoreProcedure Create(string spName, object parameterObject)
		{
			return new StoreProcedure(spName, parameterObject);
		}

		/// <summary>
		/// 创建StoreProcedure对象的实例,等同于 new StoreProcedure(string spName, params SqlParameter[] parameters);
		/// </summary>
		/// <param name="spName">存储过程名称</param>
		/// <param name="parameters">匿名对象</param>
		/// <returns>StoreProcedure对象实例</returns>
		public static StoreProcedure Create(string spName, params SqlParameter[] parameters)
		{
			return new StoreProcedure(spName, parameters);
		}

		//private static IEnumerable<SqlParameter> GetSpParameters(string spName, object parameterObject)
		//{
		//	if( parameterObject == null )
		//		return null;

		//	string connectionString = ConnectionScope.GetDefaultConnectionString();

		//	SqlParameter[] parameters = ParameterCache.GetSpParameters(connectionString, spName);

		//	PropertyInfo[] properties = parameterObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

		//	foreach( PropertyInfo property in properties ) {

		//		string name = "@" + property.Name;
		//		SqlParameter para = parameters.Where(p => p.ParameterName == name).FirstOrDefault();
		//		if (para != null)
		//		{
		//			object value = property.FastGetValue(parameterObject);
		//			if (value == null)
		//			{
		//				para.Value = DBNull.Value;
		//			}
		//			else
		//			{
		//				para.Value = value;
		//			}
		//		}
		//	}

		//	return parameters;
		//}

		internal static SqlParameter[] GetSpParameters(object parameterObject)
		{
			if( parameterObject == null )
				return null;

			PropertyInfo[] properties = parameterObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

			SqlParameter[] parameters = new SqlParameter[properties.Length];
			int index = 0;

			foreach( PropertyInfo property in properties ) {
				string name = "@" + property.Name;
				object value = property.FastGetValue(parameterObject);
				SqlParameter para = null;
				if( value == null || value  == DBNull.Value) {
					//throw new ArgumentException("输入参数的属性值不能为空。");
					para = new SqlParameter(name, DBNull.Value);
					//para.SqlDbType = SqlDbType.Variant;
				}
				else {
					SPOut spOutParam = value as SPOut;
					if( spOutParam != null ) {
						value = spOutParam.Value;
						if( value == null || value == DBNull.Value ) {
							throw new ArgumentException("输出参数的属性值不允许为空。");
						}
						else {
							//对于字符串类型的输出参数,长度需要默认设置为-1.这样避免了ADO.NET做长度推算.
							if( spOutParam.Value is string ) {
								para = new SqlParameter(name, spOutParam.Value);
								para.Size = -1;
							}
							else {
								para = new SqlParameter(name, spOutParam.Value);
							}

							if( spOutParam.Size != -1 ) {
								para.Size = spOutParam.Size;
							}
						}
						para.Direction = ParameterDirection.Output;
					}
					else {
						//支持匿名对象中直接使用SqlParameter
						SqlParameter parameter = value as SqlParameter;
						if( parameter != null ) {
							para = parameter;
						}
						else {
							para = new SqlParameter(name, value);
						}
					}
				}
				parameters[index] = para;
				index++;
			}

			return parameters;
		}

		
		#region Execute 方法

		/// <summary>
		/// 执行存储过程,并返回影响函数
		/// </summary>
		/// <returns>影响行数</returns>
		public int ExecuteNonQuery()
		{
			return DbHelper.ExecuteNonQuery(this.Command);
		}

		/// <summary>
		/// 执行存储过程,并将结果集填充到DataTable
		/// </summary>
		/// <returns>数据集</returns>
		public DataTable FillDataTable()
		{
			return DbHelper.FillDataTable(this.Command);
		}

		/// <summary>
		/// 执行存储过程,并将结果集填充到DataSet
		/// </summary>
		/// <returns>数据集</returns>
		public DataSet FillDataSet()
		{
			return DbHelper.FillDataSet(this.Command);
		}

		/// <summary>
		/// 执行存储过程,返回第一行,第一列的值,并将结果转换为T类型
		/// </summary>
		/// <typeparam name="T">返回值类型</typeparam>
		/// <returns>结果集的第一行,第一列</returns>
		public T ExecuteScalar<T>()
		{
			return DbHelper.ExecuteScalar<T>(this.Command);
		}
		/// <summary>
		/// 执行存储过程,将第一列的值填充到列表中,并将结果转换为T类型
		/// </summary>
		/// <typeparam name="T">返回值类型</typeparam>
		/// <returns>结果集的第一列集合</returns>
		public List<T> FillScalarList<T>()
		{
			return DbHelper.FillScalarList<T>(this.Command);
		}
		/// <summary>
		/// 执行存储过程,将结果集转换为实体集合
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了如何返回实体集合</para>
		/// <code>
		/// List&lt;TestDataType&gt; list = StoreProcedure.Create("usp_GetXXXX", null).ToList&lt;TestDataType&gt;();
		/// </code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <returns>实体集合</returns>
		public List<T> ToList<T>() where T : class, new()
		{
			return DbHelper.ToList<T>(this.Command);
		}
		/// <summary>
		/// 执行存储过程,将结果集转换为实体
		/// </summary>
		/// <example>
		/// <para>下面的代码演示了如何返回实体</para>
		/// <code>
		/// TestDataType obj = StoreProcedure.Create("usp_GetXXXX", null).ToSingle&lt;TestDataType&gt;();
		/// </code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <returns>实体</returns>
		public T ToSingle<T>() where T : class , new()
		{
			return DbHelper.ToSingle<T>(this.Command);
		}

		#endregion

	}
}
