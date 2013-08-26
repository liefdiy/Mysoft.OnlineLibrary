//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.Data.Common;
//using System.Reflection;
//using System.Data.SqlClient;

//namespace Mysoft.Map.Extensions.DAL
//{


//	/// <summary>
//	/// 用于缓存存储过程参数的工具类
//	/// </summary>
//	internal static class ParameterCache
//	{
//		private static Hashtable s_paramCache = new Hashtable(1024 * 4);

//		/// <summary>
//		/// 用指定的连接信息，获取一个存储过程的参数数组。
//		/// </summary>
//		/// <param name="connectionString">connectionString</param>
//		/// <param name="spName">存储过程名称</param>
//		/// <returns></returns>
//		private static SqlParameter[] DiscoverSpParameters(string connectionString, string spName)
//		{
//			SqlCommand command = null;
//			using( SqlConnection conn = new SqlConnection(connectionString) ) {
//				command = conn.CreateCommand();
//				command.CommandText = spName;
//				command.CommandType = System.Data.CommandType.StoredProcedure;

//				conn.Open();

//				SqlCommandBuilder.DeriveParameters(command);
//			}

//			// 对于SqlServer，返回值作为第一个参数
//			if( command.Parameters.Count > 0 && command.Parameters[0].Direction == System.Data.ParameterDirection.ReturnValue )
//				command.Parameters.RemoveAt(0);

//			SqlParameter[] parameters = new SqlParameter[command.Parameters.Count];
//			command.Parameters.CopyTo(parameters, 0);
//			return parameters;
//		}




//		private static SqlParameter[] CloneParameters(DbParameter[] originalParameters)
//		{
//			int count = originalParameters.Length;
//			SqlParameter[] clonedParameters = new SqlParameter[count];

//			for( int i = 0; i < count; i++ )
//				clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();


//			return clonedParameters;
//		}



//		/// <summary>
//		/// 根据一个数据库的连接，获取存储过程的参数数组
//		/// </summary>
//		/// <param name="connectionString">connectionString</param>
//		/// <param name="spName">存储过程名称</param>
//		/// <returns>存储过程的参数数组</returns>
//		public static SqlParameter[] GetSpParameters(string connectionString, string spName)
//		{
//			if( string.IsNullOrEmpty(connectionString) )
//				throw new ArgumentNullException("connectionString");
//			if( string.IsNullOrEmpty(spName) )
//				throw new ArgumentNullException("spName");


//			string key = string.Concat(spName, "###", connectionString);

//			SqlParameter[] parameters = (s_paramCache[key] as SqlParameter[]);

//			if( parameters == null ) {
//				lock( s_paramCache.SyncRoot ) {
//					parameters = (s_paramCache[key] as SqlParameter[]);
//					if( parameters == null ) {
//						parameters = DiscoverSpParameters(connectionString, spName);
//						s_paramCache[key] = parameters;
//					}
//				}
//			}

//			// 返回“克隆”后的对象，这样的返回结果可供后续调用直接使用，而不会影响缓存中的对象。
//			return CloneParameters(parameters);
//		}




//	}



//}
