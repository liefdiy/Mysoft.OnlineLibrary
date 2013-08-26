using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 二进制序列化的工具类
	/// </summary>
	internal static class BinSerializerHelper
	{
		/// <summary>
		/// 将对象序列化为二进制字节数组
		/// </summary>
		/// <param name="obj">要序列化的对象</param>
		/// <returns>字节数组</returns>
		public static byte[] Serialize(object obj)
		{
			if( obj == null ) 
				throw new ArgumentNullException("obj");
			
			using( MemoryStream stream = new MemoryStream() ) {
				new BinaryFormatter().Serialize(stream, obj);
				stream.Position = 0;
				return stream.ToArray();
			}
		}

		/// <summary>
		/// 将字节数组反序列化为对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="buffer">字节数组</param>
		/// <returns>对象实例</returns>
		public static T Deserialize<T>(byte[] buffer)
		{
			if( buffer == null ) 
				throw new ArgumentNullException("buffer");
			
			using( MemoryStream stream = new MemoryStream(buffer) ) {
				stream.Position = 0;
				BinaryFormatter formatter = new BinaryFormatter();
				return (T)formatter.Deserialize(stream);
			}
		}



	}
}
